using ApplicationCore.Specifications;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Web.Services
{
    public class HomeViewModelService : IHomeViewModelService
    {
        private readonly IRepository<Product> _productRepo;
        private readonly IRepository<Category> _categoryRepo;
        private readonly IRepository<Brand> _brandRepo;

        public HomeViewModelService(IRepository<Product> productRepo, IRepository<Category> categoryRepo, IRepository<Brand> brandRepo)
        {
            _productRepo = productRepo;
            _categoryRepo = categoryRepo;
            _brandRepo = brandRepo;
        }

        public async Task<HomeViewModel> GetHomeViewModelAsync(int? categoryId, int? brandId, int pageId)
        {
            var ipp = Constants.ITEMS_PER_PAGE;
            var specFilter = new ProductsFilterSpecification(categoryId, brandId);
            var specPaginagedFilter = new ProductsFilterSpecification(categoryId, brandId, (pageId - 1) * ipp, ipp);
            var products = await _productRepo.GetAllAsync(specPaginagedFilter);
            var totalItems = await _productRepo.CountAsync(specFilter);

            var vm = new HomeViewModel();
            vm.PaginationInfo = new PaginationInfoViewModel(pageId, products.Count, totalItems);
            vm.CategoryId = categoryId;
            vm.BrandId = brandId;
            vm.Products = products.Select(x => new ProductViewModel
            {
                Id = x.Id,
                Name = x.Name,
                PictureUri = x.PictureUri,
                Price = x.Price
            }).ToList();
            vm.Categories = (await _categoryRepo.GetAllAsync()).Select(x => new SelectListItem()
            {
                Text = x.Name,
                Value = x.Id.ToString(),
                Selected = categoryId == x.Id
            }).OrderBy(x => x.Text).ToList();
            vm.Brands = (await _brandRepo.GetAllAsync()).Select(x => new SelectListItem()
            {
                Text = x.Name,
                Value = x.Id.ToString(),
                Selected = brandId == x.Id
            }).OrderBy(x => x.Text).ToList();

            return vm;
        }
    }
}
