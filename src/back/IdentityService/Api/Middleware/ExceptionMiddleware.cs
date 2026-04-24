using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace Api.Middleware;

public sealed class ExceptionMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    /// <summary>Middleware global que captura exceções e retorna respostas no formato Problem Details (RFC 7807).</summary>
    public async Task InvokeAsync(HttpContext context) // nome obrigatório
    {
        try
        {
            await _next(context);
        }
        catch (ValidationException ex)
        {
            await WriteProblemDetails(
                context: context,
                statusCode: StatusCodes.Status400BadRequest,
                title: "Erro de validação!",
                detail: string.Join("; ", ex.Errors.Select(e => e.ErrorMessage)));
        }
        catch (UnauthorizedAccessException ex)
        {
            await WriteProblemDetails(
                context: context,
                statusCode: StatusCodes.Status401Unauthorized,
                title: "Não autorizado!",
                detail: ex.Message);
        }
        catch (KeyNotFoundException ex)
        {
            await WriteProblemDetails(
                context: context,
                statusCode: StatusCodes.Status404NotFound,
                title: "Não encontrado!",
                detail: ex.Message);
        }
        catch (Exception)
        {
            await WriteProblemDetails(
                context: context,
                statusCode: StatusCodes.Status500InternalServerError,
                title: "Erro interno do servidor",
                detail: "Ocorreu um erro inesperado. Tente novamente mais tarde.");
        }
    }

    /// <summary>Serializa e escreve a resposta de erro no formato Problem Details.</summary>
    /// <param name="context">Contexto HTTP da requisição.</param>
    /// <param name="statusCode">Código HTTP do erro.</param>
    /// <param name="title">Título do erro.</param>
    /// <param name="detail">Descrição detalhada do erro.</param>
    private static async Task WriteProblemDetails(HttpContext context, int statusCode, string title, string detail)
    {
        var problem = new ProblemDetails
        {
            Status = statusCode,
            Title = title,
            Detail = detail,
            Type = $"https://httpstatuses.com{statusCode}"
        };

        context.Response.ContentType = "application/problem+json";
        context.Response.StatusCode = statusCode;

        await context.Response.WriteAsJsonAsync(problem);
    }

}
