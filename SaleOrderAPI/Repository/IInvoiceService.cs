using SaleOrderAPI.ViewModels;

namespace SaleOrderAPI.Repository
{
    public interface IInvoiceService
    {
        Task<List<InvoiceHeader>> GetAllInviceHeader();
        Task<InvoiceHeader> GetAllInviceHeaderByCode(string invoice);
        Task<List<InvoiceDetails>> GetAllInvicDetailsByCode(string invoice);
        Task<ResponsType> Save(InvoiceEntity invoice);
        Task<ResponsType> Remove(string invoice);

    }
}
