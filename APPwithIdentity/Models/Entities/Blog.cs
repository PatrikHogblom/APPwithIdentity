using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using APPwithIdentity.Data;

namespace APPwithIdentity.Models.Entities
{
    public class Blog
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "The Header field is required.")]
        public string Header { get; set; }

        [Required(ErrorMessage = "The Text field is required.")]
        public string Text { get; set; }

        public string? Image { get; set; }

        [ForeignKey("ApplicationUser")]
        public string UserId { get; set; }

        public ApplicationUser ApplicationUser { get; set; }
    }
}
