using GymFlex.Models;
using Microsoft.AspNetCore.Mvc;

namespace GymFlex.Controllers
{
    public class UserDietController : Controller
    {
        public IActionResult ViewDietPlan(int userId)
        {
            OperationsDB db = new OperationsDB();

            string goal = db.GetUserGoal(userId);

            var days = db.GetDietDays(goal);

            ViewBag.UserId = userId;
            ViewBag.Goal = goal;

            return View(days);
        }

        public IActionResult ViewDietMeals(int userId, int day)
        {
            OperationsDB db = new OperationsDB();

            string goal = db.GetUserGoal(userId);

            var meals = db.GetDietMeals(goal, day);

            ViewBag.UserId = userId;
            ViewBag.Day = day;
            ViewBag.Goal = goal;

            return View(meals);
        }
    }
}
