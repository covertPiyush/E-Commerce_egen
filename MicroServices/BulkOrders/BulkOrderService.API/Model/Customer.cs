﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BulkOrderService.API.Model
{
    public class Customer
    {
        public int CustomerId { get; set; }
        public string FirstName { get; set; }

        public string Lastname { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }


    }
}
