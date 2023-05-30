using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebApplication1.Models;
using WebApplication1.Views.Home;
using System.Data.SQLite;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        [HttpPost]
        public IActionResult DeleteChapter(int chapterId)
        {
            // Code to delete chapter from the database using chapterId
            // ...
            return RedirectToAction("Index");
        }

        [HttpPost]
        [HttpPost]
        public IActionResult DeleteArticle(int articleId)
        {
            string connectionString = "\"C:\\Users\\Viet\\Documents\\Zalo Received Files\\net03-1.db\"";

            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                string query = "DELETE FROM Articles WHERE ID = @ID";

                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ID", articleId);
                    command.ExecuteNonQuery();
                }
            }

            return RedirectToAction("Index");
        }


        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult lienlac()
        {
            return View();
        }
        public IActionResult Edit()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
