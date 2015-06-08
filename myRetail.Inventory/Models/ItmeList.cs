using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace myRetail.Inventory.Models
{
    public class ItmeList
    {
        public List<Item> Items { get; set; }

        public int TotalItemCount { get; set; }
    }
}