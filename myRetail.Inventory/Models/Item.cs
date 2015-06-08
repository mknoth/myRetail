using System.Collections.Generic;

namespace myRetail.Inventory.Models
{
    public class Item
    {

        public string Title { get; set; }

        public string Image { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public bool Online { get; set; }
        public int Quantity { get; set; }
        public List<string> Details { get; set; }
   
    }


    public class FactoryItem
    {

        public static List<Item> CreateItemList()
        {
            List<Item> list = new List<Item>();

            int upper = 100;
            for (int i = 0; i < upper; i++)
            {
                var item = CreateItem();
                item.Title = "item-" + string.Format("{0:000}", i); ;
                item.Price =  (upper - i) + 1;
                item.Details.Add("Lorem ipsum dolor sit amet." + i);
                list.Add(item);
            }
            return list;
        }



        /// <summary>
        /// Generates a sample item. 
        /// </summary>
        /// <returns></returns>
        public static Item CreateItem()
        {
            var i = new Item()
            {
                Title = "item-1",
                Image = "sample.jpg",
                Description = "Lorem ipsum dolor sit amet.",
                Price = 1,
                Online = true,
                Quantity = 1,
                Details = new List<string>()
            };

            i.Details.Add("Lorem ipsum dolor sit amet, consectetuer adipiscing elit.");
            i.Details.Add("in, viverra quis, feugiat a, tellus. Phasellus vi.");
            i.Details.Add("quis enim. Donec pede justo.");
            i.Details.Add("Sed consequat, leo eget bibendum sodales, augue velit cursus nun.");
            i.Details.Add("Nam quam nunc, blandit vel, luctu.");
            return i;
        }

    }
}