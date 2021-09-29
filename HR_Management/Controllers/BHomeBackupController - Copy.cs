using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HR_Management.Controllers
{
    public class BHomeControllerBackup : Controller
    {
        DbHR_ManagementEntities _context = new DbHR_ManagementEntities();
        //private readonly CrudContext _context = new CrudContext();
        public ActionResult Index()
        {
            var listof = _context.TblEmployees.ToList();
            return View(listof);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(TblEmployee model)
        {
            _context.TblEmployees.Add(model);
            _context.SaveChanges();
            ViewBag.Message = "Data Insert Successfully";
            return View();
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var data = _context.TblEmployees.Where(x => x.Id == id).FirstOrDefault();
            return View(data);
        }
        [HttpPost]
        public ActionResult Edit(TblEmployee Model)
        {
            var data = _context.TblEmployees.Where(x => x.Id == Model.Id).FirstOrDefault();
            if (data != null)
            {
                data.EmpId = Model.EmpId;
                data.EmpEmail = Model.EmpEmail;
                data.EmpName = Model.EmpName;
                data.EmpDesignation = Model.EmpDesignation;
                _context.SaveChanges();
            }

            return RedirectToAction("index");
        }
        public ActionResult Delete(int id)
        {
            var data = _context.TblEmployees.Where(x => x.Id == id).FirstOrDefault();
            _context.TblEmployees.Remove(data);
            _context.SaveChanges();
            ViewBag.Messsage = "Record Delete Successfully";
            return RedirectToAction("index");
        }
        public ActionResult Detail(int id)
        {
            var data = _context.TblEmployees.Where(x => x.Id == id).FirstOrDefault();
            return View(data);
        }
    }
}