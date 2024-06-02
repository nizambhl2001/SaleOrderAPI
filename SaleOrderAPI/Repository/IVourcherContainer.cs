using SaleOrderAPI.ViewModels;

namespace SaleOrderAPI.Repository
{
    public interface IVoucherContainer
    {
        Task<List<InvoiceHeader>> GetAllInvoiceHeader();
        Task<InvoiceHeader> GetAllInvoiceHeaderbyCode(string invoiceno);
        Task<List<InvoiceDetails>> GetAllInvoiceDetailbyCode(string invoiceno);
        Task<ResponsType> Save(InvoiceInput invoiceEntity);
        Task<ResponsType> Remove(string invoiceno);
    }

}
