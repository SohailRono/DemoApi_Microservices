using DemoNet.Api.Models.Common;

namespace DemoNet.Api.Models.Entities
{
    public class CustomerInfo : EntityBase
    {
        public string CustId { get; set; }

        public string CustName { get; set; }

        public string Phone { get; set; }

        public string? Email { get; set; }

        public string? Address { get; set; }

        public string? BnName { get; set; }

        public bool? IsActive { get; set; } = false;

        public string? PackCode { get; set; }

        public decimal? BillAmt { get; set; }

        public string? Line { get; set; }

        public string? Ip { get; set; }

        public string? AreaCode { get; set; }

        public string? Remark { get; set; }
    }
}
