using System.Data.SQLite;

namespace WebApplication1.Views.Home
{
    public partial class Database
    {
        private SQLiteConnection connection;
        private string databaseName = "\"C:\\Users\\Viet\\Downloads\\net03-1.db\"";

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

            string query = "SELECT ID, Title FROM Chapters";

            SQLiteCommand command = new SQLiteCommand(query, connection);
            SQLiteDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                chapters.Add(new Chapter
                {
                    ID = reader.GetInt32(0),
                    Title = reader.GetString(1)
                });
            }

            return chapters;
        }


    }

    public class Article
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public int ChapterID { get; set; }
    }

    public class Chapter
    {
        public int ID { get; set; }
        public string Title { get; set; }
    }

    public class Section
    {
        public int Max { get; set; }
        public int Avg { get; set; }
        public int ArticleID { get; set; }
        public int Min { get; set; }
        public string Title { get; set; }
        public int DecreeID { get; set; }
        public int ID { get; set; }
        public string Content { get; set; }
    }
}
