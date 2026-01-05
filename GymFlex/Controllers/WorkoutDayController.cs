using GymFlex.Models;
using Microsoft.AspNetCore.Mvc;

namespace GymFlex.Controllers
{
    public class WorkoutDayController : Controller
    {
        public IActionResult AddWorkoutDay_PageLoad(int planId)
        {
            WorkoutDayModel obj = new WorkoutDayModel();
            obj.Plan_Id = planId;

            return View(obj);
        }
        [HttpPost]
        public IActionResult AddWorkoutDay_Click(WorkoutDayModel obj)
        {
            if (!ModelState.IsValid)
            {
                return View("AddWorkoutDay_PageLoad", obj);
            }

            OperationsDB db = new OperationsDB();
            int result = db.InsertWorkoutDay(obj);

            if (result == 1)
            {
                ViewBag.msg = "Workout Day Inserted Successfully";
                ModelState.Clear();
                return View("AddWorkoutDay_PageLoad", new WorkoutDayModel() { Plan_Id = obj.Plan_Id });
            }
            else
            {
                ViewBag.msg = "Insertion Failed";
                return View("AddWorkoutDay_PageLoad", obj);
            }
        }
        [HttpGet]
        public IActionResult ViewWorkoutDays(int planId)
        {
            OperationsDB db = new OperationsDB();

            var list = db.GetWorkoutDaysByPlan(planId);

            ViewBag.PlanId = planId;

            return View(list);
        }
    }
}
