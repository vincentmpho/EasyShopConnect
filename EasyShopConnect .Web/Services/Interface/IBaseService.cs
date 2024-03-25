using EasyShopConnect_.Web.Models.DTOs;

namespace EasyShopConnect_.Web.Services.Interface
{
    public interface IBaseService
    {
       Task<ResponseDto?> SendAsync(RequestDto requestDto);
    }
}
