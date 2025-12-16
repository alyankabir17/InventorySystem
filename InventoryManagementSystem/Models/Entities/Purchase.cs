using System.ComponentModel.DataAnnotations;

namespace InventoryManagementSystem.Models.Entities
{
    public class Purchase
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int ProductId { get; set; } // Foreign Key link to Product

        [Required]
        public int SupplierId { get; set; } // Foreign Key link to Supplier

        [Required]
        public int Quantity { get; set; }

        public DateTime PurchaseDate { get; set; } = DateTime.Now;
    }
}