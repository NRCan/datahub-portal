﻿using System.ComponentModel.DataAnnotations;
using Elemental.Components;

namespace Datahub.Finance.Data;

public class HierarchyLevel
{
    [Key]
    [AeFormIgnore]
    public int HierarchyLevelID { get; set; }
    [Required]
    [MaxLength(20)]
    public string FCCode { get; set; }
    [Required]
    [MaxLength(20)] 
    public string ParentCode { get; set; }
    public string FundCenterNameEnglish { get; set; }
    public string FundCenterNameFrench { get; set; }

    public string FundCenterModifiedEnglish { get; set; }
    public string FundCenterModifiedFrench { get; set; }
        
    public List<FundCenter> SectorFundCenters { get; set; }
    public List<FundCenter> BranchFundCenters { get; set; }
    public List<FundCenter> DivisionFundCenters { get; set; }

    public int Level { get; set; }
    public DateTime ExpiryDate { get; set; }
    public bool IsActive { get; set; }

        
}