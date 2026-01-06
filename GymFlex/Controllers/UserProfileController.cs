using GymFlex.Models;
using Microsoft.AspNetCore.Mvc;

namespace GymFlex.Controllers
{
    public class UserProfileController : Controller
    {
        public IActionResult UserProfile_PageLoad(int id)
        {
            OperationsDB db = new OperationsDB();

            var model = db.GetUserProfile(id);

            model.TodayExercises = db.GetTodayExercises(id);
            model.TodayMeals = db.GetTodayMeals(id);

            return View(model);
        }

        // Load Edit Page
        public IActionResult EditUserProfile_PageLoad(int id)
        {
            OperationsDB db = new OperationsDB();
            var model = db.GetUserProfileForEdit(id);
            return View(model);
        }

        // Save edit
        [HttpPost]
        public IActionResult EditUserProfile_Click(EditUserProfileModel obj)
        {
            if (!ModelState.IsValid)
                return View("EditUserProfile_PageLoad", obj);

            // Recalculate BMI
            obj.BMI = (float)obj.Weight / (float)Math.Pow(obj.Height / 100.0, 2);

            OperationsDB db = new OperationsDB();
            int res = db.UpdateUserProfile(obj);

            if (res == 1)
            {
                TempData["msg"] = "Profile updated successfully!";
                return RedirectToAction("UserProfile_PageLoad", new { id = obj.User_Id });
            }

            ViewBag.msg = "Update failed";
            return View("EditUserProfile_PageLoad", obj);
        }
    }
}
