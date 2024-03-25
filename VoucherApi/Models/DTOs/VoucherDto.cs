namespace VoucherApi.Models.DTOs
{
    public class VoucherDto
    {
        public int VoucherId { get; set; }
        public string VoucherCode { get; set; }
        public double DiscountAmount { get; set; }
        public int MinAmount { get; set; }
    }
}
