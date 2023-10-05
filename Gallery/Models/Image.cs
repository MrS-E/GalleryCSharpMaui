using SQLite;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gallery.Models
{
    [SQLite.Table("images")]
    public class Image
    {
        [PrimaryKey, AutoIncrement, SQLite.Column("Id")]
        public int Id { get; set; }
        public string uri { get; set; }
        public string name { get; set; }
        public string tags { get; set; }
        public string desc { get; set; }
        public string createdAt { get; set; }
    }
}
