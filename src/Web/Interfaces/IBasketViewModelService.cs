namespace Web.Interfaces
{
    public interface IBasketViewModelService
    {
        Task<BasketViewModel> AddItemToBasketAsync(int productId, int quantity);
    }
}
