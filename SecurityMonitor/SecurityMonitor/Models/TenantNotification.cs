using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SecurityMonitor.Models
{
    public class TenantNotification
    {
        public string RequestedDate { get; set; }
        public string TicketNumber { get; set; }
        public string NotificationMessage { get; set; }
    }
}
