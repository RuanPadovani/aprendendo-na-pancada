namespace Application.Common.Mediator;

/// <summary>
/// Define quem sabe processar um pedido específico.
/// Toda classe que processar um Command ou Query deve implementar essa interface.
/// Ex: LoginCommandHandler : IRequestHandler&lt;LoginCommand, LoginResult&gt;
/// </summary>
/// <typeparam name="TRequest">O tipo do pedido a ser processado.</typeparam>
/// <typeparam name="TResponse">O tipo da resposta a ser retornada.</typeparam>
public interface IRequestHandler<TRequest, TResponse> where TRequest : IRequest<TResponse>
{
    /// <summary>
    /// Processa o pedido e retorna a resposta.
    /// </summary>
    /// <param name="request">O pedido recebido (ex: LoginCommand com Email e Password).</param>
    /// <param name="ct">Token para cancelar a operação assíncrona.</param>
    Task<TResponse> Handle(TRequest request, CancellationToken ct);
}
