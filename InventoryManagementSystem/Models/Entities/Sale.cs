using System.ComponentModel.DataAnnotations;

namespace InventoryManagementSystem.Models.Entities
{
    public class Sale
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int ProductId { get; set; }

        [Required]
        public int CustomerId { get; set; }

        [Required]
        public int Quantity { get; set; }

        // THIS IS THE MISSING PART CAUSING THE ERROR
        public decimal SoldPrice { get; set; }

        public decimal TotalAmount { get; set; }

        public DateTime SaleDate { get; set; } = DateTime.Now;
    }
}