﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebApi.Models;

namespace WebApi.Modes.CartAggregate
{
    public class Cart
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string Id { get; set; }
        public List<CartItem> Items { get; set; } = new();
        public DateTime Created { get; set; }
        public string AccountID { get; set; }
        [ForeignKey("AccountID")]
        public virtual Account Account { get; set; }
        public void AddItem(Product product, int amount)
        {
            // ตรวจสอบโดยการ วนลูป ถ้าสินค้าที่ส่งมาไม่มีในตะกร้าให้เพิ่มเข้าไป
            if (Items.All(item => item.ProductId != product.Id))
            {
                //กำหนดค่าให้กับ ProductId โดยอัตโนมัติ
                Items.Add(new CartItem { Product = product, Amount = amount });
            }
            //รายการที่มีอยู่ ถ้ามีสินค้าในตะกร้าอยู่แล้วให้บวกจำนวนเพิ่มเข้าไป
            var existingItem = Items.FirstOrDefault(item => item.ProductId == product.Id);
            if (existingItem != null) existingItem.Amount += amount;
        }

        public void RemoveItem(string productId, int amount)
        {
            // ค้นหาสินค้า
            var item = Items.FirstOrDefault(item => item.ProductId == productId);
            if (item == null) return;
            item.Amount -= amount; //ลบสินค้าออกตามจำนวนที่ส่งมา
            if (item.Amount <= 0) Items.Remove(item); //ถ้าจำนวนสินค้ามันเป็น 0 หรือน้อยกว่า ให้ลบสินค้านั้นทิ้งไป
        }
    }
}
