using FitnessTrackerApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace FitnessTrackerApp.Controllers
{
    public class UserController : Controller
    {
        private static List<User> users = new List<User> { new User() };

        public IActionResult Index()
        {
            IEnumerable<User> userList = users;
            return View(userList);
        }

        // GET
        public IActionResult Create()
        {
            return View();
        }

        // POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(User obj)
        {
            if (ModelState.IsValid)
            {
                obj.UserId = users.Count + 1;
                users.Add(obj);
                TempData["success"] = "User created successfully";
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        // GET
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var user = users.FirstOrDefault(u => u.UserId == id);

            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(User obj)
        {
            if (ModelState.IsValid)
            {
                var user = users.FirstOrDefault(u => u.UserId == obj.UserId);

                if (user == null)
                {
                    return NotFound();
                }

                user.Name = obj.Name;
                user.Email = obj.Email;
                user.Age = obj.Age;
                user.HeightInCm = obj.HeightInCm;
                user.InitialWeightInKg = obj.InitialWeightInKg;
                user.RegularCalorieIntake = obj.RegularCalorieIntake;
                user.RegularCalorieExpenditure = obj.RegularCalorieExpenditure;

                TempData["success"] = "User updated successfully";
                return RedirectToAction("Index");
            }

            return View(obj);
        }

        // GET
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var user = users.FirstOrDefault(u => u.UserId == id);

            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePOST(int? id)
        {
            var user = users.FirstOrDefault(u => u.UserId == id);

            if (user == null)
            {
                return NotFound();
            }

            users.Remove(user);
            TempData["success"] = "User deleted successfully";
            return RedirectToAction("Index");
        }
    }
}
