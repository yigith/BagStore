using System.Security.Claims;

namespace Web.Services
{
    public class BasketViewModelService : IBasketViewModelService
    {
        private readonly IBasketService _basketService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        private HttpContext? HttpContext => _httpContextAccessor.HttpContext;
        private string? UserId => HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);
        private string? AnonId => HttpContext?.Request.Cookies[Constants.BASKET_COOKIENAME];
        private string BuyerId => UserId ?? AnonId ?? CreateAnonymousId();

        private string _createdAnonId = null!;
        private string CreateAnonymousId()
        {
            if (_createdAnonId == null)
            {
                _createdAnonId = Guid.NewGuid().ToString();

                HttpContext?.Response.Cookies.Append(Constants.BASKET_COOKIENAME, _createdAnonId, new CookieOptions()
                {
                    Expires = DateTime.Now.AddDays(14),
                    IsEssential = true
                });
            }

            return _createdAnonId;
        }

        public BasketViewModelService(IBasketService basketService, IHttpContextAccessor httpContextAccessor)
        {
            _basketService = basketService;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<BasketViewModel> AddItemToBasketAsync(int productId, int quantity)
        {
            var basket = await _basketService.AddItemToBasketAsync(BuyerId, productId, quantity);
            return basket.ToBasketViewModel();
        }

        public async Task<BasketViewModel> GetBasketViewModelAsync()
        {
            var basket = await _basketService.GetOrCreateBasketAsync(BuyerId);
            return basket.ToBasketViewModel();
        }

        public async Task EmptyBasketAsync()
        {
            await _basketService.EmptyBasketAsync(BuyerId);
        }

        public async Task RemoveBasketItemAsync(int productId)
        {
            await _basketService.DeleteBasketItemAsync(BuyerId, productId);
        }

        public async Task<BasketViewModel> UpdateBasketAsync(Dictionary<int, int> quantities)
        {
            var basket = await _basketService.SetQuantities(BuyerId, quantities);
            return basket.ToBasketViewModel();
        }
    }
}
