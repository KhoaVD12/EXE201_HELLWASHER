﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Entity
{
    public class Feedback
    {
        public int FeedbackId { get; set; }
        public int UserId { get; set; }
        public int? WashServiceId { get; set; }
        public int? ProductId { get; set; }
        public string comment { get; set; }
        public int Rating { get; set; }
        public DateTime Date { get; set; }

        public User User { get; set; }
        public Service? WashService { get; set; }
        public Product? Product { get; set; }
    }
}
