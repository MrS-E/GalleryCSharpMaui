using SQLite;
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

        public bool Init()
        {
            try
            {
                if (!File.Exists(dbPath))
                {
                    File.Create(dbPath);
                }
                conn = new SQLiteConnection(dbPath);
                conn.CreateTable<Models.Image>();
                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                return false;
            }
        }

        public List<Models.Image> GetImages()
        {
            return conn.Table<Models.Image>().ToList();
        }

        public void Add(Models.Image image)
        {
            conn.Insert(image);
        }

        public void Delete(int id)
        {
            conn.Delete(new { id = id });
        }

        public void Close()
        {
            conn.Close();
        }
    }
}
