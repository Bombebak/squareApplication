using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SquareApplication.Models
{
    public class selectedSetViewModel
    {
        public int set_id;
        public string name;
        public int price;
        public DateTime upload_date;
        public string user_name;
        public IList<String> tiles = new List<string>(); 
    }
}