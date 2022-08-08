using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelApp.BLL.Models.AuditModels
{
    public class AuditCreateModel
    {
        public int EntityId { get; set; }
        public string EntityName { get; set; }
        public string ActionType { get; set; }
        public DateTime TimeStamp { get; set; }
        public string? OldValues { get; set; }
        public string? NewValues { get; set; }
    }
}
