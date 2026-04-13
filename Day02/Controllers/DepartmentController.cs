using Microsoft.AspNetCore.Mvc;
using Day02.Models;
using Day02.ViewModels;
using Day02.Repositories;

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
                Students = Department.Students.Where(S => S.Age>25).ToList(),
                Status = Department.Students.Count > 50 ? "Main" : "Branch"
            };
            return View("ShowDetails", DeptVM);
        }

        // GET: /Department/Add
        [HttpGet]
        public IActionResult Add()
        {
            return View("Add");
        }

        // POST: /Department/SaveAdd?Name=CS&ManagerName=Ali
        [HttpPost]
        public IActionResult SaveAdd(Department dept)
        {
            if (dept.Name != null)
            {
                _departmentRepository.Add(dept);
                _departmentRepository.Save();
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction("Add", dept);
        }

    }
}
