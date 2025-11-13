using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using MvcStudentApp.Models;

namespace MvcStudentApp.Controllers
{
    public class StudentController : Controller
    {
        private readonly StudentDbContext _context;

        // ✅ Dependency Injection: context is automatically provided by ASP.NET Core
        public StudentController(StudentDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var students = _context.Students.ToList();
            return View(students);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Student student)
        {
            _context.Students.Add(student);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            var student = _context.Students.Find(id);

            if (student == null)
            {
                return NotFound(); // ✅ Handle missing record safely
            }
            return View(student);
        }

        [HttpPost]
        public ActionResult Edit(Student student)
        {
            // ✅ Use EF Core’s EntityState
            _context.Entry(student).State = EntityState.Modified;
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            var student = _context.Students.Find(id);
            if (student == null)
            {
                return NotFound();
            }
            return View(student);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            var student = _context.Students.Find(id);
            _context.Students.Remove(student);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
