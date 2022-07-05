using Datahub.Core.EFCore;
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

        public const string ONGOING = "Ongoing";
        public const string CLOSED = "Closed";
        public const string ON_HOLD = "On Hold";

        [Key]
        [AeFormIgnore]
        public int Engagement_ID { get; set; }

        [Required]
        [AeLabel(validValues: new[] { ONGOING, CLOSED, ON_HOLD })]
        public string Engagement_Status { get; set; }
        public Datahub_Project Project { get; set; }

        [AeFormIgnore]
        [Timestamp]
        public byte[] Timestamp { get; set; }
    }

    public class Datahub_EngagementComment
    {
        [Key]
        [AeFormIgnore]
        public int Comment_ID { get; set; }

        public DateTime Comment_Date_DT { get; set; }

        public string Comment_NT { get; set; }
        public Datahub_Engagement Engagement { get; set; }

        [AeFormIgnore]
        [Timestamp]
        public byte[] Timestamp { get; set; }
    }
}
