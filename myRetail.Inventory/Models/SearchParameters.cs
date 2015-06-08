using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace myRetail.Inventory.Models
{

    public class Sort
    {
        public string field { get; set; }
        public bool reverse { get; set; }
    }

    public class SearchParameters
    {
        private string _searchItem;
        private int _page;
        private int _itemsPerPage;

        public SearchParameters()
        {
            Sort = new List<Sort>();
        }

        public SearchParameters(string searchItem, int page, int itemsPerPage, List<Sort> sort)
        {
            _searchItem = searchItem;
            _page = page;
            _itemsPerPage = itemsPerPage;
            Sort = sort;
        }

        public string SearchItem
        {
            get { return _searchItem; }
            set { _searchItem = value; }
        }

        public int Page
        {
            get { return _page; }
            set { _page = value; }
        }

        public int ItemsPerPage
        {
            get { return _itemsPerPage; }
            set { _itemsPerPage = value; }
        }

        public List<Sort> Sort { get; set; }
    }
}