using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RnD.ExceptionSample.ViewModels
{
    public class CategoryTableModels
    {
        public string CategoryId { get; set; }
        public string Name { get; set; }
    }

    public class ProductTableModels
    {
        public string ProductId { get; set; }
        public string Name { get; set; }
        public string Price { get; set; }
        public string CategoryId { get; set; }
        public string CategoryName { get; set; }
    }

    public class LoggerTableModels
    {
        public string LoggerId { get; set; }
        public string Summery { get; set; }
        public string Details { get; set; }
        public string FilePath { get; set; }
        public string Url { get; set; }
        public string LoggerTypeName { get; set; }
    }
}