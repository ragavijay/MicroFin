using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MicroFin.Models
{
    public class UserCashReceiptStatement
    {
       
       
        public string UserId { get; set; }
        public string UserName { get; set; }
        public int Amount { get; set; }
    }
}