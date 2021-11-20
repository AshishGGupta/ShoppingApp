namespace ShoppingApp.Common
{
    using ShoppingApp.Model;
    using ShoppingApp.Model.Domain;
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    public class ProductSortAndFilter
    {
        public IQueryable<Product> GetSortAndFilterQuery(IQueryable<Product> query, SortAndFilter sort)
        {
            ProductTypeEnum productValue;
            if (sort != null && query != null)
            {
                if (!string.IsNullOrEmpty(sort.SortBy))
                {
                    Enum.TryParse(sort.SortBy, out productValue);
                    query = sort.SortOrder == "desc"
                            ? query.OrderByDescending(GetSortQuery(productValue))
                            : query.OrderBy(GetSortQuery(productValue));
                }

                if (!string.IsNullOrEmpty(sort.FilterBy))
                {
                    Enum.TryParse(sort.FilterBy, out productValue);
                    query = GetFilterQuery(query, productValue, sort);
                }

            }
            return query;
        }

        Expression<Func<Product, object>> GetSortQuery(ProductTypeEnum value)
        {
            Expression<Func<Product, object>> productExp = null;
            switch (value)
            {
                case ProductTypeEnum.Name:
                    productExp = x => x.Name;
                    break;
                case ProductTypeEnum.Price:
                    productExp = x => x.Price;
                    break;
                case ProductTypeEnum.Category:
                    productExp = x => x.Category;
                    break;
                case ProductTypeEnum.DateAdded:
                    productExp = x => x.DateAdded;
                    break;
                case ProductTypeEnum.ExpiryDate:
                    productExp = x => x.ExpiryDate;
                    break;
                default:
                    productExp = x => x.Name;
                    break;
            }
            return productExp;
        }

        IQueryable<Product> GetFilterQuery(IQueryable<Product> query, ProductTypeEnum value, SortAndFilter sort)
        {
            switch (value)
            {
                case ProductTypeEnum.Name:
                    query = query.Where(x => x.Name.Contains(sort.FilterValue));
                    break;
                case ProductTypeEnum.Price:
                    string[] s = sort.FilterValue.Split('-');
                    query = query.Where(x => x.Price >= Convert.ToInt32(s[0]) && x.Price <= Convert.ToInt32(s[1]));
                    break;
                case ProductTypeEnum.Category:
                    query = query.Where(x => x.Category == sort.FilterValue);
                    break;
            }
            return query;
        }
    }
}
