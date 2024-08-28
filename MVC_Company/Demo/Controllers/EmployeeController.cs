using AutoMapper;
using Demo.BLL.Interfaces;
using Demo.BLL.Repositories;
using Demo.DAL.Models;
using Demo.PL.Helper;
using Demo.PL.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MVC_Demo.BLL.Interfaces;
using MVC_Demo.DAL.Models;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Demo.PL.Controllers
{
	//[AllowAnonymous]
	[Authorize]
	public class EmployeeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        //private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;

        //private readonly IDepartmentRepository _departmentRepository;

        public EmployeeController(/*IEmployeeRepository employeeRepository*/ IUnitOfWork unitOfWork,IMapper mapper /*,IDepartmentRepository departmentRepository*/)  // Ask CLR Create Object from IEmployeeRepository
        {
            //this._employeeRepository = employeeRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            //_departmentRepository = departmentRepository;
        }

        // /Employee/Index
        public async Task<IActionResult> Index(string SearchInput)
        {

            var employees = Enumerable.Empty<Employee>();


            // 1. Add
            // 2. Update
            // 3. Delete

            //_unitOfWork.Complete();


            if (string.IsNullOrEmpty(SearchInput))
            {
                //employees = _employeeRepository.GetAll();
                employees = await _unitOfWork.EmployeeRepository.GetAll();

            }
            else
            {
                //employees = _employeeRepository.GetByName(SearchInput.ToLower());
                employees = await _unitOfWork.EmployeeRepository.GetByName(SearchInput.ToLower());

            }

            var result = _mapper.Map<IEnumerable<EmployeeViewModel>>(employees);


            // view dictionary : transfer extra information from action to view(one way) (key,value)

            // 1. ViewData : property inherted from controller class, dictionary

            //ViewData["Message"] = "Hello ViewData";


            // 2. ViewBagproperty : property inherted from controller class, dynamic

            ViewBag.Message = "Hello ViewBag";

            // 3. TempData : property inherted from controller class, like view data 
            // transfer information from request to another 




            return View(result);
        }



        // /Employee/Create
        [HttpGet]
        public IActionResult Create()
        {
            //ViewData["Departments"] = _unitOfWork.DepartmentRepository.GetAll();          //all Departments

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(EmployeeViewModel model)
        {

            //Employee employee = new Employee()
            //{
            //    Id = model.Id,
            //    Name = model.Name,
            //    HiringDate = model.HiringDate,
            //    Salary = model.Salary,
            //    Address = model.Address,
            //    Phone = model.Phone,
            //    DateOfCreation = model.DateOfCreation,
            //    DepartmentId = model.DepartmentId,
            //    Department = model.Department,
            //    IsDeleted = model.IsDeleted
            //};



            if (ModelState.IsValid)  // Server Side Validation
            {

                model.ImageName = DocumentSettings.UploadFile(model.Image, "images");
                
                
                var employee = _mapper.Map<Employee>(model);

                //int Count = _employeeRepository.Add(model);

                //int Count = _unitOfWork.EmployeeRepository.Add(model);
                _unitOfWork.EmployeeRepository.Add(employee);
                var Count = await _unitOfWork.Complete();

                if (Count > 0)
                {
                    TempData["Message"] = "Employee Added !!";

                }
                else
                {
                    TempData["Message"] = "Employee Not Added !!";
                }
                
                return RedirectToAction("Index");
            }

            return View();
        }


        public async Task<IActionResult> Details(int? id, string ViewName = "Details")
        {
            if (id is null)
                return BadRequest();   //400 ERROR

            //var employee = _employeeRepository.Get(id.Value);
            var employee = await _unitOfWork.EmployeeRepository.Get(id.Value);


            var employeeViewModel = _mapper.Map<EmployeeViewModel>(employee);

            if (employee is null)
                return NotFound();  //404 

            return View(ViewName, employeeViewModel);
        }


        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            //ViewData["Departments"] = _departmentRepository.GetAll();          //all Departments


            return await Details(id, "Edit");
        }
        


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromRoute] int? id, EmployeeViewModel model)
        {
            if (id != model.Id)
                return BadRequest();  //400

            if(model.ImageName is not null)
            {
                DocumentSettings.DeleteFile(model.ImageName, "images");
            }
            model.ImageName = DocumentSettings.UploadFile(model.Image, "images");



            if (ModelState.IsValid)   // Server Side Validation
            {
                 var employee = _mapper.Map<Employee>(model);
                //var count = _employeeRepository.Update(employee);
                
                //var count = _unitOfWork.EmployeeRepository.Update(employee);
                _unitOfWork.EmployeeRepository.Update(employee);
                var count = await _unitOfWork.Complete();


                if (count > 0)
                {
                    return RedirectToAction("Index");
                }
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            return await Details(id, "Delete");
        }
       


        [HttpPost]
        public async Task<IActionResult> Delete([FromRoute] int? id, EmployeeViewModel model)
        {
            if (id != model.Id)
                return BadRequest();  //400



            if (ModelState.IsValid)   // Server Side Validation
            {
                var employee = _mapper.Map<Employee>(model);
                //  var count = _employeeRepository.Delete(employee);
                //var count = _unitOfWork.EmployeeRepository.Delete(employee);
                _unitOfWork.EmployeeRepository.Delete(employee);
                var count = await _unitOfWork.Complete();


                if (count > 0)
                {
                    DocumentSettings.DeleteFile(model.ImageName, "images");

                    return RedirectToAction("Index");
                }
            }

            return View(model);
        }
    }


}

