using GymFlex.Models;
using Microsoft.AspNetCore.Mvc;

namespace GymFlex.Controllers
{
    public class WorkoutProgressController : Controller
    {
        public IActionResult StartWorkout(string exerciseName, int sets, int userId)
        {
            WorkoutProgressModel model = new()
            {
                User_Id = userId,
                ExerciseName = exerciseName,
                SetsPlanned = sets
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult CompleteWorkout(WorkoutProgressModel model)
        {
            OperationsDB db = new OperationsDB();

            db.SaveWorkoutCompletion(model);

            TempData["msg"] = "Workout marked as completed 💪";
            return RedirectToAction("UserProfile_Pageload", "UserProfile", new { id = model.User_Id });
        }

        public IActionResult WorkoutHistory(int userId)
        {
            OperationsDB db = new OperationsDB();
            var history = db.GetWorkoutHistory(userId);

            return View(history);
        }
    }
}
