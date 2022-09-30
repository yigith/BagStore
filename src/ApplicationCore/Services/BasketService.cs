using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using ApplicationCore.Specifications;

namespace ApplicationCore.Services
{
    public class BasketService : IBasketService
    {
        private readonly IRepository<Basket> _basketRepo;
        private readonly IRepository<Product> _productRepo;
        private readonly IRepository<BasketItem> _basketItemRepo;

        public BasketService(IRepository<Basket> basketRepo, IRepository<Product> productRepo, IRepository<BasketItem> basketItemRepo)
        {
            _basketRepo = basketRepo;
            _productRepo = productRepo;
            _basketItemRepo = basketItemRepo;
        }

        public async Task<Basket> AddItemToBasketAsync(string buyerId, int productId, int quantity)
        {
            var basket = await GetOrCreateBasketAsync(buyerId);
            var basketItem = basket.Items.FirstOrDefault(x => x.ProductId == productId);

            if (basketItem == null)
            {
                basketItem = new BasketItem()
                {
                    ProductId = productId,
                    Quantity = quantity,
                    Product = await _productRepo.GetByIdAsync(productId) ?? null!
                };
                basket.Items.Add(basketItem);
            }
            else
            {
                basketItem.Quantity += quantity;
            }
            await _basketRepo.UpdateAsync(basket);

            return basket;
        }

        public async Task DeleteBasketItemAsync(string buyerId, int productId)
        {
            var basket = await GetOrCreateBasketAsync(buyerId);
            var basketItem = basket.Items.FirstOrDefault(x => x.ProductId == productId);
            if (basketItem == null) return;
            await _basketItemRepo.DeleteAsync(basketItem);
        }

        public async Task EmptyBasketAsync(string buyerId)
        {
            var basket = await GetOrCreateBasketAsync(buyerId);

            foreach (var item in basket.Items.ToList())
            {
                await _basketItemRepo.DeleteAsync(item);
            }
        }

        public async Task<Basket> GetOrCreateBasketAsync(string buyerId)
        {
            var specBasket = new BasketWithItemsSpecification(buyerId);
            var basket = await _basketRepo.FirstOrDefaultAsync(specBasket);

            if (basket == null)
            {
                basket = new Basket() { BuyerId = buyerId };
                await _basketRepo.AddAsync(basket);
            }

            return basket;
        }

        public async Task<Basket> SetQuantities(string buyerId, Dictionary<int, int> quantities)
        {
            var basket = await GetOrCreateBasketAsync(buyerId);

            foreach (var item in basket.Items)
            {
                if (quantities.ContainsKey(item.ProductId))
                {
                    item.Quantity = quantities[item.ProductId];
                    await _basketItemRepo.UpdateAsync(item);
                }
            }

            return basket;
        }
    }
}
