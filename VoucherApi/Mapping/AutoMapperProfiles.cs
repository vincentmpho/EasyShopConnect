using AutoMapper;
using VoucherApi.Models.DTOs;
using VoucherApi.Models;

namespace VoucherApi.Mapping
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            // Mapping from Voucher to VoucherDto
            CreateMap<Voucher, VoucherDto>().ReverseMap(); 
        }
    }
}
