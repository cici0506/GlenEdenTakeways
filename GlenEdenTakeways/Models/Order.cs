using System.ComponentModel.DataAnnotations;

namespace GlenEdenTakeways.Models
{
    public class Order
    {
        [Display(Name = "Order")]
        public int OrderId { get; set; }
        [Display(Name = "Order Date")]
        [DataType(DataType.Date)]
        public DateTime OrderDate { get; set; }
        public string Quantity { get; set; }
        [Required(ErrorMessage = "An Item Name is required")]
        [Display(Name = "Item Name")]
        [StringLength(160)]
        public string ItemName { get; set; }
        [Required]
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }
        [Display(Name = "Customer")]
        public int CustomerId { get; set; }
        [Display(Name = "Employee")]
        public int EmployeeId { get; set; }
        public virtual Customer Customer { get; set; } = null!;

        public virtual Employee Employee { get; set; } = null!;
        public virtual Payment Payment { get; set; } = null!;
    }
}
