using FitnessTrackerApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace FitnessTrackerApp.Controllers
{
    public class CategoryController : Controller
    {

        private static List<Category> items = new List<Category> { new Category() };

        public IActionResult Index()
        {

            IEnumerable<Category> categories = items;
            return View(categories);
        }

        //GET
        public IActionResult Create()
        {
            return View();
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken] 
        public IActionResult Create(Category obj)
        {
            if (obj.Name == obj.DisplayOrder.ToString()) { 
               // ModelState.AddModelError("CustomError", "The DisplayOrder cannot exactly match the Name.");
                ModelState.AddModelError("Name", "The DisplayOrder cannot exactly match the Name.");
            }

            if (ModelState.IsValid)
            {
                obj.Id = items.Count+1;
                items.Add(obj);
                TempData["success"] = "Category created successfully";

                return RedirectToAction("Index");
            }
            return View(obj);
            
        }


        //GET
        public IActionResult Edit(int? id)
        {
            if(id == null || id == 0)
            {
                return NotFound();
            }

            var categoryFromObject = items.Find(u => u.Id == id);
            //var categoryFromObject = items.FirstOrDefault(u => u.Id == id);
            //var categoryFromObject = items.SingleOrDefault(u => u.Id == id);

            if (categoryFromObject == null)
            {
                return NotFound();
            }

            return View(categoryFromObject);
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category obj)
        {
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                // ModelState.AddModelError("CustomError", "The DisplayOrder cannot exactly match the Name.");
                ModelState.AddModelError("Name", "The DisplayOrder cannot exactly match the Name.");
            }

            if (ModelState.IsValid)
            {
                var categoryFromObject = items.Find(u => u.Id == obj.Id);

                if (categoryFromObject == null)
                {
                    return NotFound();
                }

                categoryFromObject.Name = obj.Name;
                categoryFromObject.DisplayOrder = obj.DisplayOrder;
                TempData["success"] = "Category updated successfully";
                return RedirectToAction("Index");
            }

            return View(obj);

        }



        //GET
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var categoryFromObject = items.Find(u => u.Id == id);
            //var categoryFromObject = items.FirstOrDefault(u => u.Id == id);
            //var categoryFromObject = items.SingleOrDefault(u => u.Id == id);

            if (categoryFromObject == null)
            {
                return NotFound();
            }

            return View(categoryFromObject);
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePOST(int? id)
        {

            var categoryFromObject = items.Find(u => u.Id == id);

            if (categoryFromObject == null)
            {
                return NotFound();
            }

            items.Remove(categoryFromObject);
            TempData["success"] = "Category deleted successfully";
            return RedirectToAction("Index");
           
        }

    }
}
