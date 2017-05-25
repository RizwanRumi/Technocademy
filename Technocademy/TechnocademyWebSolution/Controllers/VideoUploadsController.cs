using System;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TechnocademyWebSolution.Models;

namespace TechnocademyWebSolution.Controllers
{
    public class VideoUploadsController : Controller
    {
        private ProjectDB db = new ProjectDB();

        // GET: VideoUploads
       
        public ActionResult Index()
        {
            return View();
            //return Json(JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetAllVideo()
        {
            var videoUploads = db.VideoUploads.ToList();
            return Json(videoUploads,JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetAllCategories()
        {
            var categories = db.Categories.ToList();
            return Json(categories, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Create(string vdoName, int vdoCaId, string vdoLbl, int vdoRating, string vdoLecturer, VideoUpload model)
        {
            if (Request.Files["vdoFile"] != null)
            {
                HttpPostedFileBase vdoFile = Request.Files["vdoFile"];
                string id = Guid.NewGuid().ToString().Replace("-", "");

                var filename = Path.GetFileName(vdoFile.FileName);
                var extension = Path.GetExtension(filename);

                string folderPath = Server.MapPath("~/UploadVideoFile/Category_" + vdoCaId);

                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }

                var path = folderPath + "\\" + id + extension;
                
                vdoFile.SaveAs(path);
               
                
              
                model.VideoName = vdoName;
                model.CategoryId = vdoCaId;
                model.FileName = vdoName + extension;
                model.FilePath = ("/UploadVideofile/Category_"+vdoCaId+"/" + id + extension).Trim();
                model.Label = vdoLbl;
                model.Ratings = vdoRating;
                model.Lecturer = vdoLecturer;
                model.PublishDate = DateTime.Now;

                if (ModelState.IsValid)
                {
                    db.VideoUploads.Add(model);
                    db.SaveChanges();
                    return Json(model, JsonRequestBehavior.AllowGet);
                }
                throw new HttpException(404, "There were error in your database");

            }

            throw new HttpException(100, "File upload error");

        }


        [HttpPost]
        public JsonResult Edit(string evdoFilePath, int evdoUpId, string evdoName, int evdoCaId, string evdoLbl, int evdoRating, string evdoLecturer, VideoUpload model)
        {
            string[] separators = { "/", "."};
           
            string[] words = evdoFilePath.Split(separators, StringSplitOptions.RemoveEmptyEntries);

            if (Request.Files["evdoFile"] != null)
            {
                HttpPostedFileBase vdoFile = Request.Files["evdoFile"];
                var filename = Path.GetFileName(vdoFile.FileName);
                var extension = Path.GetExtension(filename);

                string folderPath = HttpContext.Server.MapPath("~/UploadVideoFile/Category_" + evdoCaId);

                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }
              
                var path = folderPath + "\\" + words[2] + extension;

                    vdoFile.SaveAs(path);
               

                model.VideoUploadId = evdoUpId;
                model.VideoName = evdoName;
                model.CategoryId = evdoCaId;
                model.FileName = evdoName + extension;
                model.FilePath = ("/UploadVideofile/Category_"+evdoCaId+"/" +words[2] + extension).Trim();
                model.Label = evdoLbl;
                model.Ratings = evdoRating;
                model.Lecturer = evdoLecturer;
                model.PublishDate = DateTime.Now;

                if (ModelState.IsValid)
                {
                    db.Entry(model).State = EntityState.Modified;
                    db.SaveChanges();
                    return Json(model, JsonRequestBehavior.AllowGet);
                }
                throw new HttpException(404, "There were error in your database");

            }

            return Json("error", JsonRequestBehavior.AllowGet);

        }

        public JsonResult DeleteConfirmed(int id)
        {
            var existing = db.VideoUploads.Find(id);

            if (existing != null)
            {
                db.VideoUploads.Remove(existing);
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
