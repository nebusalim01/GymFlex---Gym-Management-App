using GymFlex.Models;
using Microsoft.AspNetCore.Mvc;

namespace GymFlex.Controllers
{
    public class UserWorkoutController : Controller
    {
        public IActionResult ViewWorkoutDays(int userId)
        {
            OperationsDB db = new OperationsDB();

            int planId = db.GetAssignedWorkoutPlan(userId);

            if (planId == 0)
            {
                TempData["msg"] = "No workout plan assigned.";
                return RedirectToAction("UserProfile_Pageload", "UserProfile", new { id = userId });
            }

            var days = db.GetWorkoutDays(planId);

            ViewBag.UserId = userId;

            return View(days);
        }

        public IActionResult ViewExercises(int workoutDayId, int userId)
        {
            OperationsDB db = new OperationsDB();

            var exercises = db.GetExercises(workoutDayId);

            ViewBag.UserId = userId;

            return View(exercises);
        }
    }
}
