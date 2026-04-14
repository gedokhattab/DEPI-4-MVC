using Day02.Models;
using Day02.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Day02.Controllers
{
    public class StudentController : Controller
    {
        IStudentRepository _studentRepository;
        IDepartmentRepository _departmentRepository;
        ICourseRepository _courseRepository;

        public StudentController(IStudentRepository studentRepository, IDepartmentRepository departmentRepository, ICourseRepository courseRepository)
        {
            _studentRepository = studentRepository;
            _departmentRepository = departmentRepository;
            _courseRepository = courseRepository;
        }

        // GET: /Student
        [HttpGet]
        public IActionResult Index(string name = "", int? deptId = null, int n = 0)
        {
            ViewData["SearchTerm"] = name;
            ViewData["SelectedDept"] = deptId;
            ViewData["Departments"] = new SelectList(_departmentRepository.GetAll(), "Id", "Name");

            var query = _studentRepository.GetAllWithLoading(); // Returns IQueryable
            query = _studentRepository.SearchByName(query, name);
            query = _studentRepository.FilterDepartments(query, deptId);

            int totalCount = query.Count();
            int totalPages = (int)Math.Ceiling(totalCount / 10.0);

            if (n >= totalPages && totalPages > 0) 
                n = 0;
            
            var students = _studentRepository.Paginate(query, n);

            ViewData["TotalPages"] = totalPages;
            ViewData["CurrentPage"] = n;

            return View(students);
        }

        // GET: /Student/ShowDetails/1
        [HttpGet]
        public IActionResult ShowDetails(int id)
        {
            var Student = _studentRepository.GetById(id);
            return View("ShowDetails", Student);
        }

        // GET: /Student/ShowStuCrs?StudentId=1&CourseId=2
        [HttpGet]
        public IActionResult ShowStuCrs(int StudentId, int CourseId)
        {
            StudentCourseResult result = _studentRepository.GetStuCrs(StudentId, CourseId);
            ViewBag.GradeBadgeClass = result.Grade >= result.Course.MinDegree ? "badge-success" : "badge-danger";
            return View("ShowStuCrs", result);
        }

        // GET: /Student/Create
        [HttpGet]
        public IActionResult Create()
        {
            ViewData["Departments"] = new SelectList(_departmentRepository.GetAll(), "Id", "Name");
            return View("Create");
        }

        [HttpPost]
        public IActionResult CreateStud(Student student)
        {
            if (ModelState.IsValid)
            {
                _studentRepository.Add(student);
                _studentRepository.Save();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Departments"] = new SelectList(_departmentRepository.GetAll(), "Id", "Name");
            return View("Create", student);
        }

        [HttpPost]
        public IActionResult Edit(int id)
        {
            Student student = _studentRepository.GetById(id);
            ViewData["Departments"] = new SelectList(_departmentRepository.GetAll(), "Id", "Name");
            return View("Edit", student);
        }

        [HttpPost]
        public IActionResult EditStud(Student NewStud)
        {
            Student student = _studentRepository.GetById(NewStud.Id);
            if (ModelState.IsValid)
            {
                student.Name = NewStud.Name;
                student.Age = NewStud.Age;
                student.DepartmentId = NewStud.DepartmentId;
                _studentRepository.Save();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Departments"] = new SelectList(_departmentRepository.GetAll(), "Id", "Name");
            return View("Edit", student);
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            Student student = _studentRepository.GetById(id);
            return View("Delete", student);
        }

        [HttpPost]
        public IActionResult DeleteStud(int id)
        {
            var student = _studentRepository.GetById(id);
            if (student != null)
            {
                _studentRepository.Delete(student);
                _studentRepository.Save();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
