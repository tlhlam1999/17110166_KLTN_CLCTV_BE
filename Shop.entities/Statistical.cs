

namespace Shop.entities
{
    public class Statistical
    {
        public string Category { get; set; }
        public string Brand { get; set; }
        public string Product { get; set; }
        public double? Price { get; set; }
        public string DateTrade { get; set; }
        public int Inventory { get; set; }
        public int SoldQuantity { get; set; }
        public double Revenue { get; set; }
        public double TotalRevenue { get; set; }
    }
}
