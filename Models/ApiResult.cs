﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Linq.Dynamic.Core;

namespace OnlineAuction.Models
{
    public class ApiResult<T>
    {
        ///<summary>
        ///Private constructor called by the CreateAsync method
        ///</summary>

        private ApiResult(List<T> data, int count, int pageIndex, int pageSize, string sortColumn, string sortOrder)
        {
            Data = data;
            PageIndex = pageIndex;
            PageSize = pageSize;
            TotalCount = count;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
            SortColumn = sortColumn;
            SortOrder = sortOrder;
        }
        #region Methods
        ///<summary>
        ///Pages and/or sorts an IQuerqble source
        /// </summary>
        /// <param name="source">An IQuerable source of generic type</param>
        /// <param name="pageIndex">Zero based current page index(0 = first page)</param>
        /// <param name="pageSize" > The actual size of each page</param>
        /// <param name="sortColumn">The sorting column name</param>
        /// <param name="sortOrder" > The sorting order("ASC" or "DESC")</param>
        /// <returns>An object containing the IQueryable paged/sorted result and 
        /// all the relevant paging/sorting navigation info</returns>

        public static async Task<ApiResult<T>> CreateAsync(IQueryable<T> source, int pageIndex, int pageSize,
            string sortColumn = null, string sortOrder = null)
        {
            var count = await source.CountAsync();
            if(!String.IsNullOrEmpty(sortColumn) && IsValidProperty(sortColumn))
            {
                sortOrder = !String.IsNullOrEmpty(sortOrder) && sortOrder.ToUpper() == "ASC" ? "ASC" : "DESC";
                source = source.OrderBy(String.Format("{0}{1}", sortColumn, sortOrder));
            }

            source = source
                .Skip(pageIndex * pageSize)
                .Take(pageSize);
            var data = await source.ToListAsync();
            return new ApiResult<T>(data, count, pageIndex, pageSize, sortColumn, sortOrder);
        }

        ///<summary>
        ///Checks if the given property name exists to protect agains SQL injection attacks</summary>
        ///
        public static bool IsValidProperty(string propertyName, bool throExceptionNotFound = true)
        {
            var prop = typeof(T).GetProperty(propertyName, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
            if(prop == null && throExceptionNotFound)
            {
                throw new NotSupportedException(String.Format("Error: Property '{0}' does not exist.", propertyName));
            }
            return prop != null;
        }
        #endregion

        #region properties
        ///<summary>The data result</summary>       
        public List<T> Data { get; private set; }

        ///<summary>Zero based index of current page</summary>      
        public int PageIndex { get; private set; }

        ///<summary>Number of items contained in each page</summary>      
        public int PageSize { get; private set; }

        ///<summary>Totals items count</summary>      
        public int TotalCount { get; private set; }

        ///<summary>Total pages count</summary>      
        public int TotalPages { get; private set; }

        ///<summary>True if the currrent page has a previous page, False otherwise</summary>      
        public bool HasPreviousPage
        {
            get
            {
                return (PageIndex > 0);
            }
        }

        ///<summary>True if the currrent page has a next page, False otherwise</summary>      
        public bool HasNextPage
        {
            get
            {
                return ((PageIndex + 1) < TotalPages);
            }
        }

        ///<summary>Sorting Column name(or null if none set)</summary>      
        public string SortColumn { get; set; }

        ///<summary>Sorting Order ("ASC","DESC" or null if none set)</summary>      
        public string SortOrder { get; set; }
        #endregion
    }
}