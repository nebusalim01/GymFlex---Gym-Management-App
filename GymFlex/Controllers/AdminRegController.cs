using GymFlex.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;

namespace GymFlex.Controllers
{
    public class AdminRegController : Controller
    {
        public IActionResult AdminReg_Pageload()
        {
            return View();
        }
        public IActionResult AdminReg_Click(AdminReg objCls)
        {
            if (!ModelState.IsValid)
            {
                return View("AdminReg_Pageload", objCls);
            }
            try
            {
                OperationsDB objDB = new OperationsDB();
                int emailCount = objDB.getEmailCount(objCls);
                if (emailCount == 0)
                {
                    int regId = objDB.getMaxRegId();                    
                    int result = objDB.InsertAdmin(objCls, regId);
                    if (result == 1)
                    {
                        int loginResult = objDB.InsertLogin(objCls, regId);
                        if(loginResult ==1)
                        {
                            ViewBag.msg = "Registration Successful. Your Admin ID is " + regId;
                            return View("AdminReg_Pageload");
                        }
                        else
                        {
                            ViewBag.msg = "Registration Failed during login creation. Please try again.";
                        }
                    }
                    else
                    {
                        ViewBag.msg = "Registration Failed. Please try again.";
                    }
                }
                else
                {
                    ViewBag.msg = "Email already exists. Please use a different email.";
                }
                return View("AdminReg_Pageload");
            }
            catch (Exception ex)
            {
                ViewBag.msg = "An error occurred: " + ex.Message;
                return View("AdminReg_Pageload");
            }
        }
    }
}
