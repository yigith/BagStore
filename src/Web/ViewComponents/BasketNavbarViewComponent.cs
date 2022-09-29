using Microsoft.AspNetCore.Mvc;

namespace Web.ViewComponents
{
    public class BasketNavbarViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View();
        }
    }
}
