using SQLite;
using Gallery.Models;
using System.Diagnostics;

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
            try
            {
                if (!File.Exists(dbPath))
                {
                    File.Create(dbPath);
                }
                conn = new SQLiteConnection(dbPath);
                conn.CreateTable<Models.Image>();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
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
