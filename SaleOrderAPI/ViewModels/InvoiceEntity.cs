namespace SaleOrderAPI.ViewModels
{
    public class InvoiceEntity
    {
        public InvoiceHeader? header { get; set; }
        public List<InvoiceDetails>? details { get; set; }
    }
}
