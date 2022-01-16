
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shop.entities
{
    public class Blog
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Content { get; set; }
        public string Title { get; set; }
        public bool Status { get; set; }
        public string CreatedDate { get; set; }
        public int UserId { get; set; }
        public string Image { get; set; } 
        public List<Comment> Comments { get; set; }
        [NotMapped]
        public string Author { get; set; }
    }
}
