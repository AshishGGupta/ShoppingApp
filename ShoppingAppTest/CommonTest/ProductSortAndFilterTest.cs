namespace ShoppingAppTest.CommonTest
{
    using ShoppingApp.Common;
    using ShoppingApp.Models.Domain;
    using ShoppingApp.Models.Model;
    using ShoppingAppTest.Common;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Xunit;

    public class ProductSortAndFilterTest
    {
        private readonly ProductSortAndFilter sortAndFilter = new ProductSortAndFilter();
        private readonly GetData getData = new GetData();

        [Theory]
        [InlineData("Name", "asc", "Name", "samsung")]
        [InlineData("Price", "desc", "Price", "1-20000")]
        [InlineData("Category", "asc", "Category", "Mobile")]
        [InlineData("DateAdded", "desc", "Name", "a")]
        [InlineData("ExpiryDate", "asc", "Name", "a")]
        public void SortByNameAndFilterByName(string sortBy, string sortOrder, string filterBy, string filterValue)
        {
            //arrange
            IQueryable<Product> query = Enumerable.Empty<Product>().AsQueryable();
            SortAndFilter sortFilter = new SortAndFilter()
            {
                SortBy = sortBy,
                SortOrder = sortOrder,
                FilterBy = filterBy,
                FilterValue = filterValue
            };
            //Act
            var v = sortAndFilter.GetSortAndFilterQuery(query, sortFilter);
            //Assert
            Assert.Empty(v.ToList());
        }
    }
}
