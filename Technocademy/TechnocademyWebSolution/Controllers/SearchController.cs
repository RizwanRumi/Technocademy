using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using TechnocademyWebSolution.Models;

namespace TechnocademyWebSolution.Controllers
{
    public class SearchController : Controller
    {
        private ProjectDB db = new ProjectDB();
        // GET: Search
        public ActionResult Index()
        {
            return View();
        }

        //public JsonResult GetAllVideo()
        //{
        //    var allVideo = db.VideoUploads.ToList();
        //    return Json(allVideo, JsonRequestBehavior.AllowGet);
        //}

        public JsonResult GetAllCourses()
        {
            var courses = db.Courses.ToList();
            return Json(courses, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetAllCategories()
        {
            var categories = db.Categories.ToList();
            return Json(categories, JsonRequestBehavior.AllowGet);
        }

        public JsonResult SearchVideo(int id, int val, string search)
        {
            if (!string.IsNullOrEmpty(search))
            {
                var videoList = db.VideoUploads.Where(v => v.VideoName.Contains(search)).OrderBy(v =>v.VideoUploadId).Skip(val).Take(10).ToList();

                return Json(videoList, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var videoList = db.VideoUploads.ToList();

                if (id == 0)
                {
                    return Json(videoList.OrderBy(v => v.VideoUploadId).Skip(val).Take(10).ToList(), JsonRequestBehavior.AllowGet);
                }

                var categories = db.Categories.Where(ca => ca.CourseId == id).ToList();

                List<VideoUpload> sortedVideoUploads = (from category in categories
                                                        from videoUpload in videoList
                                                        where videoUpload.CategoryId == category.CategoryId
                                                        select videoUpload).OrderBy(v => v.VideoUploadId).Skip(val).Take(10).ToList();

                return Json(sortedVideoUploads, JsonRequestBehavior.AllowGet);
            }
        }

        //public JsonResult SearchVideo(string searchValue)
        //{
        //    if (!string.IsNullOrEmpty(searchValue))
        //    {
        //        var videoList = db.VideoUploads.Where(v => v.VideoName.Contains(searchValue)).ToList();

        //        return Json(videoList,JsonRequestBehavior.AllowGet);
        //    }

        //    return null;
        //}

        //public JsonResult GetVideo(int val)
        //{
        //    var selectedvideo = db.VideoUploads.OrderBy(v => v.VideoUploadId).Skip(val).Take(10).ToList();
        //    return Json(selectedvideo, JsonRequestBehavior.AllowGet);
        //} 
    }
}