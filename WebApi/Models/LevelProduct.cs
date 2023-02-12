
using System.ComponentModel.DataAnnotations;

namespace WebApi.Models
{
    public class LevelProduct
    {
        [Key]
        public int Id { get; set; }
        public string Level { get; set; }
    }
}
