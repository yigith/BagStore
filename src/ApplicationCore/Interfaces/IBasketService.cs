using ApplicationCore.Entities;

namespace ApplicationCore.Interfaces
{
    public interface IBasketService
    {
        Task<Basket> AddItemToBasketAsync(string buyerId, int productId, int quantity);
    }
}
