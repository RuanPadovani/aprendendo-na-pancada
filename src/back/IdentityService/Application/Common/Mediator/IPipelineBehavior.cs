namespace Application.Common.Mediator;

/// <summary>
/// Define um interceptador no pipeline.
/// Qualquer classe que implementar essa interface pode executar lógica
/// antes e depois do handler, e decide se o pedido continua passando
/// pelo corredor chamando next().
/// Ex: LoggingBehavior, ValidationBehavior.
/// </summary>
/// <typeparam name="TRequest">O tipo do pedido que será interceptado.</typeparam>
/// <typeparam name="TResponse">O tipo da resposta esperada.</typeparam>
public interface IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
{
    /// <summary>
    /// Intercepta o pedido. Chame next() para passar para o próximo do corredor.
    /// Se next() não for chamado, o handler nunca executa.
    /// </summary>
    /// <param name="request">O pedido em trânsito pelo pipeline.</param>
    /// <param name="next">Função que passa o pedido para o próximo interceptador ou handler.</param>
    /// <param name="ct">Token para cancelar a operação assíncrona.</param>
    Task<TResponse> Handle(TRequest request, Func<Task<TResponse>> next, CancellationToken ct);
}
