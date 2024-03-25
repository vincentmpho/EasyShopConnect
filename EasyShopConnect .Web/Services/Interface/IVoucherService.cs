using EasyShopConnect_.Web.Models.DTOs;

namespace EasyShopConnect_.Web.Services.Interface
{
    public interface IVoucherService
    {
        Task<ResponseDto?> GetVoucherAsync(string voucherCode);
        Task<ResponseDto?> GetAllVoucherAsync();
        Task<ResponseDto?> GetVoucherByIdAsync(int id);
        Task<ResponseDto?> CreateVoucherAsync(VoucherDto voucherDto);
        Task<ResponseDto?> UpdateVoucherAsync(VoucherDto voucherDto);
        Task<ResponseDto?> DeleteVoucherAsync(int id);
       
    }
}
