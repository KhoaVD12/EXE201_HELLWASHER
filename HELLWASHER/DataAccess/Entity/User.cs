﻿using DataAccess.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Entity
{
    public class User
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Phone { get; set; }
        public string Token { get; set; }
        public string Role { get; set; }
        public UserEnum Status { get; set; }

        public ICollection<Order> Orders { get; set; }
    }
}
