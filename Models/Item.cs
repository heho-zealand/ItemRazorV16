using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ItemRazorV1.Models
{
    public class Item : IComparable<Item>
    {
        [Display(Name = "Item ID")]
        [Required(ErrorMessage = "Der skal angives et ID til Item")]
        [Range(typeof(int), "0", "10000", ErrorMessage = "ID skal være mellem (1) og (2)")]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        [Display(Name = "Item Navn")]
        [Required(ErrorMessage = "Item skal have et navn")]
        [StringLength(100)]

        public string Name { get; set; }

        [Display(Name = "Pris")]
        [Required(ErrorMessage = "Der skal angives en pris")]
        public decimal Price { get; set; }

        public string Description { get; set; }

        public string ItemImage { get; set; }

        public Item()
        {
            Id = 0;
            Name = "";
            Price = 0;
            Description = "";
            ItemImage = "";
        }

        public Item(int id, string name, decimal price)
        {
            Id = id;
            Name = name;
            Price = price;
        }

        public int CompareTo(Item other)
        {         
            return Id - other.Id;
        }
    }
}
