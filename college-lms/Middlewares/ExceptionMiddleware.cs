using System.ComponentModel.DataAnnotations;
using college_lms.Data.DTOs.Base;

namespace college_lms.Middlewares;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionMiddleware> _logger;

    public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (UnauthorizedAccessException ex)
        {
            _logger.LogWarning(ex, "Unauthorized access");
            await HandleExceptionAsync(
                context,
                new ErrorResponse { Message = "Недостаточно прав" },
                StatusCodes.Status403Forbidden
            );
        }
        catch (ValidationException ex)
        {
            _logger.LogWarning(ex, "Validation failed");
            await HandleExceptionAsync(
                context,
                new ValidationErrorResponse
                {
                    Message = "Одно или несколько полей не прошли валидацию",
                    Errors = ex.ValidationResult.MemberNames.Select(name => new ValidationError
                    {
                        Field = name,
                        Message = ex.ValidationResult.ErrorMessage,
                    }),
                },
                StatusCodes.Status422UnprocessableEntity
            );
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unhandled exception");
            await HandleExceptionAsync(
                context,
                new ErrorResponse { Message = "Произошла непредвиденная ошибка" },
                StatusCodes.Status500InternalServerError
            );
        }
    }

    private static Task HandleExceptionAsync<T>(HttpContext context, T response, int statusCode)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = statusCode;

        return context.Response.WriteAsJsonAsync(response);
    }
}
