using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Class02.Models;

namespace Class02.Controllers
{
    public class PersonsController : Controller
    {
        // GET: PersonsController
        public ActionResult Index()
        {
            return View();
        }

        // GET: PersonsController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: PersonsController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PersonsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                if(string.IsNullOrEmpty(collection["name"]) == true)
                {
                    ModelState.AddModelError("name", "Mnadatory Field");
                }
                if (string.IsNullOrEmpty(collection["age"]) == true)
                {
                    ModelState.AddModelError("age", "Mnadatory Field");
                }
                else
                {
                    int aux;
                    try
                    {
                        aux = int.Parse(collection["age"]);
                        if(aux < 18 || aux > 100)
                        {
                            ModelState.AddModelError("age", "Age should be between 18 and 100");
                        }
                    }
                    catch (FormatException)
                    {
                        ModelState.AddModelError("age", "Must indicate an integer value");
                    }
                }
                if(ModelState.IsValid)
                {
                    //process the information

                    //transfers information to other action
                    TempData["values"] = collection["name"] + " [" + collection["age"] + "]";
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    return View();
                }
            }
            catch
            {
                return View();
            }
        }

        [HttpPost]
        public ActionResult Create(Person newPerson)
        {
            if (ModelState.IsValid)
            {
                //process the information

                //transfers information to other action
                TempData["values"] = newPerson.Name + " [" + newPerson.Age + "]";
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View();
            }
        }

        // GET: PersonsController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: PersonsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: PersonsController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: PersonsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
