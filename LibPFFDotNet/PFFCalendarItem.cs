using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibPFFDotNet
{
    public class PFFCalendarItem : PFFMessage
    {
        public PFFCalendarItem(IntPtr CalendarItemPtr)
            : base(CalendarItemPtr)
        {

        }

        public PFFCalendarItem(PFFMessage p)
            : base(p.Handler)
        {
        }

        public override unsafe string GetBody(BodyType PreferredType)
        {
            string location = GetMapiStringValue(mapi.EntryTypes.AppointmentLocation);
            string timezone = GetMapiStringValue(mapi.EntryTypes.AppointmentTimezone);
            return "<table>" +
                    "<tr><td>Location</td><td>" + location + "</td></tr>" +
                    "<tr><td>Timezone</td><td>" + timezone + "</td></tr>" +
                    "</table><hr>";

        }
    }
}
