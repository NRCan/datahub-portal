using System.ComponentModel.DataAnnotations;

namespace Datahub.Core.EFCore;

public class Datahub_Project_Sectors_And_Branches
{
    [Key]
    public int SectorAndBranchS_ID { get; set; }

    public int Organization_ID { get; set; }

    [StringLength(4000)]
    public string Full_Acronym_E { get; set; }
    [StringLength(4000)]
    public string Full_Acronym_F { get; set; }
    [StringLength(4000)]
    public string Org_Acronym_E { get; set; }
    [StringLength(4000)]
    public string Org_Acronym_F { get; set; }
    [StringLength(4000)]
    public string Org_Name_E { get; set; }
    [StringLength(4000)]
    public string Org_Name_F { get; set; }
    [StringLength(1)]
    public string Org_Level { get; set; }
    public int? Superior_OrgId { get; set; }
}