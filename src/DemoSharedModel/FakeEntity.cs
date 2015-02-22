using System.ComponentModel.DataAnnotations;

namespace CcAcca.DemoSharedModel
{
    public class FakeEntity
    {
        public int Id { get; set; }

        [StringLength(150)]
        public string Name { get; set; }
    }
}