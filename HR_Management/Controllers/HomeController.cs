using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using System.IO;

namespace HR_Management.Controllers
{

    public class HomeController : Controller
    {
        DbHR_ManagementEntities _context = new DbHR_ManagementEntities();
        DbHR_ManagementEntities1 _context1 = new DbHR_ManagementEntities1();
        public string deid = "";
        //private readonly CrudContext _context = new CrudContext();
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            //var listof = _context.TblEmployees.ToList();
            //return View(listof);
            //ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            //ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";
            ViewBag.CurrentSort = sortOrder;
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name" : "";
            ViewBag.EmailSortParm = String.IsNullOrEmpty(sortOrder) ? "mail" : "";
            ViewBag.DesignationSortParm = String.IsNullOrEmpty(sortOrder) ? "Designation" : "";
            ViewBag.EIdSortParm = String.IsNullOrEmpty(sortOrder) ? "EmployeeId" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "Date" : "Date";
            ViewBag.BySortParm = String.IsNullOrEmpty(sortOrder) ? "By" : "";
            var employees = from s in _context.TblEmployees
                            select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                employees = employees.Where(s => s.EmpName.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "EmployeeId":
                    employees = employees.OrderByDescending(s => s.EmpId);
                    break;
                case "name":
                    employees = employees.OrderByDescending(s => s.EmpName);
                    break;
                case "mail":
                    employees = employees.OrderByDescending(s => s.EmpEmail);
                    break;
                case "Date":
                    employees = employees.OrderBy(s => s.CreatedDate);
                    break;
                case "Designation":
                    employees = employees.OrderByDescending(s => s.EmpDesignation);
                    break;
                case "By":
                    employees = employees.OrderByDescending(s => s.CreatedBy);
                    break;
                default:
                    employees = employees.OrderBy(s => s.EmpId);
                    break;
            }
            int pageSize = 3;
            int pageNumber = (page ?? 1);
            return View(employees.ToPagedList(pageNumber, pageSize));
            //return View(employees.ToList());
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
        //public ActionResult Documents(int id)
        //{
        //    string sid = id.ToString();
        //    var data = _context1.TblEmpDetails.Where(x => x.EmpId == sid).FirstOrDefault();
        //    return View(data);
        //}
        /// <summary>
        /// /////////////
        //[HttpGet]
        //public ActionResult Documents(int id)
        //{
        //    var data = _context.TblEmployees.Where(x => x.Id == id).FirstOrDefault();
        //    ViewBag.eid = data;
        //    return View(data);
        //}
        //[HttpPost]
        public ActionResult Documents(string sortOrder, string currentFilter, string searchString, int? page, int id)
        {
            ViewBag.eid = id;
            deid = id.ToString();
            Session["EID"] = id.ToString();
            ViewBag.CurrentSort = sortOrder;
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "FileName" : "";
            //ViewBag.EmailSortParm = String.IsNullOrEmpty(sortOrder) ? "mail" : "";
            //ViewBag.DesignationSortParm = String.IsNullOrEmpty(sortOrder) ? "Designation" : "";
            //ViewBag.EIdSortParm = String.IsNullOrEmpty(sortOrder) ? "EmployeeId" : "";
            //ViewBag.DateSortParm = sortOrder == "Date" ? "Date" : "Date";
            //ViewBag.BySortParm = String.IsNullOrEmpty(sortOrder) ? "By" : "";
            var empdoc = from s in _context1.TblEmpDetails
                         select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                empdoc = empdoc.Where(s => s.FileName.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "FileName":
                    empdoc = empdoc.OrderByDescending(s => s.FileName);
                    break;
                case "FilePath":
                    empdoc = empdoc.OrderByDescending(s => s.FilePath);
                    break;
                default:
                    empdoc = empdoc.OrderBy(s => s.EmpId);
                    break;
            }
            int pageSize = 3;
            int pageNumber = (page ?? 1);
            return View(empdoc.ToPagedList(pageNumber, pageSize));
            //return View(employees.ToList());
        }
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult CreateDoc(int eid)
        {
            ViewBag.eid = eid.ToString();
            return View();
        }

        [HttpPost]
        public ActionResult CreateDoc(HttpPostedFileBase file, TblEmpDetail Model)
        {
            try
            {
                string[] permittedExtensions = { ".txt", ".pdf", ".doc", ".docx", ".jpg", ".jpeg", ".png", ".xls", ".xlsx" };

                //var ext = Path.GetExtension(file).ToLowerInvariant();
                var ext = Path.GetExtension(file.FileName).ToLowerInvariant();
                if (string.IsNullOrEmpty(ext) || !permittedExtensions.Contains(ext))
                {
                    ViewBag.Message = "File upload failed!! Upload only .txt, .pdf, .doc, .docx, .jpg, .jpeg, .png, .xls, xlsx file only.";
                    return View();
                }
                else
                {

                    if (file.ContentLength > 0)
                    {
                        
                        string _FileName = Path.GetFileName(file.FileName);
                        string _path = Path.Combine(Server.MapPath("~/EmployeeDocuments/EID_"+Session["EID"].ToString()), _FileName);
                        bool folderExists = Directory.Exists(Server.MapPath("~/EmployeeDocuments/EID_" + Session["EID"].ToString()));
                        if (!folderExists)
                            Directory.CreateDirectory(Server.MapPath("~/EmployeeDocuments/EID_" + Session["EID"].ToString()));
                        file.SaveAs(_path);
                        Model.FileName = _FileName;
                        Model.FilePath = _path;
                        Model.EmpId = Session["EID"].ToString();
                        Model.CreatedDate = DateTime.Today.ToString("dd-MM-yyyy");
                        Model.CreatedBy = "Admin";
                        _context1.TblEmpDetails.Add(Model);
                        //_context1.TblEmpDetails.Add(Model);
                        _context1.SaveChanges();
                        return RedirectToAction("Documents/" + Session["EID"].ToString());
                    }
                    ViewBag.Message = "File Uploaded Successfully!!";
                    return View();
                }
            }
            catch
            {
                ViewBag.Message = "File upload failed!!";
                return View();
            }
        }
    
        [HttpGet]
        public ActionResult EditDoc(int id)
        {
            var data = _context1.TblEmpDetails.Where(x => x.Id == id).FirstOrDefault();
            return View(data);
        }
        [HttpPost]
        public ActionResult EditDoc(HttpPostedFileBase file, TblEmpDetail Model)
        {
            //var data = _context1.TblEmpDetails.Where(x => x.Id == Model.Id).FirstOrDefault();
            //if (data != null)
            //{
            //    data.FileName = Model.FileName;
            //    data.FilePath = Model.FilePath;
            //    _context1.SaveChanges();
            //}

            //return RedirectToAction("Documents/"+Model.EmpId);
            try
            {
                string[] permittedExtensions = { ".txt", ".pdf", ".doc", ".docx", ".jpg", ".jpeg", ".png", ".xls", ".xlsx" };

                //var ext = Path.GetExtension(file).ToLowerInvariant();
                var ext = Path.GetExtension(file.FileName).ToLowerInvariant();
                if (string.IsNullOrEmpty(ext) || !permittedExtensions.Contains(ext))
                {
                    ViewBag.Message = "File upload failed!! Upload only .txt, .pdf, .doc, .docx, .jpg, .jpeg, .png, .xls, xlsx file only.";
                    return View();
                }
                else
                {

                    if (file.ContentLength > 0)
                    {

                        string _FileName = Path.GetFileName(file.FileName);
                        string _path = Path.Combine(Server.MapPath("~/EmployeeDocuments/EID_" + Session["EID"].ToString()), _FileName);
                        bool folderExists = Directory.Exists(Server.MapPath("~/EmployeeDocuments/EID_" + Session["EID"].ToString()));
                        if (!folderExists)
                            Directory.CreateDirectory(Server.MapPath("~/EmployeeDocuments/EID_" + Session["EID"].ToString()));
                        file.SaveAs(_path);
                        //Model.FileName = _FileName;
                        //Model.FilePath = _path;
                        //Model.EmpId = Session["EID"].ToString();
                        //Model.CreatedDate = DateTime.Today.ToString("dd-MM-yyyy");
                        //Model.CreatedBy = "Admin";
                        //_context1.TblEmpDetails.Add(Model);
                        ////_context1.TblEmpDetails.Add(Model);
                        //_context1.SaveChanges();
                        var data = _context1.TblEmpDetails.Where(x => x.Id == Model.Id).FirstOrDefault();
                        if (data != null)
                        {
                            data.FileName = _FileName;
                            data.FilePath = _path;
                            _context1.SaveChanges();
                        }
                        return RedirectToAction("Documents/" + Session["EID"].ToString());
                    }
                    ViewBag.Message = "File Uploaded Successfully!!";
                    return View();
                }
            }
            catch
            {
                ViewBag.Message = "File upload failed!!";
                return View();
            }
        }
        public ActionResult DeleteDoc(int id, int eid)
        {
            var data = _context1.TblEmpDetails.Where(x => x.Id == id).FirstOrDefault();
            _context1.TblEmpDetails.Remove(data);
            _context1.SaveChanges();
            ViewBag.Messsage = "Record Delete Successfully";
            return RedirectToAction("Documents/" + eid);
        }

        public FilePathResult GetFileFromDisk(string fileName)
        {
            return File(Server.MapPath("~/EmployeeDocuments/EID_" + Session["EID"].ToString()+"/"+fileName), "multipart/form-data", fileName);
        }
    }
}