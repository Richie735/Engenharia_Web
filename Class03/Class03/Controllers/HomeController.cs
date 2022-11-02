using Class03.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using System.Diagnostics;
using System.Drawing;


namespace Class03.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHostEnvironment _he;  //necessary to get application folder

        public HomeController(ILogger<HomeController> logger, IHostEnvironment e)
        {
            _logger = logger;
            _he = e;    //injects in the constructor information about hostEnvironment
        }

        public IActionResult Upload()
        {
            return View();
        }

        public IActionResult Index()
        {
            //get the information of the files in the Documents folder
            //using the classe DocFiles
            DocFiles files = new DocFiles();

            return View(files.GetFiles(_he));
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost]
        public IActionResult Upload(IFormFile Name)
        {
            //other file properties could be checked here but we assume everything is OK
            if (ModelState.IsValid)
            {
                string destination = Path.Combine(_he.ContentRootPath, "wwwroot/Documents/", Path.GetFileName(Name.FileName));

                //creates a filestream to store the file bytes
                FileStream fs = new FileStream(destination, FileMode.Create);

                Name.CopyTo(fs);
                fs.Close();

                //after saving file, redirects to file listing
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
        }

        public IActionResult Download(string id)
        {
            //id is the filename
            string pathFile = Path.Combine(_he.ContentRootPath, "wwwroot/Documents/", id);

            byte[] fileBytes = System.IO.File.ReadAllBytes(pathFile);

            string mimeType;
            //this code assumes that content type is always obtained.
            //otherwise, the result should be verified (boolean value)
            new FileExtensionContentTypeProvider().TryGetContentType(id, out mimeType);

            return File(fileBytes, mimeType);
        }

        public IActionResult Delete(string id)
        {
            String file = _he.ContentRootPath + "wwwroot/Documents/" + id;
            System.IO.File.Delete(file);
            return RedirectToAction("Index");
        }
    }
}