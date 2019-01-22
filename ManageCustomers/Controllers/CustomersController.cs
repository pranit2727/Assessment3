using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BOL;
using DAL;


namespace ManageCustomers.Controllers
{
    public class CustomersController : Controller
    {

        // GET: Customers/Create
        [HttpGet]
        [ActionName("Create")]
        public ActionResult Register_Get()
        {
            return View();
        }

        
        // POST: Customers/Create
        [HttpPost]
        [ActionName("Create")]
        public ActionResult Register_Post(Customer collection)
        {
            if (ModelState.IsValid)
            {
                //Session["Id"] = collection.CustomerID;
                CustomerBuissnessLayer buiss = new CustomerBuissnessLayer();
                buiss.AddCustomers(collection);
                return RedirectToAction("Login", "Logins", null);
            }
            return View();
        }
       
    }
}
