using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SaleOrderAPI.DTO;
using SaleOrderAPI.Models;
using SaleOrderAPI.Repository;

namespace SaleOrderAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerService service;

        public CustomersController(ICustomerService service)
        {
            this.service = service;
        }

      

        [HttpGet("GetTodoItems")]
        public async Task<ActionResult<CustomerDto>> GetTodoItems()
        {
            var result = await this.service.GetCustomerList();
            return Ok(result);
        }


    }
}
