using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BOL;
using DAL;

namespace ManageCustomers.Controllers
{
    public class LoginsController : Controller
    {
        // GET: Logins
        public ActionResult Login()
        {
            return View();
        }


        // POST: Logins
        [HttpPost]
        [ActionName("Login")]
        public ActionResult Login_Check(Login collection)
        {
            if (ModelState.IsValid)
            {
                Session["user"] = collection.Email;
                LoginBuissnessLayer buiss = new LoginBuissnessLayer();
                Customer isTrue = buiss.customers(collection);
                Session["userId"] = isTrue.CustomerID;

                try
                {
                    int id = isTrue.CustomerID;
                    if (isTrue.IsValidUser != 0)
                    {
                        if (isTrue.RoleId == 2)
                        {
                            return RedirectToAction("GetAllCustomers");
                        }
                        else
                        {
                            return RedirectToAction("Details" + "/" + id);
                        }
                    }
                    else
                    {
                        return RedirectToAction("ErrorPage", "Logins", null);
                    }
                }
                catch
                {
                    return RedirectToAction("WrongDetailsMessage", "Logins", null);
                }
            }
            return View();
        }


        // GET: Logins/Details/5

        public ActionResult Details(int id)
        {
            try
            {
                int isValid = Convert.ToInt32(Session["userId"].ToString());
                if ((Session["user"] != null) && (isValid == id))
                {
                    CustomerBuissnessLayer personContext = new CustomerBuissnessLayer();
                    Customer customer = personContext.customers(id);
                    return View(customer);
                }
                return RedirectToAction("DetailsMessageOfLogin", "Logins", null);
            }
            catch
            {
                return RedirectToAction("DetailsMessageOfLogin", "Logins", null);
            }

        }


        // GET: Logins/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Logins/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Logins/Edit/5
        [HttpGet]
        public ActionResult Edit(int id)
        {
            try
            {
                int isValid = Convert.ToInt32(Session["userId"].ToString());

                if ((Session["user"] != null) && (isValid == id))
                {
                    CustomerBuissnessLayer personBuissnessLayer = new CustomerBuissnessLayer();
                    Customer person = personBuissnessLayer.persons.Single(per => per.CustomerID == id);
                    return View(person);
                }
                return RedirectToAction("DetailsMessageOfLogin", "Logins", null);
            }
            catch
            {
                return RedirectToAction("DetailsMessageOfLogin", "Logins", null);
            }
        }


        // POST: Logins/Edit/5
        [HttpPost]
        [ActionName("Edit")]
        public ActionResult Edit_Post(Customer cust)
        {
            if (Session["user"] != null)
            {
                CustomerBuissnessLayer personBuissnessLayer = new CustomerBuissnessLayer();
                personBuissnessLayer.UpdatePerson(cust);
                return RedirectToAction("Details" + "/" + cust.CustomerID);
            }
            return RedirectToAction("DetailsMessageOfLogin", "Logins", null);
        }

        // GET: Logins/Delete/5

        public ActionResult Delete(int id)
        {
            if (Session["user"] != null)
            {
                CustomerBuissnessLayer personBuissnessLayer = new CustomerBuissnessLayer();
                personBuissnessLayer.DeleteCustomer(id);
                return RedirectToAction("DeleteMessage", "Logins", null);
            }
            //Session.RemoveAll();
            return RedirectToAction("DetailsMessageOfLogin", "Logins", null);

        }

        // POST: Logins/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        // POST: Logins/GetAllCustomers
        public ActionResult GetAllCustomers()
        {
            if (Session["user"] != null)
            {
                CustomerBuissnessLayer personBuissnessLayer = new CustomerBuissnessLayer();
                List<Customer> customer = personBuissnessLayer.persons.ToList();
                return View(customer);
            }
            return RedirectToAction("DetailsMessageOfLogin", "Logins", null);
        }

        public ActionResult Deactivate(int id)
        {
            if (Session["user"] != null)
            {
                CustomerBuissnessLayer personBuissnessLayer = new CustomerBuissnessLayer();
                personBuissnessLayer.DeactivateCustomer(id);
                return RedirectToAction("DeactivateMessage", "Logins", null);
            }
            return RedirectToAction("DetailsMessageOfLogin", "Logins", null);
        }

        public ActionResult Logout()
        {
            Session.RemoveAll();
            return RedirectToAction("Login");
        }
        public ActionResult ErrorPage()
        {
            return View();
        }


        public ActionResult WrongDetailsMessage()
        {
            return View();
        }

        public ActionResult DeleteMessage()
        {
            return View();
        }


        public ActionResult DeactivateMessage()
        {
            return View();
        }


        public ActionResult DetailsMessageOfLogin()
        {
            return View();
        }

    }
}
