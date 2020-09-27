using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication17.Models
{
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
       //This allows null
        public string? Phone { get; set; }
        public string? Email { get; set; }    //This property was added later
        public ICollection<Post> Posts { get; set; }  //Navigation Property  i.e help internal link to the other class

}
}
