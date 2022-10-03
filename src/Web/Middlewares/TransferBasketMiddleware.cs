namespace Web.Middlewares
{
    public class TransferBasketMiddleware
    {
        private readonly RequestDelegate _next;

        public TransferBasketMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, IBasketViewModelService basketViewModelService)
        {
            await basketViewModelService.TransferBasketAsync();
            await _next(context);
        }
    }

    public static class TransferBasketMiddlewareExtensions
    {
        public static IApplicationBuilder UseTransferBasket(this IApplicationBuilder app)
        {
            return app.UseMiddleware<TransferBasketMiddleware>();
        }
    }
}
