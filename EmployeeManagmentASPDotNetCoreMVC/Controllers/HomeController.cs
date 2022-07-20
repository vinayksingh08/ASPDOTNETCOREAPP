using EmployeeManagmentASPDotNetCoreMVC.Models;
using EmployeeManagmentASPDotNetCoreMVC.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using Microsoft.AspNetCore.Authorization;

namespace EmployeeManagmentASPDotNetCoreMVC.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly IEmployeeRepository _employeeRepository;
        //private readonly IHostingEnvironment HostingEnvironment;
        private readonly IWebHostEnvironment HostingEnvironment;
        //constructor injection
        public HomeController(IEmployeeRepository employeeRepository, IWebHostEnvironment hostingEnvinment)
        {
            _employeeRepository = employeeRepository;
            this.HostingEnvironment = hostingEnvinment;
        }
        
        [Route("")]
        [Route("Home")]
        [Route("Home/Index")]
        [HttpGet]
        [AllowAnonymous]
        public ViewResult Index()
        {
            var model= _employeeRepository.GetAllEmployee();
            return View(model);
        }

        //public JsonResult Details()
        //{
        //    Employee model = _employeeRepository.GetEmployee(1);
        //    return Json(model);
        //}
        //public ObjectResult Details()
        //{
        //    Employee model = _employeeRepository.GetEmployee(1);
        //    return new ObjectResult(model);
        //}
       
        [HttpGet]
        [AllowAnonymous]
        public ViewResult Details(int? id)
        {
            //Employee model = _employeeRepository.GetEmployee(1);
            //ViewData["Emp"] = model;
            //ViewData["PageTitle"] = "Employee Details";

            //ViewBag.Emp = model;
            //ViewBag.PageTitle = "Employee Details";
            //throw new Exception("fgf"); 

            Employee Emp = _employeeRepository.GetEmployee(id.Value);
                if(Emp==null)
            {

                Response.StatusCode = 400;
                    return View("EmployeeNotFound",id.Value);
            }
              

            HomeDetailsViewModel homeDetailsViewModel = new HomeDetailsViewModel()
            {
                Employee = Emp,
                PageTitle= "Employee Details"
            };

            return View(homeDetailsViewModel);

            //return View(test); test is view name
            // return View("MyView/test.cshtml");  define absolute path myview is folder 
            //return View("../test/test"); relative path test is folder in view folder
        }
        [HttpGet]
       
        public ViewResult Create()
        {


            return View();
        }
        //[HttpPost]
        //public IActionResult Create(Employee emp)
        //{
        //    if(ModelState.IsValid)
        //    {
        //        Employee newemp = _employeeRepository.AddEmployee(emp);

        //        //return RedirectToAction("Details", new { id = newemp.ID });

        //    }
        //    return View();

        //}


        [HttpPost]
       
        public IActionResult Create(EmployeeCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
             
                string UniqueFileName = Process_UplodedFile(model);
                Employee newemp = new Employee
                {
                    Name = model.Name,
                    Email = model.Email,
                    Department = model.Department,
                    PhotoPath = UniqueFileName


                };
                _employeeRepository.AddEmployee(newemp);


                return RedirectToAction("Details", new { id = newemp.ID });

            }
            return View();

        }
        [HttpGet]
        //[Authorize]
        public ViewResult Edit(int id)
        {
            Employee employee = _employeeRepository.GetEmployee(id);
            EmployeeEditViewModel employeeEditViewModel = new EmployeeEditViewModel
            {
                Id = employee.ID,
                Name = employee.Name,
                Email = employee.Email,
                Department = employee.Department,
                ExistingPhotoPath = employee.PhotoPath
            };
            return View(employeeEditViewModel);
        }
        [HttpPost]
        //[Authorize]
        public IActionResult Edit(EmployeeEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                Employee Emp = _employeeRepository.GetEmployee(model.Id);
                Emp.Name = model.Name;
                Emp.Email = model.Email;
                Emp.Department = model.Department;
                Emp.PhotoPath = model.ExistingPhotoPath;
                if (model.Photo != null)
                {
                    if(model.ExistingPhotoPath!=null)
                    {
                       string filePath= Path.Combine(HostingEnvironment.WebRootPath, "Images", model.ExistingPhotoPath);
                        System.IO.File.Delete(filePath);
                    }
                    Emp.PhotoPath = Process_UplodedFile(model);
                }
                
               
                _employeeRepository.UpdateEmployee(Emp);


                return RedirectToAction("Index");

            }
            return View();

        }

        private string Process_UplodedFile(EmployeeCreateViewModel model)
        {
            string UniqueFileName = null;
            if (model.Photo != null)
            {
                string PhotoPath = Path.Combine(HostingEnvironment.WebRootPath, "Images");
                UniqueFileName = Guid.NewGuid() + "_" + model.Photo.FileName;
                string filePath = Path.Combine(PhotoPath, UniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.Photo.CopyTo(fileStream);

                }
                   
            }

            return UniqueFileName;
        }
        //private readonly ILogger<HomeController> _logger;

        //public HomeController(ILogger<HomeController> logger)
        //{
        //    _logger = logger;
        //}

        //public IActionResult Index()
        //{
        //    return View();
        //}

        //public IActionResult Privacy()
        //{
        //    return View();
        //}

        //[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        //public IActionResult Error()
        //{
        //    return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        //}
    }
}
