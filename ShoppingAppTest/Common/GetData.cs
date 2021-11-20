﻿using ShoppingApp.Model;
using ShoppingApp.Model.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingAppTest.Common
{
    public class GetData
    {
        public CartResponse GetCartResponseData()
        {
            return new CartResponse( GetProductQuantityData(), GetUserDetailsData(), "Data Found Successfully");
        }

        public List<ProductQuantity> GetProductQuantityData()
        {
            return new List<ProductQuantity>()
            {
                new ProductQuantity()
                {
                    Quantity = 2,
                    CartId = 1,
                    ProductExpired = false,
                    Product = GetProductsData().FirstOrDefault()
                }
            };
        }

        public List<UserDetails> GetUserDetailsData()
        {
            return new List<UserDetails>()
            {
                new UserDetails()
                {
                    Id = 1,
                    Address = "324 mumbai -243223",
                    PhoneNumber = "123321123",
                    TokenUserId = "user123"
                },
                new UserDetails()
                {
                    Id = 1,
                    Address = "324 Delhi -243223",
                    PhoneNumber = "78876678876",
                    TokenUserId = "user123"
                }
            };
        }

        public List<Product> GetProductsData()
        {
            return new List<Product>()
            {
                new Product()
                {
                    ProductId = 1,
                    Name = "Samsung",
                    Description = "mobile",
                    Category = "Mobile",
                    DateAdded = DateTime.Now,
                    Price = 10000,
                    ExpiryDate = DateTime.Now.AddDays(10),
                },
                new Product()
                {
                    ProductId = 2,
                    Name = "Moto",
                    Description = "mobile",
                    Category = "Mobile",
                    DateAdded = DateTime.Now,
                    Price = 10000,
                    ExpiryDate = DateTime.Now.AddDays(10),
                },
            };
        }

        public List<Cart> GetCartData()
        {
            return new List<Cart>()
            {
                new Cart()
                {
                    CartId = 1,
                    ProductId = 1,
                    Quantity = "2",
                    TokenUserId = "user123",
                    Product = GetProductsData().FirstOrDefault(x => x.ProductId == 1),
                },
                new Cart()
                {
                    CartId = 2,
                    ProductId = 2,
                    Quantity = "2",
                    TokenUserId = "user123",
                    Product = GetProductsData().FirstOrDefault(x => x.ProductId == 2)
                }
            };
        }

        public SortAndFilter GetSortAndFilterData()
        {
            return new SortAndFilter()
            {
                SortBy = "Name",
                SortOrder = "asc",
                FilterBy = "Category",
                FilterValue = "Mobile"
            };
        }

        public UserModel GetUserModelData()
        {
            return new UserModel()
            {
                UserName = "user123",
                Password = "pass123321"
            };
        }

        public AuthResponse GetAuthResponseData()
        {
            return new AuthResponse()
            {
                Email = "user123",
                _Id = "user123",
                Access_token = "user123",
                Email_verified = "true"
            };
        }
    }
}