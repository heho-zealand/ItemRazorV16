using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ItemRazorV1.Models
{
    public class Order
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OrderId { get; set; }
        
        [Range(1, int.MaxValue, ErrorMessage = "Please enter a value bigger than {1}")]
        public int Count { get; set; }
        
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime Date { get; set; }
        
        [Required]
        public int UserId { get; set; }
        
        public User User { get; set; }
        
        [Required]
        public int ItemId { get; set; }
        
        public Item Item { get; set; }

        public Order()
        {

        }

        public Order(User user, Item item)
        {
            Date = DateTime.Now;
            User = user;
            Item = item;
        }

    }
}

