using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication17.Models
{
    public class PostCategory
    {
        public PostCategory(int CategoryId)
        {
            this.CategoryId = CategoryId;
        
        }
        public int Id { get; set; }

        public int PostId { get; set; }
        public int CategoryId { get; set; }

        public Post Post { get; set; }   //Navigation Property
        public Category Category { get; set; }   //Navigation Property 
    }
}
