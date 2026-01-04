using GymFlex.Models;
using Microsoft.AspNetCore.Mvc;

namespace GymFlex.Controllers
{
    public class WorkoutPlanController : Controller
    {
        public IActionResult CreateWorkoutPlan_Pageload()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateWorkoutPlan_Click(WorkoutPlanModel obj)
        {
            if (!ModelState.IsValid)
                return View("CreateWorkoutPlan", obj);

            OperationsDB db = new OperationsDB();
            int res = db.InsertWorkoutPlan(obj);

            if (res == 1)
            {
                ViewBag.msg = "Workout plan created successfully";
                ModelState.Clear();
            }
            else
            {
                ViewBag.msg = "Failed to create workout plan";
            }

            return View("CreateWorkoutPlan");
        }
    }
}
