using System.ComponentModel.DataAnnotations;

namespace CcAcca.BaseLibrary
{
    public class AlternativeEntityMetadata : EntityMetadata
    {
        [Required]
        public string Reason { get; set; } 
    }
}