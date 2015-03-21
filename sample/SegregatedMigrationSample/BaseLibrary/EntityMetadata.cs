using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CcAcca.BaseLibrary
{
    public class EntityMetadata
    {
        private ICollection<EntityPropertyMetadata> _properties = new List<EntityPropertyMetadata>();

        public int Id { get; set; }

        [Required]
        public string EntityName { get; set; }

        public virtual ICollection<EntityPropertyMetadata> Properties
        {
            get { return _properties; }
            set { _properties = value; }
        }
    }
}