using DeliveryApp.Core.Entities.Concrete;
using DeliveryApp.Core.Repositories.Abstract;
using StackExchange.Redis;
using System;
using System.Text.Json;
using System.Threading.Tasks;

namespace DeliveryApp.Data.Repositories
{
    public class BasketRepository : IBasketRepository
    {
        private readonly IDatabase _database;
        public BasketRepository(IConnectionMultiplexer redis)
        {
            _database = redis.GetDatabase();
        }

        public async Task<bool> DeleteBasketAsync(string id)
        {
            return await _database.KeyDeleteAsync(id);
        }

        public async Task<bool> DeleteProductFromBasketAsync(string basketId, int productId)
        {
            var data = await _database.StringGetAsync(basketId);
            if(!data.IsNullOrEmpty)
            {
                var basket= JsonSerializer.Deserialize<CustomerBasket>(data);
                foreach (var item in basket.Items)
                {
                    if(item.Id==productId)
                    {
                        basket.Items.Remove(item);
                        basket.TotalPrice =basket.TotalPrice - item.Quantity * item.Price;
                        await UpdateBasketAsync(basket);
                        return true;
                    }
                }
            }
            return false;
        }

        public async Task<CustomerBasket> GetBasketAsync(string id)
        {
            var data = await _database.StringGetAsync(id);
            CustomerBasket basket = new CustomerBasket();
            if (!data.IsNullOrEmpty)
            {
                basket = JsonSerializer.Deserialize<CustomerBasket>(data);
            }

            return data.IsNullOrEmpty ? null : basket;
        }

        public async Task<CustomerBasket> UpdateBasketAsync(CustomerBasket basket)
        {
            basket.TotalPrice = 0;
            foreach (var item in basket.Items)
            {
                basket.TotalPrice += item.Price * item.Quantity;
            }
            var created = await _database.StringSetAsync(basket.Id, JsonSerializer.Serialize(basket),
               TimeSpan.FromDays(30));

            if (!created) return null;

            return await GetBasketAsync(basket.Id);
        }
    }
}
