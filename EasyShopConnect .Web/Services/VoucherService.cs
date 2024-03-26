using EasyShopConnect_.Web.Models.DTOs;
using EasyShopConnect_.Web.Models.Utility;
using EasyShopConnect_.Web.Services.Interface;

namespace EasyShopConnect_.Web.Services
{
    public class VoucherService : IVoucherService
    {
        private readonly IBaseService _baseService;

        public VoucherService(IBaseService baseService)
        {
            _baseService = baseService;
        }
        public async Task<ResponseDto?> CreateVoucherAsync(VoucherDto voucherDto)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = StaticDetails.ApiType.POST,
                Data = voucherDto,
                Url = StaticDetails.VoucherAPIBase + "api/voucher"
            });
        }

        public async Task<ResponseDto?> DeleteVoucherAsync(int id)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = StaticDetails.ApiType.DELETE,
                Url = StaticDetails.VoucherAPIBase + "api/voucher/" + id
            });
        }

        public async Task<ResponseDto?> GetAllVoucherAsync()
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = StaticDetails.ApiType.GET,
                Url = StaticDetails.VoucherAPIBase + "api/voucher"
            });
        }

        public async Task<ResponseDto?> GetVoucherAsync(string voucherCode)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = StaticDetails.ApiType.GET,
                Url = StaticDetails.VoucherAPIBase + "api/voucher/GetByCode/"+voucherCode
            });
        }

        public async Task<ResponseDto?> GetVoucherByIdAsync(int id)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = StaticDetails.ApiType.GET,
                Url = StaticDetails.VoucherAPIBase + "api/voucher/" + id
            });
        }

        public  async Task<ResponseDto?> UpdateVoucherAsync(VoucherDto voucherDto)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = StaticDetails.ApiType.PUT,
                Data = voucherDto,
                Url = StaticDetails.VoucherAPIBase + "api/voucher"
            });
        }
    }
}
