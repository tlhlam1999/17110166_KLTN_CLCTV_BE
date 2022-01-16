using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shop.entities
{
    public class Comment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Content { get; set; }
        public int UserId { get; set; } 
        public string Image { get; set; }
        public int BlogId { get; set; }
        public string DateCreated { get; set; }
        [NotMapped]
        public string UserName { get; set; }
    }
}