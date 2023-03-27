using RestASPNET.Model.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestASPNET.Model
{
    [Table("books")]
    public class Book: BaseEntity
    {
        [Column("title")]
        public string Title { get; set; }
        [Column("author")]
        public string Author { get; set; }
        [Column("launch_date")]
        public DateTime Launch_Date { get; set; }
        [Column("price")]
        public decimal Price { get; set; }
    }
}
