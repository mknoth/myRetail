using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using myRetail.Inventory.Models;
using myRetail.Inventory.Code;


namespace myRetail.Inventory.Controllers
{



    public class InventoryController : ApiController
    {
        // GET api/<controller>
        public Item Get(string title)
        {
            IQueryable<Item> items = FactoryItem.CreateItemList().AsQueryable();

            // searching{
            title = title.ToLower();
            return items.Where(x => x.Title.ToLower() == title).FirstOrDefault();
             
        }

        // GET api/<controller>/5
        public ItmeList Get([FromUri] dynamic parameters)
        {

            //because I am passing a list of oobject need to parse out what is being passed in off the parms.
            var parseParms = Newtonsoft.Json.JsonConvert.DeserializeObject(parameters);
            SearchParameters searchInfo = new SearchParameters()
            {
                ItemsPerPage = parseParms.SelectToken(Constants.ItemsPerPage),
                Page = parseParms.SelectToken(Constants.Page),
                SearchItem = parseParms.SelectToken(Constants.SearchProduct)
            };
            searchInfo.Sort = new List<Sort>();
            var sorts = parseParms.SelectToken(Constants.Sort);
            foreach (var sort in sorts)
            {
                Sort s = new Sort()
                {
                    field = sort.SelectToken(Constants.Field),
                    reverse = sort.SelectToken(Constants.Reverse),
                };
                searchInfo.Sort.Add(s);
            }

            //create list            
            var list = FactoryItem.CreateItemList().AsQueryable();

            //create IQueryable for getting info ready.
            IQueryable<Item> items = null;

            // searching
            if (!string.IsNullOrWhiteSpace(searchInfo.SearchItem))
            {
                searchInfo.SearchItem = searchInfo.SearchItem.ToLower();
                items = list.Where(x => x.Title.ToLower() == searchInfo.SearchItem);
            }
            else
            {
                items = list;
            }

            //get total row count before doing paging
            var results = new ItmeList()
            {
                TotalItemCount = items.Count()
            };

            //build srt string and tax on defualt sort.
            var sortString = "";
            if (searchInfo.Sort.Count() > 0)
            {
                foreach (var s in searchInfo.Sort)
                {
                    sortString = sortString + ", " + s.field + (s.reverse ? " descending" : "");
                }
                sortString = sortString.Substring(1);
            }
            else
            {
                //need to sort if we are paging. 
                sortString = "Title Ascending";
            }
            // sorting (done with the System.Linq.Dynamic library available on NuGet)
            items = items.OrderBy(sortString);

            // paging
            List<Item> listPages = items.Skip((searchInfo.Page - 1) * searchInfo.ItemsPerPage).Take(searchInfo.ItemsPerPage).ToList();
            results.Items = listPages;

            return results;
        }

    }
}