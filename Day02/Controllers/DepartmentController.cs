using Day02.Models;
using Day02.Repositories;
using Day02.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Day02.Controllers
{
    public class DepartmentController : Controller
    {
        IDepartmentRepository _departmentRepository;

        public DepartmentController(IDepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository;   
        }

        // GET: /Department/Index
        [HttpGet]
        public IActionResult Index()
        {
            var Departments = _departmentRepository.GetAllWithLoading();
            return View("Index", Departments);
        }

        // GET: /Department/ShowDetails/1
        // GET: /Department/ShowDetails?id=1
        [HttpGet]
        public IActionResult ShowDetails(int id)
        {
            var Department = _departmentRepository.GetById(id);
            DepartmentViewModel DeptVM= new DepartmentViewModel()
            {
                DeptName = Department.Name,
                Students = Department.Students.ToList(),
                Status = Department.Students.Count > 50 ? "Main" : "Branch"
            };
            return View("ShowDetails", DeptVM);
        }

        // GET: /Department/Create
        [HttpGet]
        public IActionResult Create()
        {
            return View("Create");
        }

        // POST: /Department/CreateDep
        [HttpPost]
        public IActionResult CreateDep(Department department)
        {
            if (ModelState.IsValid)
            {
                _departmentRepository.Add(department);
                _departmentRepository.Save();
                return RedirectToAction(nameof(Index));
            }
            return View("Create", department);
        }

        // GET: /Department/Edit/1
        [HttpGet]
        public IActionResult Edit(int id)
        {
            Department department = _departmentRepository.GetById(id);
            return View("Edit", department);
        }

        // POST: /Department/EditDep
        [HttpPost]
        public IActionResult EditDep(Department NewDept)
        {
            Department department = _departmentRepository.GetById(NewDept.Id);
            if (ModelState.IsValid)
            {
                department.Name = NewDept.Name;
                department.ManagerName = NewDept.ManagerName;
                _departmentRepository.Save();
                return RedirectToAction(nameof(Index));
            }
            return View("Edit", department);
        }

    }
}
