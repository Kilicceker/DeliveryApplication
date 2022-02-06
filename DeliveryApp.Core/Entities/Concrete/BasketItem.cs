using DeliveryApp.Core.Entities.Abstaract;

namespace DeliveryApp.Core.Entities.Concrete
{
    public class BasketItem:IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public string PictureUrl { get; set; }
        public string ProductBrand { get; set; }
        public string ProductType { get; set; }
    }
}
