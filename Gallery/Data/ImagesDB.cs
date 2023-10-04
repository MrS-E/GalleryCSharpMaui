using SQLite;
using Gallery.Models;

namespace Gallery.Data
{
    public class ImagesDB
    {
        string dbPath;
        private SQLiteConnection conn;

        public ImagesDB(string dbPath)
        {
            this.dbPath = dbPath;
        }

        public void Init()
        {
            if(!File.Exists(dbPath)) 
            { 
                File.Create(dbPath);
            }
            conn = new SQLiteConnection(dbPath);
            conn.CreateTable<Models.Image>();
        }

        public List<Models.Image> GetImages()
        {
            Init();
            return conn.Table<Models.Image>().ToList();
        }

        public void Add(Models.Image image)
        {
            Init();
            conn.Insert(image);
        }

        public void Delete(int id)
        {
            Init();
            conn.Delete(new {id = id});
        }
    }
}
