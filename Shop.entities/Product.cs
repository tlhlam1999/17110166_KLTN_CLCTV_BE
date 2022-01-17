using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shop.entities
{
    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int BrandId { get; set; }
        public int CompositionId { get; set; }
        public string NameProduct { get; set; } 
        public string Images { get; set; }
        public string Code { get; set; }
        public double Price { get; set; }
        public string Description { get; set; }
        public int TotalItems { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public bool IsDisabled { get; set; }
        [NotMapped]
        public string BrandName { get; set; }
        [NotMapped]
        public string CompositionName { get; set; }
    }
}
