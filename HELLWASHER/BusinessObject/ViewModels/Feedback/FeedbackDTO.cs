using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.ViewModels.Feedback
{
    public class FeedbackDTO
    {
        public int FeedbackId { get; set; }
        public int UserId { get; set; }
        public int? WashServiceId { get; set; }
        public int? ProductId { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; }
    }
}
