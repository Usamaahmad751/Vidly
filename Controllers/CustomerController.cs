using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Web.Mvc;
using Vidly.ViewModel;
using Vidly.Models;
namespace Vidly.Controllers
{
    public class CustomerController : Controller
    {

        private ApplicationDbContext _context;
        public CustomerController() {

            _context = new ApplicationDbContext();
        }
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing); 
            
        }
        // GET: Customer
        // Form for New Customer


        public ActionResult CustomerForm() {

            var MemberShipType = _context.MemberShipType.ToList();
            var ViewModel = new CustomerFormViewModel {
                MemberShipTypes = MemberShipType
        };
            return View(ViewModel);
        }


        // Handle Create Customer


        [HttpPost]
        public ActionResult Save(Customer customer) {
            if (customer.Id == 0)
            {
                _context.Customers.Add(customer);
            }
            else
            {
                var CustomerInDb = _context.Customers.Single(c => c.Id == customer.Id);
                CustomerInDb.Name = customer.Name;
                CustomerInDb.BirthDate = customer.BirthDate;
                CustomerInDb.IsSubscribedToNewsLetter = customer.IsSubscribedToNewsLetter;

                CustomerInDb.MemberShipTypeId = customer.MemberShipTypeId;

            }
            try
            {

                _context.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {
                Console.WriteLine(e);
            }
          
                return RedirectToAction("Index", "Customer");
            
        }

        // Customer Home

        public ActionResult Index()
        {
            var Customer = _context.Customers.Include(c=> c.MemberShipType).ToList();
            return View(Customer);
        }




        // Customer Detail Function
        public ActionResult Details(int id)
        {
            var customer = _context.Customers.Include(c => c.MemberShipType ).SingleOrDefault(c => c.Id == id);
            if (customer == null)
            {

                return HttpNotFound();
            }
            return View(customer);
        }

        public ActionResult Edit(int id) {
            var Customer = _context.Customers.SingleOrDefault(c => c.Id == id);
            if (Customer == null)
            {
                return HttpNotFound();
            }
            var viewModel = new CustomerFormViewModel {
                Customer = Customer,
                MemberShipTypes = _context.MemberShipType.ToList(),
        };
            return View("CustomerForm", viewModel);
        }
    }
}