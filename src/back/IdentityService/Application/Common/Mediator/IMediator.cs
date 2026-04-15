namespace Application.Common.Mediator;

/// <summary>
/// Contrato do intermediário central.
/// O Controller só conhece essa interface — ele entrega o pedido aqui
/// sem saber quem vai processar nem como.
/// </summary>
public interface IMediator
{
    /// <summary>
    /// Envia um pedido pelo pipeline e retorna a resposta.
    /// </summary>
    /// <typeparam name="TResponse">Tipo da resposta esperada.</typeparam>
    /// <param name="request">O pedido a ser enviado (ex: LoginCommand).</param>
    /// <param name="ct">Token para cancelar a operação assíncrona.</param>
    Task<TResponse> Send<TResponse>(IRequest<TResponse> request, CancellationToken ct = default);
}
