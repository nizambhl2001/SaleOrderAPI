using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SaleOrderAPI.DTO;
using SaleOrderAPI.Models;
using SaleOrderAPI.ViewModels;

namespace SaleOrderAPI.Repository
{
    public class InoviceService : IInvoiceService
    {

        private readonly Sales_DBContext _dBContext;
        private readonly IMapper mapper;

        public InoviceService(Sales_DBContext dBContext,IMapper mapper)
        {
        
            _dBContext = dBContext;
            this.mapper = mapper;
        }
        public async Task<List<InvoiceDetails>> GetAllInvicDetailsByCode(string invoice)
        {
            var data = await _dBContext.TblSalesProductInfos.Where(item=>item.InvoiceNo == invoice).ToListAsync();
            if(data != null && data.Count > 0)
            {
                return mapper.Map<List<TblSalesProductInfo>, List<InvoiceDetails>>(data);
            }
            return new List<InvoiceDetails>();
        }

        public async Task<List<InvoiceHeader>> GetAllInviceHeader()
        {

               var data = await _dBContext.TblSalesHeaders.ToListAsync(); 
                if(data !=null && data.Count > 0)
                {
                    return this.mapper.Map<List<TblSalesHeader>, List<InvoiceHeader>>(data);
                }

                return new List<InvoiceHeader>();
            
        }

        public async Task<InvoiceHeader> GetAllInviceHeaderByCode(string invoice)
        {
            var data = await _dBContext.TblSalesHeaders.FirstOrDefaultAsync(item => item.InvoiceNo == invoice);
            if(data != null)
            {
                return mapper.Map<TblSalesHeader, InvoiceHeader>(data);
            }
            return new InvoiceHeader();
        }

        public async Task<ResponsType> Remove(string invoice)
        {
            try
            {
                var header = await _dBContext.TblSalesHeaders.FirstOrDefaultAsync(item => item.InvoiceNo == invoice);
                if (header != null)
                {
                    _dBContext.TblSalesHeaders.Remove(header);
                }


                var details = await _dBContext.TblSalesProductInfos.Where(item => item.InvoiceNo == invoice).ToListAsync();
                if (details != null && details.Count > 0)
                {
                    _dBContext.TblSalesProductInfos.RemoveRange(details);
                }
                return new ResponsType() { Result = "pass", KyValue = invoice};
            }
            catch(Exception ex)
            {
                throw ex;
            }
           
        }

        public async Task<ResponsType> Save(InvoiceEntity invoice)
        {
            string Result = string.Empty;
            int processcount = 0;
            var respons = new ResponsType();
            if (invoice != null)
            {
                using (var dbTrans = await this._dBContext.Database.BeginTransactionAsync())
                {
                    if (invoice.header != null) Result = await this.SaveHeader(invoice.header);

                    if (!string.IsNullOrEmpty(Result) && (invoice.details != null && invoice.details.Count > 0))
                    {
                        invoice.details.ForEach(item =>
                        {
                            bool saveresult = this.SaveDetails(item).Result;
                            if (saveresult)
                            {
                                processcount++;
                            }
                        });

                        if (invoice.details.Count == processcount)
                        {
                            await _dBContext.SaveChangesAsync();
                            await dbTrans.CommitAsync();
                            respons.Result = "Pass";
                            respons.Result = Result;
                        }
                        else
                        {
                            await dbTrans.RollbackAsync();
                            respons.Result = "fail";
                            respons.Result = string.Empty;
                        }
                    }
                }
            }
            else
            {
                return new ResponsType();
            }
            return respons;

        }  
        private async Task<string> SaveHeader(InvoiceHeader invoiceHeader)
        {
            string Results = string.Empty;
            try
            {
       
                TblSalesHeader _header = this.mapper.Map<InvoiceHeader, TblSalesHeader>(invoiceHeader);
                var header = await _dBContext.TblSalesHeaders.FirstOrDefaultAsync(item => item.InvoiceNo == invoiceHeader.InvoiceNo);

                if(header != null)
                {
                    header.CustomerId = invoiceHeader.CustomerId;
                    header.CustomerName = invoiceHeader.CustomerName;
                    header.DeliveryAddress = invoiceHeader.DeliveryAddress;
                    header.Total = invoiceHeader.Total;
                    header.Remarks = invoiceHeader.Remarks;
                    header.Tax = invoiceHeader.Tax;
                    header.NetTotal = invoiceHeader.NetTotal;
                    header.ModifyUser = invoiceHeader.CreateUser;
                    header.ModifyDate = invoiceHeader.CreateDate;

                    var data = await _dBContext.TblSalesProductInfos.Where(item => item.InvoiceNo == invoiceHeader.InvoiceNo).ToListAsync();
                    if (data != null && data.Count > 0)
                    {
                        _dBContext.TblSalesProductInfos.RemoveRange(data);
                    }
                }
                else
                {
                    await _dBContext.TblSalesHeaders.AddAsync(_header);
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
            return Results;

        }

        private async Task<bool> SaveDetails(InvoiceDetails invoiceDetails)
        {
           
            try
            {
                TblSalesProductInfo _Details = this.mapper.Map<InvoiceDetails, TblSalesProductInfo>(invoiceDetails);
                await _dBContext.TblSalesProductInfos.AddAsync(_Details);
                return true;
            }
            catch(Exception ex)
            {
                throw ex;
            }
 
        }
    }
}
