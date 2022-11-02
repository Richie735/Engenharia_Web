using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Class02.Controllers
{
    public class ExampleController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(IFormCollection formData) 
        {
            //action used to process the form submition
            ViewData["text_inserted"] = formData["name"];       //transfer datas to the View
            ViewData["other_text_inserted"] = formData["age"];  //transfer datas to the View

            return View("Index2");  //uses another view instead using the default view name
        }
    }
}
