using GymFlex.Models;
using Microsoft.AspNetCore.Mvc;

namespace GymFlex.Controllers
{
    public class AssignPlanController : Controller
    {
        public IActionResult AssignPlan_PageLoad()
        {
            OperationsDB db = new OperationsDB();

            ViewBag.Users = db.GetUsers();
            ViewBag.WorkoutPlans = db.GetWorkoutPlans_DT();
            ViewBag.DietPlans = db.GetDietPlans();

            return View();
        }
        [HttpPost]
        public IActionResult AssignPlan_Click(AssignPlanModel obj)
        {
            if (!ModelState.IsValid)
            {
                return AssignPlan_PageLoad();
            }

            OperationsDB db = new OperationsDB();
            int res = db.AssignPlanToUser(obj);

            if (res == 1)
            {
                ViewBag.msg = "Plan Assigned Successfully!";
            }
            else
            {
                ViewBag.msg = "Assignment Failed!";
            }

            return AssignPlan_PageLoad();
        }
    }
}
