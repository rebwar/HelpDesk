using HelpDesk.MVC.Models.Users;

namespace HelpDesk.MVC.Models.Articles
{
    public class Statistics
    {
        public int Articles { get; set; }
        public int Users { get; set; }
        public int PageVisited { get; set; }
        public int Categories { get; set; }
        public int UserId { get; set; }
        public ApplicationUsers User { get; set; }

    }
}
