using System.ComponentModel.DataAnnotations;

namespace VoucherApi.Models
{
    public class Voucher
    {
        [Key]
        public int VoucherId  { get; set; }
        [Required]
        public string VoucherCode { get; set; }
        [Required]
        public double DiscountAmount  { get; set; }
        public int MinAmount { get; set; }
    }
}
