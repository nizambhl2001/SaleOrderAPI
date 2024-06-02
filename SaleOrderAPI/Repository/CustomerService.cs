using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SaleOrderAPI.DTO;
using SaleOrderAPI.Models;
using SaleOrderAPI.Repository;
namespace SaleOrderAPI.Repository
{
    public class CustomerService : ICustomerService
    {
        private readonly Sales_DBContext dBContext;
        private readonly IMapper mapper;

 
        public CustomerService(Sales_DBContext _DBContext,IMapper mapper)
        {
            dBContext = _DBContext;
            this.mapper = mapper;
        }


         public async Task<List<CustomerDto>> GetCustomerList()
        {
            var customerData = await dBContext.TblCustomers.ToListAsync();
            if (customerData != null && customerData.Count > 0)
            {
                return this.mapper.Map<List<TblCustomer>, List<CustomerDto>>(customerData);
            }

            return new List<CustomerDto>();
        }
    }
}
