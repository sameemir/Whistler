using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Whistler.Model
{
    public class Consumer
    {
        public string bgURL { get; set; }
        public int duration { get; set; }
        public string prompt { get; set; }
    }

    public class SubCategory
    {
        public string name { get; set; }
        public string label { get; set; }
        public string image { get; set; }
        public string status { get; set; }
    }
    public class Provider
    {
        public string prompt { get; set; }
        public int duration { get; set; }
        public string bgURL { get; set; }
    }

    public class Category
    {
        public string _id { get; set; }
        public string lastUpdatedAt { get; set; }
        public string image { get; set; }
        public int index { get; set; }
        public string name { get; set; }
        public string label { get; set; }
        public List<SubCategory> subcategories { get; set; }
        public string status { get; set; }
        public Consumer consumer { get; set; }
        public Provider provider { get; set; }
    }

    public class CategoryModel
    {
        public CategoryModel()
        {
            categories = new List<Category>();
        }

        public List<Category> categories { get; set; }
    }
}
