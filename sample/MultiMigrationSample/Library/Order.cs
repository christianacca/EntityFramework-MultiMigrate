using System;
using System.ComponentModel.DataAnnotations;
using CcAcca.BaseLibrary;

namespace CcAcca.Library
{
    public class Order
    {
        public int Id { get; set; }

        public DateTimeOffset OrderDate { get; set; }

        [Required]
        public string CustomerName { get; set; }

        public LookupItem OrderStatus { get; set; }

        public int OrderStatusId { get; set; }

        public CustomLookupItem OrderRecommendation { get; set; }

        public int OrderRecommendationId { get; set; }
    }
}