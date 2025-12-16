using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace InventoryManagementSystem.Models.AddModels
{
    public class AddUser
    {
        [Required]
        [DisplayName("First Name")]
        public String? FirstName { get; set; }
        [Required]
        [DisplayName("Last Name")]
        public String? LastName { get; set; }
        [Required]
        [DisplayName("Email Address")]
        public String? Email { get; set; }
        [Required]
        public String? Username { get; set; }
        [Required]
        public String? Password { get; set; }

        public int Role { get; set; }
    }
}
