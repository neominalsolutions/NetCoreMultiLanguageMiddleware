using static System.Net.Mime.MediaTypeNames;

namespace NetCoreMultiLanguageMiddleware.Middlewares
{
  public class ExceptionMiddleware : IMiddleware
  {
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {

      try
      {
        await next(context);
      }
      catch (Exception)
      {
        context.Response.StatusCode = StatusCodes.Status500InternalServerError;
        context.Response.ContentType = Text.Html;
        await context.Response.WriteAsync("<h1>Hata</h1><p>Sunucuda beklemeyen bir hata meydana geldi</p>");
      }

    }
  }
}
