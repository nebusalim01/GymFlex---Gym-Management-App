using GymFlex.Models;
using Microsoft.AspNetCore.Mvc;

namespace GymFlex.Controllers
{
    public class WorkoutExerciseController : Controller
    {
        public IActionResult AddExercise_PageLoad(int dayId)
        {
            WorkoutExerciseModel obj = new WorkoutExerciseModel();
            obj.Workoutday_Id = dayId;
            return View(obj);
        }
        [HttpPost]
        public IActionResult AddExercise_Click(WorkoutExerciseModel obj)
        {
            if (!ModelState.IsValid)
                return View("AddExercise_PageLoad", obj);

            OperationsDB db = new OperationsDB();
            int result = db.InsertWorkoutExercise(obj);

            if (result == 1)
            {
                TempData["msg"] = "Exercise Added Successfully";
                return RedirectToAction("ViewExercises", new { dayId = obj.Workoutday_Id });
            }

            ViewBag.msg = "Failed to add exercise";
            return View("AddExercise_PageLoad", obj);
        }

        public IActionResult ViewExercises(int dayId)
        {
            OperationsDB db = new OperationsDB();
            var list = db.GetExercisesByDay(dayId);

            ViewBag.DayId = dayId;

            return View(list);
        }
    }
}
