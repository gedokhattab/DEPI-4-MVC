using Day02.Models;
using Day02.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Day02.Controllers
{
    public class CourseController : Controller
    {
        IDepartmentRepository _departmentRepository;
        ICourseRepository _courseRepository;
        ITeacherRepository _teacherRepository;

        public CourseController(IDepartmentRepository departmentRepository, ICourseRepository courseRepository, ITeacherRepository teacherRepository)
        {
            _departmentRepository = departmentRepository;
            _courseRepository = courseRepository;
            _teacherRepository = teacherRepository;
        }

        // GET: /Course
        [HttpGet]
        public IActionResult Index(string name = "", int? deptId = null, int n = 0)
        {
            ViewData["SearchTerm"] = name;
            ViewData["SelectedDept"] = deptId;
            ViewData["Departments"] = new SelectList(_departmentRepository.GetAll(), "Id", "Name");

            var query = _courseRepository.GetAllWithLoading(); // Returns IQueryable
            query = _courseRepository.SearchByName(query, name);
            query = _courseRepository.FilterDepartments(query, deptId);

            int totalCount = query.Count();
            int totalPages = (int)Math.Ceiling(totalCount / 10.0);

            if (n >= totalPages && totalPages > 0)
                n = 0;

            var courses = _courseRepository.Paginate(query, n);

            ViewData["TotalPages"] = totalPages;
            ViewData["CurrentPage"] = n;

            return View(courses);
        }

        // GET: /Course/ShowDetails/1
        [HttpGet]
        public IActionResult ShowDetails(int id)
        {
            var course = _courseRepository.GetById(id);
            ViewBag.AvgDegree = _courseRepository.AvgDegree(id);
            return View("ShowDetails", course);
        }

        // GET: /Course/Create
        [HttpGet]
        public IActionResult Create()
        {
            ViewData["Departments"] = new SelectList(_departmentRepository.GetAll(), "Id", "Name");
            return View("Create");
        }

        [HttpPost]
        public IActionResult CreateCourse(Course course)
        {
            if (ModelState.IsValid)
            {
                _courseRepository.Add(course);
                _courseRepository.Save();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Departments"] = new SelectList(_departmentRepository.GetAll(), "Id", "Name");
            return View("Create", course);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            Course course = _courseRepository.GetById(id);
            ViewData["Departments"] = new SelectList(_departmentRepository.GetAll(), "Id", "Name");
            ViewBag.AllTeachers = _teacherRepository.GetAll();
            return View("Edit", course);
        }

        [HttpPost]
        public IActionResult EditCourse(Course newCourse, List<int> selectedTeacherIds)
        {
            Course course = _courseRepository.GetById(newCourse.Id);
            if (ModelState.IsValid)
            {
                course.Name = newCourse.Name;
                course.Degree = newCourse.Degree;
                course.MinDegree = newCourse.MinDegree;
                course.DepartmentId = newCourse.DepartmentId;

                _courseRepository.UpdateTeachers(course, selectedTeacherIds);
                _courseRepository.Save();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Departments"] = new SelectList(_departmentRepository.GetAll(), "Id", "Name");
            ViewBag.AllTeachers = _teacherRepository.GetAll();
            return View("Edit", course);
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            Course course = _courseRepository.GetById(id);
            return View("Delete", course);
        }

        [HttpPost]
        public IActionResult DeleteCourse(int id)
        {
            var course = _courseRepository.GetById(id);
            if (course != null)
            {
                _courseRepository.Delete(course);
                _courseRepository.Save();
            }
            return RedirectToAction(nameof(Index));
        }

    }
}
