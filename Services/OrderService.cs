using ProvaPub.Enums;
using ProvaPub.Models;
using ProvaPub.Models.DTO;
using ProvaPub.Repository;

namespace ProvaPub.Services
{
	public class OrderService
	{
        TestDbContext _ctx;

        public OrderService(TestDbContext ctx)
        {
            _ctx = ctx;
        }

        public async Task<OrderDTO> PayOrder(string paymentMethod, decimal paymentValue, int customerId)
        {
            if (Enum.TryParse(paymentMethod.ToUpper(), true, out PaymentMethodEnum paymentMethodEnum))
            {
                switch (paymentMethodEnum)
                {
                    case PaymentMethodEnum.PIX:
                        //Executa pedido com PIX
                        break;
                    case PaymentMethodEnum.CREDITCARD:
                        //Executa pedido com CREDITCARD
                        break;
                    case PaymentMethodEnum.PAYPAL:
                        //Executa pedido com PAYPAL
                        break;
                }
            }
            else
            {
                throw new PaymentMethodException();
            }

            Order order = new Order();

            order.Value = paymentValue;
            order.CustomerId = customerId;
            order.OrderDate = DateTime.UtcNow;

            Task<OrderDTO> tskOrderDTO = InsertOrder(order);

            return tskOrderDTO.Result;
		}

		public async Task<OrderDTO> InsertOrder(Order order)
        {
            Order orderNew = (await _ctx.Orders.AddAsync(order)).Entity;

            return OrderDTO.EntityToDTO(orderNew);
        }
	}
}
