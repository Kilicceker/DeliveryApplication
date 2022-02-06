using DeliveryApp.Core.Entities.Abstaract;
using System;

namespace DeliveryApp.Core.Entities.Concrete
{
    public class Comment:IEntity
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public virtual bool IsPublished { get; set; } = false;
    }
}
