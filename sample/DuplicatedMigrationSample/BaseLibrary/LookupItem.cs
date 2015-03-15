using System.ComponentModel.DataAnnotations;

namespace CcAcca.BaseLibrary
{
    public class LookupItem
    {
        public int Id { get; set; }

        [StringLength(50)]
        public string Code { get; set; }

        public string Description { get; set; }

        public virtual Lookup Lookup { get; set; }
        public int LookupId { get; set; }
    }
}