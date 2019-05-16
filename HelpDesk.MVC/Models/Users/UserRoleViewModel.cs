using System.Collections.Generic;

namespace HelpDesk.MVC.Models.Users
{
    public class UserRoleViewModel
    {
        public ApplicationUsers User { get; set; }
        public List<string> Roles { get; set; }
        public List<ApplicaionRoles> ApplicaionRoles { get; set; }
    }
}
