namespace Web.Interfaces
{
    public interface IBasketViewModelService
    {
        Task<BasketViewModel> GetBasketViewModelAsync();

        Task<BasketViewModel> AddItemToBasketAsync(int productId, int quantity);
    }
}
