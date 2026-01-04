using GymFlex.Models;
using Microsoft.AspNetCore.Mvc;

namespace GymFlex.Controllers
{
    public class AdminProfileController : Controller
    {
        public IActionResult AdminProfile_Pageload(int id)
        {
            OperationsDB db = new OperationsDB();

            AdminProfileModel admin = db.GetAdminProfile(id);

            if (admin == null || admin.AdminId == 0)
            {
                ViewBag.msg = "Admin profile not found.";
                admin = new AdminProfileModel();
            }

            return View(admin);
        }
        public IActionResult ManageUsers()
        {
            OperationsDB db = new OperationsDB();

            var users = db.GetAllUsers();

            return View(users);
        }

    }
}
