using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using APPwithIdentity.Data;

namespace APPwithIdentity.Models.Entities
{
    public class Blog
    {
        public int Id { get; set; }

        [Required]
        public string Header { get; set; }

        [Required]
        public string Text { get; set; }

        public string Image { get; set; }

        [ForeignKey("ApplicationUser")]
        public string UserId { get; set; }

        public ApplicationUser ApplicationUser { get; set; }
    }
}
