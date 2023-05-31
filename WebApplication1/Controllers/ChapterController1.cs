using Microsoft.AspNetCore.Mvc;
using WebApplication1.Views.Home;
using WebApplication1.Views.Home;

namespace WebApplication1.Controllers
{
    public class ChapterController1 : Controller
    {

        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Edit(Chapter chapter)
        {
            if (ModelState.IsValid)
            {
                // Cập nhật dữ liệu của chapter vào cơ sở dữ liệu ở đây
                Database db = new Database();
                db.UpdateChapter(chapter);

                return RedirectToAction("Index");
            }

            // Nếu ModelState không hợp lệ, quay lại trang Edit với dữ liệu chapter
            return View(chapter);
        }

    }
}
