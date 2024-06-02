using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SaleOrderAPI.Models;
using SaleOrderAPI.Repository;
using SaleOrderAPI.ViewModels;

namespace SaleOrderAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VourcherController : ControllerBase
    {

        private readonly IVoucherContainer _container;
        
        public VourcherController(IVoucherContainer container)
        {
            this._container = container;
          
        }

        [HttpGet("GetAllHeader")]
        public async Task<List<InvoiceHeader>> GetAllHeader()
        {
            return await this._container.GetAllInvoiceHeader();

        }
        [HttpGet("GetAllHeaderbyCode")]
        public async Task<InvoiceHeader> GetAllHeaderbyCode(string invoiceno)
        {
            return await this._container.GetAllInvoiceHeaderbyCode(invoiceno);

        }

        [HttpGet("GetAllDetailbyCode")]
        public async Task<List<InvoiceDetails>> GetAllDetailbyCode(string invoiceno)
        {
            return await this._container.GetAllInvoiceDetailbyCode(invoiceno);

        }

        [HttpPost("Save")]
        public async Task<ResponsType> Save([FromBody] InvoiceInput invoiceEntity)
        {
            return await this._container.Save(invoiceEntity);

        }

        [HttpDelete("Remove")]
        public async Task<ResponsType> Remove(string InvoiceNo)
        {
            return await this._container.Remove(InvoiceNo);

        }




    }
}
