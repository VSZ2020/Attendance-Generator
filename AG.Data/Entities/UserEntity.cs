using System.ComponentModel.DataAnnotations;

namespace AG.Data.Entities
{
    public class UserEntity: BaseEntity
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string PasswordHash { get; set; }

        public string Email { get; set; }

        public bool IsEmailConfirmed { get; set; }

        public string Role { get; set; }

        public bool IsActivated { get; set; }

        public DateTime LastVisit { get; set; }

        public DateTime CreatedAt { get; set; }

    }
}
