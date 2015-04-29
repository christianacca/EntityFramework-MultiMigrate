using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CcAcca.BaseLibrary
{
    public class Lookup
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public virtual ICollection<LookupItem> Items { get; set; }
    }
}