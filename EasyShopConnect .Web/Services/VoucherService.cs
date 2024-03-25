using EasyShopConnect_.Web.Models.DTOs;
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
        public Task<ResponseDto?> CreateVoucherAsync(VoucherDto voucherDto)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseDto?> DeleteVoucherAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseDto?> GetAllVoucherAsync()
        {
            throw new NotImplementedException();
        }

        public Task<ResponseDto?> GetVoucherAsync(string voucherCode)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseDto?> GetVoucherByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseDto?> UpdateVoucherAsync(VoucherDto voucherDto)
        {
            throw new NotImplementedException();
        }
    }
}
