﻿using ShoppingApp.Model.Domain;
using System;
using System.Collections.Generic;

namespace ShoppingApp.Model
{
    public class OrderAndPaymentResponse
    {
        public int OrderId { get; set; }

        public string TokenUserId { get; set; }

        public List<Product> Products { get; set; }

        public UserDetails UserDetail { get; set; }

        public int TotalQuantity { get; set; }

        public string PaymentType { get; set; }

        public DateTime OrderDate { get; set; }
    }
}
