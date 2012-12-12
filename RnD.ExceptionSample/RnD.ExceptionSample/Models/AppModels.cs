using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace RnD.ExceptionSample.Models
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }

        [DisplayName("Category Name")]
        [Required(ErrorMessage = "Name is required")]
        [MaxLength(200)]
        public string Name { get; set; }
    }

    public class Product
    {
        [Key]
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

        [ForeignKey("CategoryId")]
        public virtual Category Category { get; set; }
    }

    public class Logger
    {
        [Key]
        public int LoggerId { get; set; }

        [MaxLength(200)]
        public string Summery { get; set; }

        [MaxLength(200)]
        public string Details { get; set; }

        [MaxLength(200)]
        public string FilePath { get; set; }

        [MaxLength(200)]
        public string Url { get; set; }

        public int LoggerTypeId { get; set; }
        [ForeignKey("LoggerTypeId")]
        public virtual LoggerType LoggerType { get; set; }
    }

    public class LoggerType
    {
        [Key]
        public int LoggerTypeId { get; set; }

        [MaxLength(200)]
        public string LoggerTypeName { get; set; }
    }
}