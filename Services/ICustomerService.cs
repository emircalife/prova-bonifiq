using ProvaPub.Models;

namespace ProvaPub.Services
{
    public interface ICustomerService
    {
        CustomerList ListCustomers(int page);
    }
}
