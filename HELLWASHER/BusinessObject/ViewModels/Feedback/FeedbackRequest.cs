using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.ViewModels.Feedback
{
    public class FeedbackRequest
    {
        public int? WashServiceId { get; set; }
        public int? ProductId { get; set; }

        [Required]
        [MaxLength(500)]
        public string comment { get; set; }

        [Required]
        [Range(1, 5, ErrorMessage ="Rating must be from 1 to 5")]
        public int Rating { get; set; }
    }
}
