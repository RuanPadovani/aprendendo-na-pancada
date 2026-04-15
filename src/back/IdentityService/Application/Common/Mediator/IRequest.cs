namespace Application.Common.Mediator;

/// <summary>
/// Marcador que identifica um pedido e qual tipo de resposta ele retorna.
/// Toda Command ou Query deve implementar essa interface.
/// Ex: LoginCommand : IRequest&lt;LoginResult&gt;
/// </summary>
/// <typeparam name="TResponse">Tipo da resposta que esse pedido vai produzir.</typeparam>
public interface IRequest<TResponse> { }
