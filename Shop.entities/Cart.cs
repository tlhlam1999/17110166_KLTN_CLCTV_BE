 
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema; 

namespace Shop.entities
{
    public class Cart
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int Quantity { get; set; }
        public int? UserId { get; set; }
        public string ClientIp { get; set; }
        public int ProductId { get; set; }
        public double Balance { get; set; }

        [NotMapped]
        public Product Product { get; set; }
    }
}
