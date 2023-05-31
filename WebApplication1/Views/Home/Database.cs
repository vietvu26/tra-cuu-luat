using System.Data.SQLite;


namespace WebApplication1.Views.Home
{
    public partial class Database
    {
        private SQLiteConnection connection;
        private string databaseName = "\"C:\\Users\\Viet\\Documents\\Zalo Received Files\\net03-1.db\"";

        public Database()
        {
            ConnectToDatabase();
        }

        private void ConnectToDatabase()
        {
            connection = new SQLiteConnection($"Data Source={databaseName}; Version=3;");
            connection.Open();
        }

        public List<Article> LoadData()
        {
            List<Article> articles = new List<Article>();

            string query = "SELECT ID, Title, Content, ChapterID FROM Articles";

            SQLiteCommand command = new SQLiteCommand(query, connection);
            SQLiteDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                articles.Add(new Article
                {
                    ID = reader.GetInt32(0),
                    Title = reader.GetString(1),
                    Content = reader.GetString(2),
                    ChapterID = reader.GetInt32(3)
                });
            }

            return articles;
        }

        public List<Chapter> LoadChapters()
        {
            List<Chapter> chapters = new List<Chapter>();

            string query = "SELECT ID, Title, Content FROM Chapters";

            SQLiteCommand command = new SQLiteCommand(query, connection);
            SQLiteDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                chapters.Add(new Chapter
                {
                    ID = reader.GetInt32(0),
                    Title = reader.GetString(1),
                    Content = reader.GetString(2)
                });
            }

            return chapters;
        }
        public List<Section> LoadSections()
        {
            List<Section> sections = new List<Section>();

            string query = "SELECT * FROM Sections";

            SQLiteCommand command = new SQLiteCommand(query, connection);
            SQLiteDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                Section a = new Section();
                a.ID = reader.GetInt32(0);
                a.Title = reader.GetString(1);
                a.Content = reader.GetString(2);
                a.Min = reader.GetString(3);
                a.Max = reader.GetString(4);
                a.Avg = reader.GetString(5);
                a.ArticleID = reader.GetInt32(8);
                sections.Add(a);
            }

            return sections;
        }
        public Section getKhoan(int khoanId)
        {
            string query = "Select * FROM Sections WHERE ID=@ID";
            SQLiteCommand command = new SQLiteCommand(query, connection);
            command.Parameters.AddWithValue("@ID", khoanId);
            Section a = new Section();
            SQLiteDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                a.ID = reader.GetInt32(0);
                a.Title = reader.GetString(1);
                a.Content = reader.GetString(2);
                a.Min = reader.GetString(3);
                a.Max = reader.GetString(4);
                a.Avg = reader.GetString(5);
                a.ArticleID = reader.GetInt32(8);
            }
            return a;
        }
        public void UpdateChapter(Chapter chapter)
        {
            string query = "UPDATE Chapters SET Title = @Title, Content = @Content WHERE ID = @ID";

            using (SQLiteCommand command = new SQLiteCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Title", chapter.Title);
                command.Parameters.AddWithValue("@Content", chapter.Content);


                command.ExecuteNonQuery();
            }
        }


      

    }

    public class Article
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public int ChapterID { get; set; }
    }
    public class ArticleAd
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }

    }
    public class ChapterAd
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
    }
    public class SectionAd
    {
        public int ID { get; set; }
        public string Content { get; set; }
        public string Min { get; set; }
        public string Max { get; set; }
        public string Avg { get; set; }

    }

    public class Chapter
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
    }

    public class Section
    {
        
        public int ArticleID { get; set; }
        public string Min { get; set; }
        public string Max { get; set; }
        public string Avg { get; set; }


        public string Title { get; set; }
        public int DecreeID { get; set; }
        public int ID { get; set; }
        public string Content { get; set; }
    }
}
