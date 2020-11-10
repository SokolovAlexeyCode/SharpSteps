using System.Linq;
using System.Web.Mvc;
using MVCFirstProject.DAL;
using MVCFirstProject.DAL;
using MVCFirstProject.Models;

namespace MVCFirstProject.Controllers
{
    public class HomeController : Controller
    {
        private SchoolContext db = new SchoolContext();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            var groups = db.Students
                .GroupBy(s => s.EnrollmentDate).Select(g =>
                    new EnrollmentDateGroup
                    {
                        EnrollmentDate = g.Key,
                        StudentCount = g.Count()
                    }).ToList();

            ViewBag.Message = "Your application description page.";
            return View(groups);
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
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