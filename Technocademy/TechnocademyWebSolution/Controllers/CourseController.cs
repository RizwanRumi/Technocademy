using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using TechnocademyWebSolution.Models;

namespace TechnocademyWebSolution.Controllers
{
    public class CourseController : Controller
    {
        private ProjectDB db = new ProjectDB(); 
        // GET: Course
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetAllCourses()
        {
            var courses = db.Courses.ToList();
            return Json(courses, JsonRequestBehavior.AllowGet);
        }
       

        [HttpPost]
        public JsonResult Create([Bind(Include = "CourseId,CourseName")]Course model)
        {
            var courseList = db.Courses.ToList();

            if (courseList.Any(course => course.CourseName == model.CourseName))
            {
                return Json("no", JsonRequestBehavior.AllowGet);
            }
            
            if (ModelState.IsValid)
            {
                db.Courses.Add(model);
                db.SaveChanges();
                return Json(model, JsonRequestBehavior.AllowGet);
            }

            throw new HttpException(404, "There were error in your database");
        }

        [HttpPost]
        public JsonResult Edit([Bind(Include = "CourseId,CourseName")]Course model)
        {
            if (ModelState.IsValid)
            {
                db.Entry(model).State = EntityState.Modified;
                db.SaveChanges();
                return Json(model, JsonRequestBehavior.AllowGet);
            }
            throw new HttpException(404, "There were error in your database");
        }

        public JsonResult DeleteConfirmed(int id)
        {
            var existing = db.Courses.Find(id);

            if (existing != null)
            {
                db.Courses.Remove(existing);
                db.SaveChanges();
                return Json("ok", JsonRequestBehavior.AllowGet);
            }
            throw new HttpException(404, "There were error in your database");
        }
    }
}