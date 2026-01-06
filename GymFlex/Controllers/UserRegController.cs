using GymFlex.Models;
using Microsoft.AspNetCore.Mvc;

namespace GymFlex.Controllers
{
    public class UserRegController : Controller
    {
        public IActionResult UserReg_Pageload()
        {
            return View();
        }

        public IActionResult UserReg_Click(UserReg obj)
        {
            if (!ModelState.IsValid)
                return View("UserReg_Pageload", obj);

            OperationsDB db = new OperationsDB();

            // check email duplicate
            int emailCount = db.getEmailCount(new AdminReg { Email = obj.Email });

            if (emailCount > 0)
            {
                ViewBag.msg = "Email already exists";
                return View("UserReg_Pageload");
            }

            int regId = db.getMaxRegId();

            // BMI calculation
            obj.BMI = db.CalculateBMI(obj.Height, obj.Weight);

            int res1 = db.InsertUser(obj, regId);

            if (res1 == 1)
            {
                db.InsertUserLogin(obj, regId);
                db.InsertUserPlan(regId, obj.BMI, obj.Goal);

                ViewBag.msg = "User Registered Successfully. Your User ID = " + regId;
                return View("UserReg_Pageload");
            }
            else
            {
                ViewBag.msg = "Registration failed";
                return View("UserReg_Pageload");
            }
        }
    }
}
