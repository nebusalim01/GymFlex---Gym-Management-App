using GymFlex.Models;
using Microsoft.AspNetCore.Mvc;

namespace GymFlex.Controllers
{
    public class DietPlanController : Controller
    {
        public IActionResult AddDietPlan_PageLoad()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddDietPlan_Click(DietPlanModel obj)
        {
            if (!ModelState.IsValid)
                return View("AddDietPlan_PageLoad", obj);

            OperationsDB db = new OperationsDB();
            int res = db.InsertDietPlan(obj);

            if (res == 1)
            {
                ViewBag.msg = "Diet Plan Added Successfully";
                ModelState.Clear();
                return View("AddDietPlan_PageLoad");
            }
            else
            {
                ViewBag.msg = "Failed to insert diet plan";
                return View("AddDietPlan_PageLoad", obj);
            }
        }
        public IActionResult ViewDietPlans_PageLoad()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ViewDietPlans_Click(string goal)
        {
            OperationsDB db = new OperationsDB();
            var list = db.GetDietPlansByGoal(goal);

            ViewBag.SelectedGoal = goal;

            return View("ViewDietPlans_List", list);
        }

    }
}
