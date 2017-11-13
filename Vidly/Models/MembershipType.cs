using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Vidly.Models
{
    public class MembershipType
    {
        public byte Id { get; set; }
        [StringLength(50)]
        public string Name { get; set; }
        public short SignUpFee { get; set; }
        public byte DurationInMonths { get; set; }  /* value from 1 to 12 */
        public byte DiscountRate { get; set; } /* value from 0 to 100 */

        //add these to prevent magic numbers in validation parts => regarded as referrence code
        //Alternative way is using enum here => but need to cast to the property's type every time
        public static readonly byte Unknown = 0;
        public static readonly byte PayAsYouGo = 1;

    }
}