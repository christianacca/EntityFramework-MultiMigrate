using System.ComponentModel.DataAnnotations;

namespace CcAcca.BaseLibrary
{
    public class EntityPropertyMetadata
    {
        public int Id { get; set; }

        [Required]
        public string PropertyName { get; set; }

        public virtual EntityMetadata Entity { get; set; }
        public int EntityId { get; set; }
    }
}