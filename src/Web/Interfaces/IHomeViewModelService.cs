namespace Web.Interfaces
{
    public interface IHomeViewModelService
    {
        Task<HomeViewModel> GetHomeViewModel(int? categoryId, int? brandId, int pageId);
    }
}
