using GymFlex.Models;
using Microsoft.AspNetCore.Mvc;

namespace GymFlex.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Login_PageLoad()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login_Click(LoginModel obj)
        {
            if (!ModelState.IsValid)
                return View("Login_PageLoad", obj);

            OperationsDB db = new OperationsDB();

            // 1️⃣ check count first
            int count = db.CheckLoginCount(obj.Email!, obj.Password!);

            if (count != 1)   // must be EXACTLY 1
            {
                ViewBag.msg = "Invalid Email or Password";
                return View("Login_PageLoad");
            }

            // 2️⃣ get login type
            string role = db.GetLoginType(obj.Email!, obj.Password!);

            // 3️⃣ get Reg_Id
            int regId = db.GetRegId(obj.Email!, obj.Password!);

            // extra safety check
            if (regId == 0 || string.IsNullOrEmpty(role))
            {
                ViewBag.msg = "Invalid Email or Password";
                return View("Login_PageLoad");
            }

            TempData["Reg_Id"] = regId;
            TempData["Login_Type"] = role;

            if (role == "Admin")
            {
                return RedirectToAction("AdminProfile_Pageload", "AdminProfile", new { id = regId });
            }
            else if (role == "User")
            {
                return RedirectToAction("UserProfile_PageLoad", "UserProfile", new { id = regId });
            }
            else
            {
                ViewBag.msg = "Invalid Email or Password";
                return View("Login_PageLoad");
            }
        }

    }
}
