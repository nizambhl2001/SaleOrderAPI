using SaleOrderAPI.DTO;
using SaleOrderAPI.Models;

namespace SaleOrderAPI.Repository
{
    public interface ICustomerService
    {
        Task<List<CustomerDto>> GetCustomerList();
    }
}
