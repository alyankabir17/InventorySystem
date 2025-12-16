using Microsoft.EntityFrameworkCore;

namespace InventoryManagementSystem.Models.Entities
{
    public class User
    {
        public int Id { get; set; }
        public String? FirstName { get; set; }
        public String? LastName { get; set; }
        public String? Email { get; set; }
        public String? Username { get; set; }
        public String? Password { get; set; }
        public int Role { get; set; }
    }
}
