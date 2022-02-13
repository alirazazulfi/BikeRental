using BikeRental.DTO.Helpers;
using Microsoft.AspNetCore.Http;
using System.Net;
using System.Text.Json;

namespace BikeRental.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate next;

        public ExceptionMiddleware(RequestDelegate next) => this.next = next;

        public async Task Invoke(HttpContext context)
        {
            try { await next(context); }
            catch (Exception ex) { await HandleException(context, ex); }
        }

        private static Task HandleException(HttpContext context, Exception ex)
        {
            HttpStatusCode code = HttpStatusCode.InternalServerError; // 500 if unexpected 
            string result = JsonSerializer.Serialize(ApiResponse.ExceptionResponse(ex));
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;
            return context.Response.WriteAsync(result);
        }
    }
}