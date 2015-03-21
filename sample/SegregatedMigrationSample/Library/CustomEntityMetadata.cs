using System.ComponentModel.DataAnnotations;
using CcAcca.BaseLibrary;

namespace CcAcca.Library
{
    public class CustomEntityMetadata : EntityMetadata
    {
        [StringLength(255)]
        public string Description { get; set; }
    }
}