namespace Web.Services
{
    public class HomeViewModelService : IHomeViewModelService
    {
        private readonly IRepository<Product> _productRepo;

        public HomeViewModelService(IRepository<Product> productRepo)
        {
            _productRepo = productRepo;
        }

        public async Task<HomeViewModel> GetHomeViewModel(int? categoryId, int? brandId)
        {
            var products = await _productRepo.GetAllAsync();

            var vm = new HomeViewModel();
            vm.CategoryId = categoryId;
            vm.BrandId = brandId;
            vm.Products = products.Select(x => new ProductViewModel
            {
                Id = x.Id,
                Name = x.Name,
                PictureUri = x.PictureUri,
                Price = x.Price
            }).ToList();

            return vm;
        }
    }
}
