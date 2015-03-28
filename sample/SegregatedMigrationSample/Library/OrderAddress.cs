using System.ComponentModel.DataAnnotations;
using CcAcca.BaseLibrary;

namespace CcAcca.Library
{
    public class OrderAddress
    {
        public int Id { get; set; }

        [Required]
        public Address Address { get; set; }
        public int AddressId { get; set; }

        [Required]
        public Order Order { get; set; }
        public int OrderId { get; set; }

        public bool IsDefaultDeliveryAddress { get; set; }
    }
}