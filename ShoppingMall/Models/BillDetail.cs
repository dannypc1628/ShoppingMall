﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace ShoppingMall.Models
{
    public partial class BillDetail
    {
        public Guid Id { get; set; }
        public Guid BillId { get; set; }
        public string Name { get; set; }
        public decimal UnitPrice { get; set; }
        public int Count { get; set; }

        public virtual Bill Bill { get; set; }
    }
}