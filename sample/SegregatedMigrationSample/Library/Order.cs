using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using CcAcca.BaseLibrary;

namespace CcAcca.Library
{
    public class Order
    {
        public Order()
        {
            Addresses = new List<OrderAddress>();
        }

        public int Id { get; set; }

        public DateTimeOffset OrderDate { get; set; }

        [Required]
        public string CustomerName { get; set; }

        public LookupItem OrderStatus { get; set; }

        public int OrderStatusId { get; set; }

        public CustomLookupItem OrderRecommendation { get; set; }

        [Range(1, Int32.MaxValue)]
        public int OrderRecommendationId { get; set; }

        public IList<OrderAddress> Addresses { get; set; }
    }
}