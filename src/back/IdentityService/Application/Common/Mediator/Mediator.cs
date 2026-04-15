using Microsoft.Extensions.DependencyInjection;

namespace Application.Common.Mediator;

/// <summary>
/// Intermediário central do padrão Mediator.
/// Recebe um pedido, monta o corredor de behaviors e entrega ao handler correto.
/// </summary>
public class Mediator : IMediator
{
    /// <summary>
    /// Container de injeção de dependência.
    /// É por ele que o Mediator encontra o handler e os behaviors registrados.
    /// </summary>
    private readonly IServiceProvider _serviceProvider;

    /// <summary>
    /// Recebe o IServiceProvider via injeção de dependência.
    /// </summary>
    public Mediator(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    /// <summary>Recebe um pedido, monta o pipeline e retorna a resposta.</summary>
    /// <typeparam name="TResponse">Tipo da resposta esperada.</typeparam>
    /// <param name="request">O pedido a ser processado (ex: LoginCommand).</param>
    /// <param name="ct">Token para cancelar a operação assíncrona.</param>
    /// <returns>A resposta produzida pelo handler.</returns>
    public Task<TResponse> Send<TResponse>(IRequest<TResponse> request, CancellationToken ct = default)
    {
        // Descobre o tipo real do pedido em tempo de execução. Ex: LoginCommand.
        var requestType = request.GetType();

        // Monta o tipo do handler: IRequestHandler<LoginCommand, Result<LoginResponse>>
        var handlerType  = typeof(IRequestHandler<,>).MakeGenericType(requestType, typeof(TResponse));

        // Monta o tipo dos behaviors: IPipelineBehavior<LoginCommand, Result<LoginResponse>>
        var behaviorType = typeof(IPipelineBehavior<,>).MakeGenericType(requestType, typeof(TResponse));

        // Busca o handler registrado no container. Lança exceção se não encontrar.
        dynamic handler = _serviceProvider.GetRequiredService(handlerType);

        // Busca todos os behaviors registrados para esse pedido. Retorna lista vazia se não houver nenhum.
        dynamic behaviors = _serviceProvider.GetServices(behaviorType);

        // Define o ponto final do corredor: o handler em si.
        // Func<Task<TResponse>> é uma função que, quando chamada, executa o handler.
        Func<Task<TResponse>> pipeline = () => handler.Handle((dynamic)request, ct);

        // Monta o corredor de trás para frente.
        // Reverse() garante que o primeiro behavior registrado seja o primeiro a executar.
        // Cada iteração embrulha o pipeline anterior como o "next" do behavior atual.
        foreach (var behavior in ((IEnumerable<dynamic>)behaviors).Reverse())
        {
            var current = pipeline; // salva o pipeline atual antes de sobrescrever
            pipeline = () => behavior.Handle((dynamic)request, current, ct);
        }

        // Dispara o corredor inteiro: Behavior1 → Behavior2 → Handler
        return pipeline();
    }
}
