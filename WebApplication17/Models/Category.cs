using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication17.Models
{
    public class Category
    {

 
        public int Id { get; set; }

        public string Title { get; set; }
        public ICollection<PostCategory> PostCategories { get; set; }  //Navigation Property 
    }
}
