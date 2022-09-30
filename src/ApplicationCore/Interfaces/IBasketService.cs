using ApplicationCore.Entities;

namespace ApplicationCore.Interfaces
{
    public interface IBasketService
    {
        Task<Basket> GetOrCreateBasketAsync(string buyerId);

        Task<Basket> AddItemToBasketAsync(string buyerId, int productId, int quantity);

        Task EmptyBasketAsync(string buyerId);

        Task DeleteBasketItemAsync(string buyerId, int productId);

        Task<Basket> SetQuantities(string buyerId, Dictionary<int, int> quantities);
    }
}
