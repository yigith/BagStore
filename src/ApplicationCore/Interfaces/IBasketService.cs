using ApplicationCore.Entities;

namespace ApplicationCore.Interfaces
{
    public interface IBasketService
    {
        Task<Basket> GetOrCreateBasketAsync(string buyerId);

        Task<Basket> AddItemToBasketAsync(string buyerId, int productId, int quantity);
    }
}
