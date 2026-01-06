using GymFlex.Models;
using Microsoft.AspNetCore.Mvc;

namespace GymFlex.Controllers
{
    public class ChangePasswordController : Controller
    {
        public IActionResult ChangePassword_PageLoad()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ChangePassword_Click(ChangePasswordModel obj)
        {
            if (!ModelState.IsValid)
                return View("ChangePassword_PageLoad", obj);

            OperationsDB db = new OperationsDB();

            // 1️⃣ verify old password
            int count = db.CheckOldPassword(obj.Email!, obj.OldPassword!);

            if (count != 1)
            {
                ViewBag.msg = "Old password is incorrect.";
                return View("ChangePassword_PageLoad", obj);
            }

            // 2️⃣ update password
            int res = db.UpdatePassword(obj.Email!, obj.NewPassword!);

            if (res == 1)
            {
                ViewBag.msg = "Password changed successfully.";
                ModelState.Clear();
                return View("ChangePassword_PageLoad");
            }
            else
            {
                ViewBag.msg = "Failed to update password.";
                return View("ChangePassword_PageLoad", obj);
            }
        }
    }
}
