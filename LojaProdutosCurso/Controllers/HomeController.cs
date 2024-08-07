using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace LojaProdutosCurso.Controllers
{
    public class HomeController : Controller
    {

        public IActionResult Index()
        {
            return View();
        }

       
    }
}
