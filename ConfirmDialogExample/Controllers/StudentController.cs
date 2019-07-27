using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ConfirmDialogExample.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ConfirmDialogExample.Controllers
{
    public class StudentController : Controller
    {

        //static student list with samples
        static IList<Student> StudentList = new List<Student>() {
            new Student(){Id=1, Firstname="John", Lastname="Galt"},
            new Student(){Id=2, Firstname="Jane", Lastname="Malt"},
            new Student(){Id=3, Firstname="Mary", Lastname="Salt"}
        };

        // GET: Student
        public ActionResult Index()
        {
            return View(StudentList);
        }

        // GET: Student/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Student/Create
        public ActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// This action creates a Student and insert into StudentList.
        /// It is called by the ajax requests which can evaluate return values.
        /// 
        /// </summary>
        /// <param name="student"></param>
        /// <returns>Json object which keeps 3 different values.
        ///     "state" (mandatory) 0 is sent when success occurs, other values sent in failures.
        ///     "msg" (mandatory) Description of the result. Message to the user.
        ///     "redirectUrlInSuccess" (optional) This is used to redirect the user to another page after 
        ///     successful operations. If null or empty, user sees the same page after closing messages.
        /// </returns>
        // POST: Student/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Student student)
        {
            if (student == null)
            {
                return Json(
                    new
                    {
                        state = 1, //1:random number for fail code
                        msg = "Student Model is NULL"
                    }
                );
            }

            if (StudentList.Any(a => a.Firstname == student.Firstname && a.Lastname == student.Lastname))
            {
                return Json(
                    new
                    {
                        state = 2, //2:random number for fail code
                        msg = "Student already EXISTS!"
                    }
                );
            }

            try
            {
                int maxUsedId = StudentList.Max(a => a.Id);
                student.Id = ++maxUsedId;
                StudentList.Add(student);

                return Json(
                    new
                    {
                        state = 0, //0 for success
                        msg = "Student has been CREATED successfully.",
                        redirectUrlInSuccess = "Index" // Goes to /Student/Index page. If we want to be redirected to page under another controller, we could use /Home/Index.
                    }
                );
            }
            catch (Exception e)
            {
                return Json(
                    new
                    {
                        state = 99, //99:random number for fail code
                        msg = "Exception occurred while user in creation!" + e.Message
                    }
                );
            }
        }


        // GET: Student/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Student/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Student/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Student/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}