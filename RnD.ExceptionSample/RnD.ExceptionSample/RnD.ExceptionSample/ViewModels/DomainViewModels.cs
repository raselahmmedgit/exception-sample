using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RnD.ExceptionSample.Models;
using System.ComponentModel;

namespace RnD.ExceptionSample.ViewModels
{
    public class CategoryViewModel
    {
        public int CategoryId { get; set; }

        [DisplayName("Category Name")]
        [Required(ErrorMessage = "Name is required")]
        [MaxLength(200)]
        public string Name { get; set; }

        public IEnumerable<Product> Products { get; set; }
    }

    public class ProductViewModel
    {
        public int ProductId { get; set; }

        [DisplayName("Product Name")]
        [Required(ErrorMessage = "Name is required.")]
        [MaxLength(200)]
        public string Name { get; set; }

        [DisplayName("Product Price")]
        [Required(ErrorMessage = "Price is required.")]
        public decimal Price { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Select one category.")]
        public int CategoryId { get; set; }

        [DisplayName("Category Name")]
        public string CategoryName { get; set; }

        public IEnumerable<SelectListItem> ddlCategories { get; set; }
    }
}