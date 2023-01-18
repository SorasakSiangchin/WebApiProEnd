using System.ComponentModel.DataAnnotations.Schema;

namespace WebApiProjectEnd.Modes
{
    public class DetailProduct
    {
        public int Id { get; set; }
        public string SpeciesName { get; set; }
        public string Description { get; set; } 
        public string FertilizeMethod { get; set; } // การใส่ปุ๋ย
        public string PlantingMethod { get; set; } // การปลูก
        public string GrowingSeason { get; set; } // ฤดูปลูก (ควรปลูกฤดูไหน)
        public string HarvestTime { get; set; } // ระยะเวลาเก็บเกี่ยว
        public string ProductID { get; set; }
        [ForeignKey("ProductID")]
        public virtual Product Product { get; set; }    
    }
}
