﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace ShoppingMall.Models
{
    public partial class Bill
    {
        public Bill()
        {
            BillDetail = new HashSet<BillDetail>();
        }

        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime CreateDateTime { get; set; }
        public DateTime? PaymentDateTime { get; set; }

        public virtual User User { get; set; }
        public virtual ICollection<BillDetail> BillDetail { get; set; }
    }
}