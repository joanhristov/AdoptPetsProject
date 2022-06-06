namespace AdoptPetsProject.Areas.Admin.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    public class PetsController : AdminController
    {
        public IActionResult Index() => View();
    }
}
