using Elemental.Components;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datahub.Core.EFCore
{
    public class Datahub_Engagement
    {
        [AeFormIgnore]
        [Key]
        public int EngagementID { get; set; }

        public DateTime? ReceivedDate { get; set; }
        public DateTime EngagementStart { get; set; }
        public DateTime EngagementEnd { get; set; }
        public DateTime? Release1Date { get; set; }
        public DateTime? Release2Date { get; set; }
        public DateTime? Release3Date { get; set; }

        [StringLength(50)]
        [AeLabel("Assyst Ticket Number", placeholder: "Assyst Ticket Number")]
        public string TicketNumber { get; set; }
        public Datahub_Project Project { get; set; }
    }
}
