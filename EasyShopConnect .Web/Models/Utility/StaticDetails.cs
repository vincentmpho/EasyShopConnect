namespace EasyShopConnect_.Web.Models.Utility
{
    public class StaticDetails
    {
        public static string VoucherAPIBase {  get; set; }
        public enum ApiType
        {
            GET,
            POST,
            PUT,
            DELETE
        }
    }
}
