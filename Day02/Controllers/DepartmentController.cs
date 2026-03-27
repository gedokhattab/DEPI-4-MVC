using Microsoft.AspNetCore.Mvc;
using Day02.Models;
using Day02.ViewModels;

namespace Day02.Controllers
{
    public class DepartmentController : Controller
    {
        DepartmentBL departmentBL = new DepartmentBL();

        // GET: /Department/Index
        [HttpGet]
        public IActionResult Index()
        {
            var Departments = departmentBL.GetAll();
            return View("Index", Departments);
        }

        // GET: /Department/ShowDetails/1
        // GET: /Department/ShowDetails?id=1
        [HttpGet]
        public IActionResult ShowDetails(int id)
        {
            var Department = departmentBL.GetById(id);
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
                departmentBL.Add(dept);
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction("Add", dept);
        }

    }
}
