using System.ComponentModel.DataAnnotations;
using CcAcca.DemoSharedModel;

namespace CcAcca.DemoModel
{
    public class CustomUserRole : UserRole
    {
        [StringLength(150)]
        public string CustomRoleProp { get; set; }
    }
}