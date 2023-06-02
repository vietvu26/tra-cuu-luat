using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebApplication1.Models;
using WebApplication1.Views.Home;
using System.Data.SQLite;
using WebApplication1.Services;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;


        public HomeController(ILogger<HomeController> logger)
        {
           
            _logger = logger;
        }
        


        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Delete()
        {
            return View();
        }
        public IActionResult Edit()
        {
            return View();
        }
        public IActionResult EditSection(int id)
        {
            Database database = new Database();
            Section model = database.getKhoan(id);
            return View(model);
        }
        public IActionResult EditChapter(int id)
        {
            Database database = new Database();
            Chapter model = database.getChuong(id);
            return View(model);
        }
        public IActionResult EditArticle(int id)
        {
            Database database = new Database();
            Article model = database.getDieu(id);
            return View(model);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        // đăng nhập
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(User model)
        {

            string dbPath = "Models\\net03-1.db";
            SQLiteManager sqliteManager = new SQLiteManager(dbPath);
            User users = sqliteManager.GetUsers(model);
            if (users != null)
            {
                // Đăng nhập thành công
                // Kiểm tra vai trò của người dùng và chuyển hướng đến trang tương ứng
                HttpContext.Session.SetString("ID", users.ID.ToString());
                TempData["ok"] = true;
                TempData.Keep("ok");
                HttpContext.Session.SetString("ok", "true");
                if (users.Role == "admin")
                {

                    return RedirectToAction("home_admin", "Home"); // Chuyển hướng đến trang admin
                }
                else
                {
                    return RedirectToAction("Index", "Home"); // Chuyển hướng đến trang chủ
                }

            }
            else
            {
                ModelState.AddModelError("", "Tên đăng nhập hoặc mật khẩu không đúng.");
                // Đăng nhập không thành công, hiển thị lại trang đăng nhập với thông báo lỗi
                return View();
            }
        }

        public IActionResult home_admin()
        {
            return View();
        }
        // đăng ký
        public IActionResult Signup()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Signup(User model)
        {
            string dbPath = "Models\\net03-1.db";
            SQLiteManager sqliteManager = new SQLiteManager(dbPath);

            // Kiểm tra xem người dùng đã tồn tại trong cơ sở dữ liệu chưa
            User existingUser = sqliteManager.CheckUsers(model);
            if (existingUser != null)
            {
                ModelState.AddModelError("", "Người dùng đã tồn tại.");
                return View();
            }
            else
            {
                sqliteManager.AddUser(model);
                // Đăng ký thành công, chuyển hướng đến trang đăng nhập
                return RedirectToAction("Login");
            }
        }


        // quên pass
        public ActionResult ForgotPassword()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ForgotPassword(User model)
        {
            string dbPath = "Models\\net03-1.db";
            SQLiteManager sqliteManager = new SQLiteManager(dbPath);
            // Kiểm tra thông tin người dùng:
            User checkUser = sqliteManager.CheckUsers1(model);
            if (checkUser != null)
            {
                return RedirectToAction("NewPassWord");
            }
            else
            {
                ModelState.AddModelError("", "Không tồn tại người dùng.");
                return View();
            }
           
        }
        public ActionResult NewPassWord()
        {
            return View();
        }
        [HttpPost]
        // Phương thức cập nhật mật khẩu mới cho người dùng
        public ActionResult UpdateUserPassword(string email, string newPassword)
        {
            string dbPath = "Models\\net03-1.db";
            SQLiteManager sqliteManager = new SQLiteManager(dbPath);
            sqliteManager.UpdatePassword(email, newPassword);
            return RedirectToAction("PasswordResetConfirmation", "Home");
        }

     
        public ActionResult PasswordResetConfirmation()
        {
            return View();
        }
        public ActionResult LuuThongTin(string tenTruycap, string email, string sodienthoai)
        {
            string dbPath = "Models\\net03-1.db";
            SQLiteManager sqliteManager = new SQLiteManager(dbPath);
            sqliteManager.saveUser(tenTruycap, email, sodienthoai);
            return RedirectToAction("trangcanhan", "Home");
        }


        public IActionResult trangcanhan()
        {
            var id = HttpContext.Session.GetString("ID");
            string dbPath = "Models\\net03-1.db";
            SQLiteManager sqliteManager = new SQLiteManager(dbPath);
            User users = sqliteManager.GetUsers(id);
            return View(users);
        }



        public IActionResult Logout()
        {

            TempData.Remove("ok");
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }


        public IActionResult timkiem(string data)
        {
            ViewBag.data = data;
            // Gọi phương thức timkiem và truyền dữ liệu tìm kiếm vào
            List<Section> searchResults = new Database().search(data);
            // Truyền danh sách kết quả tìm kiếm vào view
            return View("timkiem", searchResults);
        }
        public IActionResult Save()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Save(Chapter model)
        {
            string dbPath = "Models\\net03-1.db";

            using (var connection = new SQLiteConnection($"Data Source={dbPath}"))
            {
                connection.Open();

                // Cập nhật title và content của chương
                string updateQuery = "UPDATE Chapters SET Title = @Title, Content = @Content WHERE ID = @ID";
                using (var command = new SQLiteCommand(updateQuery, connection))
                {
                    command.Parameters.AddWithValue("@Title", model.Title);
                    command.Parameters.AddWithValue("@Content", model.Content);
                    command.Parameters.AddWithValue("@ID", model.ID);

                    command.ExecuteNonQuery();
                }
            }

            // Chuyển hướng về trang khác sau khi cập nhật thành công
            return RedirectToAction("home_admin");
        }
        public ActionResult SaveSection(Section model)
        {
            string dbPath = "Models\\net03-1.db";

            using (var connection = new SQLiteConnection($"Data Source={dbPath}"))
            {
                connection.Open();

                // Cập nhật title và content của chương
                string updateQuery = "UPDATE Sections SET Title = @Title, Content = @Content, Min = @Min, Max = @Max, Avg = @Avg WHERE ID = @ID";
                using (var command = new SQLiteCommand(updateQuery, connection))
                {
                    command.Parameters.AddWithValue("@Title", model.Title);
                    command.Parameters.AddWithValue("@Content", model.Content);
                    command.Parameters.AddWithValue("@ID", model.ID);
                    command.Parameters.AddWithValue("@Min", model.Min);
                    command.Parameters.AddWithValue("@Max", model.Max);
                    command.Parameters.AddWithValue("@Avg", model.Avg);


                    command.ExecuteNonQuery();
                }
            }

            // Chuyển hướng về trang khác sau khi cập nhật thành công
            return RedirectToAction("home_admin");
        }
       

        public ActionResult SaveArticle(Article model)
        {
            string dbPath = "Models\\net03-1.db";

            using (var connection = new SQLiteConnection($"Data Source={dbPath}"))
            {
                connection.Open();

                // Cập nhật thông tin của Article vào cơ sở dữ liệu
                string updateQuery = "UPDATE Articles SET Title = @Title, Content = @Content WHERE ID = @ID";
                using (var command = new SQLiteCommand(updateQuery, connection))
                {
                    command.Parameters.AddWithValue("@Title", model.Title);
                    command.Parameters.AddWithValue("@Content", model.Content);
                    command.Parameters.AddWithValue("@ID", model.ID);

                    command.ExecuteNonQuery();
                }
            }

            // Chuyển hướng về trang khác sau khi cập nhật thành công
            return RedirectToAction("home_admin");
        }
        public ActionResult DeleteArticle(int id)
        {
            string dbPath = "Models\\net03-1.db";

            using (var connection = new SQLiteConnection($"Data Source={dbPath}"))
            {
                connection.Open();

                // Xóa article với ID tương ứng
                string deleteQuery = "DELETE FROM Articles WHERE ID = @ID";
                using (var command = new SQLiteCommand(deleteQuery, connection))
                {
                    command.Parameters.AddWithValue("@ID", id);
                    command.ExecuteNonQuery();
                }
            }

            // Chuyển hướng về trang danh sách article sau khi xóa thành công
            return RedirectToAction("home_admin");
        }
        public ActionResult DeleteChapter(int id)
        {
            string dbPath = "Models\\net03-1.db";

            using (var connection = new SQLiteConnection($"Data Source={dbPath}"))
            {
                connection.Open();

                // Xóa article với ID tương ứng
                string deleteQuery = "DELETE FROM Chapters WHERE ID = @ID";
                using (var command = new SQLiteCommand(deleteQuery, connection))
                {
                    command.Parameters.AddWithValue("@ID", id);
                    command.ExecuteNonQuery();
                }
            }

            // Chuyển hướng về trang danh sách article sau khi xóa thành công
            return RedirectToAction("home_admin");
        }
        public ActionResult DeleteSection(int id)
        {
            string dbPath = "Models\\net03-1.db";

            using (var connection = new SQLiteConnection($"Data Source={dbPath}"))
            {
                connection.Open();

                // Xóa article với ID tương ứng
                string deleteQuery = "DELETE FROM Sections WHERE ID = @ID";
                using (var command = new SQLiteCommand(deleteQuery, connection))
                {
                    command.Parameters.AddWithValue("@ID", id);
                    command.ExecuteNonQuery();
                }
            }

            // Chuyển hướng về trang danh sách article sau khi xóa thành công
            return RedirectToAction("home_admin");
        }
        [HttpGet]
        public IActionResult Contact()
        {
            return View(new ContactViewModel());
        }
        [HttpPost]
        public async Task<IActionResult> Contact(ContactViewModel model, [FromServices] IEmailService emailService)
        {
            if (ModelState.IsValid)
            {
                // Gửi email thông tin liên hệ
                await emailService.SendContactEmailAsync(model);

                // Hiển thị thông báo thành công
                TempData["SuccessMessage"] = "Cảm ơn bạn đã liên hệ với chúng tôi! Chúngta sẽ phản hồi lại trong thời gian sớm nhất.";

                // Chuyển hướng lại trang liên hệ
                return RedirectToAction("Contact");
            }

            // Hiển thị lại form liên hệ nếu thông tin không hợp lệ
            return View(model);
        }
    }
}
