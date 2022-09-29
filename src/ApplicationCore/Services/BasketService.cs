using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using ApplicationCore.Specifications;

namespace ApplicationCore.Services
{
    public class BasketService : IBasketService
    {
        private readonly IRepository<Basket> _basketRepo;
        private readonly IRepository<Product> _productRepo;

        public BasketService(IRepository<Basket> basketRepo, IRepository<Product> productRepo)
        {
            _basketRepo = basketRepo;
            _productRepo = productRepo;
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
    }
}
