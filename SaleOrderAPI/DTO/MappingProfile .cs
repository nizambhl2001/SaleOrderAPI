using AutoMapper;
using SaleOrderAPI.Models;
using SaleOrderAPI.ViewModels;

namespace SaleOrderAPI.DTO
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
           
            CreateMap<TblCustomer, CustomerDto>().ForMember(item=>item.StatusName,opt=>opt.MapFrom(src=>src.IsActive == true ? "Active": "Inactive")) .ReverseMap();
            CreateMap<TblSalesHeader, InvoiceHeader>().ReverseMap();
            CreateMap<TblSalesProductInfo, InvoiceDetails>().ReverseMap();
            CreateMap<InvoiceInput, TblSalesHeader>().ReverseMap();
            
        }
    }
}
