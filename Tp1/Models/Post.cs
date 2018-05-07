using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tp1.Models
{
    public class Post
    {
        public string ID { get; set; }

        public User User { get; set; }

        public string Content { get; set; }

    }
}
