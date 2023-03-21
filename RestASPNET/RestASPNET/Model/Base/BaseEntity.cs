using System.ComponentModel.DataAnnotations.Schema;

namespace RestASPNET.Model.Base
{
    public class BaseEntity
    {
        [Column("id")]
        public int Id { get; set; }
    }
}
