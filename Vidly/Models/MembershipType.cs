using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Vidly.Models
{
    public class MembershipType
    {
        public byte Id { get; set; }
        public short SignUpFee { get; set; }
        public byte DurationInMonths { get; set; }  /* value from 1 to 12 */
        public byte DiscountRate { get; set; } /* value from 0 to 100 */

    }
}