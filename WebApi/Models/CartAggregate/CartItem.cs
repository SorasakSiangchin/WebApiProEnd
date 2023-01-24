using System.ComponentModel.DataAnnotations.Schema;
using WebApi.Models;
using WebApiProjectEnd.Modes;

namespace WebApi.Modes.CartAggregate
{
    [Table("CartItems")]
    public class CartItem
    {
        public int Id { get; set; }
        public int Amount { get; set; }
        public string ProductId { get; set; }
        public Product Product { get; set; }
        // ต้อง Save Basket ก่อน
        public string CartId { get; set; }
        public Cart Cart { get; set; }
    }
}
