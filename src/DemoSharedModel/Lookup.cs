using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CcAcca.DemoSharedModel
{
    [ReferenceData]
    public class Lookup
    {
        public int Id { get; set; }
        public ICollection<LookupItem> LookupItems { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [StringLength(30)]
        public string DisplayField { get; set; }
    }
}