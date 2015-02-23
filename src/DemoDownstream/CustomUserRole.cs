using System.ComponentModel.DataAnnotations;
using CcAcca.DemoUpstream;

namespace CcAcca.DemoDownstream
{
    public class CustomUserRole : UserRole
    {
        [StringLength(150)]
        public string CustomRoleProp { get; set; }
    }
}