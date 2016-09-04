using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Vidly.Models
{
    public class MembershipType
    {
        public string Name { get; set; }

        public short SignUpFee { get; set; }
        public byte DurationInMonths { get; set; }
        public byte DiscountRate { get; set; }
        //Every Entity must have a key which is mapped to the corresponding table in the database. This should be called Id like below:
        public byte Id { get; set; }

        public static readonly byte Unknown = 0;
        public static readonly byte PayAsYouGo = 1;


    }
}