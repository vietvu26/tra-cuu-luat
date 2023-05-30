using System.Data.SQLite;


namespace WebApplication1.Views.Home
{
    public partial class Database
    {
        private SQLiteConnection connection;
        private string databaseName = "Models\\net03-1.db";

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
        public List<Section> LoadSections()
        {
            List<Section> sections = new List<Section>();

            string query = "SELECT ID,Title,Content,ArticleID FROM Sections";

            SQLiteCommand command = new SQLiteCommand(query, connection);
            SQLiteDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                sections.Add(new Section
                {
                    ID = reader.GetInt32(0),
                    Title = reader.GetString(1),
                    Content = reader.GetString(2),
                    ArticleID = reader.GetInt32(3)
                });
            }

            return sections;
        }

        public void DeleteChapter(int chapterId)
        {
            string query = "DELETE FROM Chapters WHERE ID = @ID";

            SQLiteCommand command = new SQLiteCommand(query, connection);
            command.Parameters.AddWithValue("@ID", chapterId);
            command.ExecuteNonQuery();
        }

        public void DeleteArticle(int articleId)
        {
            string query = "DELETE FROM Articles WHERE ID = @ID";

            SQLiteCommand command = new SQLiteCommand(query, connection);
            command.Parameters.AddWithValue("@ID", articleId);
            command.ExecuteNonQuery();
        }

        public void DeleteSection(int sectionId)
        {
            string query = "DELETE FROM Sections WHERE ID = @ID";

            SQLiteCommand command = new SQLiteCommand(query, connection);
            command.Parameters.AddWithValue("@ID", sectionId);
            command.ExecuteNonQuery();
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
