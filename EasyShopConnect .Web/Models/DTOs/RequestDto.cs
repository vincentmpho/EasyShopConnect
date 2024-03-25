using static EasyShopConnect_.Web.Models.Utility.StaticDetails;

namespace EasyShopConnect_.Web.Models.DTOs
{
    public class RequestDto
    {
        public ApiType ApiType { get; set; }
        public string Url  { get; set; } 
        public object Data  { get; set; } 
        public string AccessToken  { get; set; } 
    }
}
