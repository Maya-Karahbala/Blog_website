using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication17.Models
{
    public class PostViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Tags { get; set; }// can be modeled as category 
        public DateTime CreatingTime { get; set; }
        public int UserId { get; set; }

        public User User { get; set; }   //Navigation Property
        public List<int> categories { get; set; }  //Navigation Property 
    }
}
