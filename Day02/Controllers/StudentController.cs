using Day02.Models;
using Microsoft.AspNetCore.Mvc;

namespace Day02.Controllers
{
    public class StudentController : Controller
    {
        // GET: /Student
        public IActionResult Index()
        {
            return View();
        }

        // GET: /Student/ShowAll
        public IActionResult ShowAll()
        {
            StudentBL studentBL = new StudentBL();
            var Students = studentBL.GetAll();
            return View("ShowAll", Students);
        }

        // GET: /Student/ShowDetails/1
        public IActionResult ShowDetails(int id)
        {
            StudentBL studentBL = new StudentBL();
            var Student = studentBL.GetById(id);
            return View("ShowDetails", Student);
        }
    }
}
