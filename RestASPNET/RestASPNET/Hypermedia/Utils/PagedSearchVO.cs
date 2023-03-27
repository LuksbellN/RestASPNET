﻿using RestASPNET.Hypermedia.Abstract;

namespace RestASPNET.Hypermedia.Utils
{
    public class PagedSearchVO<T> where T : ISupportsHypermedia
    {
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int TotalResults { get; set; }
        public string SortFields { get; set; }
        public string SortDirections { get; set; }
        public Dictionary<string, Object> Filters { get; set; }
        public List<T> Data { get; set; }

        public PagedSearchVO() { }

        public PagedSearchVO(int currentPage, int pageSize, string sortFields, string sortDirections)
        {
            CurrentPage = currentPage;
            PageSize = pageSize;
            SortFields = sortFields;
            SortDirections = sortDirections;
        }

        public PagedSearchVO(int currentPage, int pageSize, string sortFields, string sortDirections, Dictionary<string, object> filters) :
            this(currentPage, pageSize, sortFields, sortDirections)
        {
            Filters = filters;
        }

        public PagedSearchVO(int currentPage, string sortFields, string sortDirections) :
            this(currentPage, 10, sortFields, sortDirections)
        { }

        public int getCurrentPage()
        {
            return CurrentPage == 0 ? 2 : CurrentPage;
        }
        public int getPageSize()
        {
            return PageSize == 0 ? 10 : PageSize;
        }
    }
}
