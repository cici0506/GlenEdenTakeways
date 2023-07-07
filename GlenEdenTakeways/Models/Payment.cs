using System.ComponentModel.DataAnnotations;

namespace GlenEdenTakeways.Models
{
    public class Payment
    {
        [Display(Name = "Payment")]
        public int PaymentId { get; set; }
        [Display(Name = "Customer")]
        public int CustomerId { get; set; }
        [Display(Name = "Order Item")]
        public int OrderItemId { get; set; }
        [Display(Name = "Payment Date")]
        [DataType(DataType.Date)]
        public DateTime PaymentDate { get; set; }
        [Required]
        [Display(Name = "Total Amount")]
        [DataType(DataType.Currency)]
        public decimal TotalAmount { get; set; }
        [Display(Name = "Payment Type")]
        public int PaymentTypeId { get; set; }

        public virtual PaymentType PaymentType { get; set; } = null!;
        public virtual ICollection<Customer> Customer { get; set; } = new List<Customer>();
        public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}
