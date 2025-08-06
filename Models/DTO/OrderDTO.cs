using System.Runtime.InteropServices;

namespace ProvaPub.Models.DTO
{
    public class OrderDTO
    {
        public int Id { get; set; }
        public decimal Value { get; set; }
        public int CustomerId { get; set; }
        public string OrderDate { get; set; }
        public Customer Customer { get; set; }

        public static OrderDTO EntityToDTO(Order order)
        {
            var timezoneId = RuntimeInformation.IsOSPlatform(OSPlatform.Windows)
                                ? "E. South America Standard Time"
                                : "America/Sao_Paulo";

            var brasilTimeZone = TimeZoneInfo.FindSystemTimeZoneById(timezoneId);
            var orderDate = TimeZoneInfo.ConvertTimeFromUtc(order.OrderDate, brasilTimeZone);

            return new OrderDTO
            {
                Id = order.Id,
                Value = order.Value,
                CustomerId = order.CustomerId,
                OrderDate = orderDate.ToString("dd/MM/yyyy HH:mm"),
                Customer = order.Customer
            };
        }
    }
}
