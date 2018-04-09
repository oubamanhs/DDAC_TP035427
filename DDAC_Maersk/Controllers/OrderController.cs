using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DDAC_Maersk.Models;
using DDAC_Maersk.ViewModel;

namespace DDAC_Maersk.Controllers
{
    public class OrderController : Controller
    {
        // GET: Customer
        private ApplicationDbContext _context;

        public OrderController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }


        // GET: Order
        public ActionResult Index()
        {
            return View();
        }

        //Select schedule in booking view
        public ActionResult SelectSchedule()
        {
            var schedule = _context.Schedules.ToList();
            return View(schedule);
        }

        //Select Ship in booking view
        public ActionResult SelectShip(int Scheduleid)
        {
            var schedule = _context.Schedules.SingleOrDefault(s => s.ScheduleID == Scheduleid);
            var shipList = _context.Ships.ToList();

            var viewModel = new SSCViewModel
            {
                Schedule = schedule,
                Ships = shipList
            };

            return View(viewModel);
        }

        //Select Customer in booking view
        public ActionResult SelectCustomer(int Scheduleid, int Shipid)
        {
            var schedule = _context.Schedules.SingleOrDefault(s => s.ScheduleID == Scheduleid);
            var ship = _context.Ships.SingleOrDefault(s => s.ShipId == Shipid);
            var CustomerList = _context.Customers.ToList();

            var viewModel = new SSCViewModel
            {
                Schedule = schedule,
                Ship = ship,
                Customers = CustomerList

            };

            return View(viewModel);
        }


        //Select Container in booking view
        public ActionResult SelectContainer(int Shipid, int Scheduleid, int Customerid)
        {
            var schedule = _context.Schedules.SingleOrDefault(s => s.ScheduleID == Scheduleid);
            var Ship = _context.Ships.SingleOrDefault(s => s.ShipId == Shipid);
            var customer = _context.Customers.SingleOrDefault(c => c.CustomerID == Customerid);
            var ContainerList = _context.Containers.ToList();

            var viewModel = new SSCViewModel
            {
                Schedule = schedule,
                Ship = Ship,
                Customer = customer,
                Containers = ContainerList

            };

            return View(viewModel);
        }

        public ActionResult CreateBooking(SSCViewModel sscvm)
        {
            var order = new Order()
            {
                ScheduleID = sscvm.Schedule.ScheduleID,
                ShipId = sscvm.Ship.ShipId,
                CustomerID = sscvm.Customer.CustomerID
            };

            _context.Orders.Add(order);
            _context.SaveChanges();

            var container = new Container()
            {
                ContainerID = sscvm.Container.ContainerID,
                TypeOfContainer = sscvm.Container.TypeOfContainer,
                WeightofContainer = sscvm.Container.WeightofContainer,

                OrderID = sscvm.Order.OrderID
            };

            _context.Containers.Add(container);
            _context.SaveChanges();

            //var orderList = _context.Containers.Include(o => o.)

            //return View(orderList);
            return View();
        }
    }
}