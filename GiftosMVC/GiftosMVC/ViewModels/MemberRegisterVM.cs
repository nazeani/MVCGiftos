using System.ComponentModel.DataAnnotations;

namespace GiftosMVC.ViewModels
{
    public class MemberRegisterVM
    {
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [MinLength(8)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        [MinLength(8)]
        [DataType(DataType.Password)]
        [Compare(nameof(Password))]
        public string RepeatPassword { get; set; }

    }
}
