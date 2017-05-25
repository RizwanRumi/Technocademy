using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TechnocademyWebSolution.Models;

namespace TechnocademyWebSolution.Controllers
{
    public class CategoryController : Controller
    {
        private ProjectDB db = new ProjectDB();
      
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetAllCategories()
        {
            var categories = db.Categories.ToList();
            return Json(categories, JsonRequestBehavior.AllowGet);
        }
        
        [HttpPost]
        public JsonResult Create([Bind(Include = "CategoryId,CategoryName,CourseId")] Category model)
        {
            var categoryList = db.Categories.ToList();

            if (categoryList.Any(c => c.CategoryName == model.CategoryName))
            {
                return Json("no", JsonRequestBehavior.AllowGet);
            }
            
            if (ModelState.IsValid)
            {
                db.Categories.Add(model);
                db.SaveChanges();
               
                return Json(model, JsonRequestBehavior.AllowGet);
            }
            throw new HttpException(404, "There were error in your database");
        }
       
        [HttpPost]
        public JsonResult Edit([Bind(Include = "CategoryId,CategoryName,CourseId")] Category model)
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
            var existing = db.Categories.Find(id);

            if (existing != null)
            {
                db.Categories.Remove(existing);
                db.SaveChanges();
                return Json("ok", JsonRequestBehavior.AllowGet);
            }
            throw new HttpException(404, "There were error in your database");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
