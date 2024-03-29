﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Elemental.Components;
using Microsoft.AspNetCore.Components;

namespace Datahub.PIP.Data;

public class PIP_Tombstone
{

    [Key]
    [AeFormIgnore]
    public int Tombstone_ID { get; set; }


    [MaxLength(20)]
    [AeFormIgnore]
    public string ProgramCode { get; set; }

    [AeFormIgnore]
    [Editable(false)] public int FiscalYearId { get; set; }

    [AeLabel(row: "1", column: "1")]
    [AeFormCategory("Program Information", 1)]
    [MaxLength(400)] public string Program_Title { get; set; }
    [AeFormCategory("Program Information", 1)]
    [AeLabel(row: "1", column: "2")]
    [MaxLength(400)] public string Lead_Sector { get; set; }
    [AeFormCategory("Program Information", 1)]
    [AeLabel(row: "1", column: "3")]
    [MaxLength(400)] public string Program_Official_Title { get; set; }

    [AeLabel(isDropDown: true, placeholder: "Please Select", row: "2", column: "1")]
    [AeFormCategory("Program Information", 1)]

    [MaxLength(400)]
    public string Core_Responsbility_DESC { get; set; }

    [AeLabel(size: 100, row: "3", column: "1")]
    [AeFormCategory("Program Information", 1)]
    public string Program_Inventory_Program_Description_URL { get; set; }

    [AeLabel(size: 100, row: "4", column: "1")]
    [AeFormCategory("Program Information", 1)]
    [MaxLength(400)]
    public string Logic_Model { get; set; }



    [AeFormIgnore]
    [AeLabel(row: "6", column: "1")]
    [AeFormCategory("Spending (in $) as per GC InfoBase (to be updated by CMSS)", 20)]
    [Column(TypeName = "Money")]
    public double? Planned_Spending_AMTL { get; set; }
    [AeFormIgnore]
    [AeLabel(row: "6", column: "2", placeholder: "to be updated by CMSS")]
    [AeFormCategory("Spending (in $) as per GC InfoBase (to be updated by CMSS)", 20)]
    [Column(TypeName = "Money")]
    public double? Actual_Spending_AMTL { get; set; }


    [AeLabel(isDropDown: true, placeholder: "Please Select", row: "8", column: "1")][AeFormCategory("Program Tags to be Updated by Sector", 50)][MaxLength(400)] public string Strategic_Priorities_1_DESC { get; set; }

    [AeLabel(isDropDown: true, placeholder: "Please Select", row: "9", column: "1")][AeFormCategory("Program Tags to be Updated by Sector", 50)][MaxLength(400)] public string Strategic_Priorities_2_DESC { get; set; }


    [AeLabel(isDropDown: true, placeholder: "Please Select", row: "10", column: "1")][AeFormCategory("Program Tags to be Updated by Sector", 50)][MaxLength(400)] public string Mandate_Letter_Commitment_1_DESC { get; set; }

    [AeLabel(isDropDown: true, placeholder: "Please Select", row: "11", column: "1")][AeFormCategory("Program Tags to be Updated by Sector", 50)][MaxLength(400)] public string Mandate_Letter_Commitment_2_DESC { get; set; }

    [AeLabel(isDropDown: true, placeholder: "Please Select", row: "12", column: "1")][AeFormCategory("Program Tags to be Updated by Sector", 50)][MaxLength(400)] public string Mandate_Letter_Commitment_3_DESC { get; set; }

    [AeLabel(isDropDown: true, placeholder: "Please Select", row: "13", column: "1")][AeFormCategory("Program Tags to be Updated by Sector", 50)][MaxLength(400)] public string Mandate_Letter_Commitment_4_DESC { get; set; }

    [AeLabel(isDropDown: true, placeholder: "Please Select", row: "14", column: "1")][AeFormCategory("Program Tags to be Updated by Sector", 50)][MaxLength(400)] public string Transfer_Payment_Programs_1_DESC { get; set; }

    [AeLabel(isDropDown: true, placeholder: "Please Select", row: "15", column: "1")][AeFormCategory("Program Tags to be Updated by Sector", 50)][MaxLength(400)] public string Transfer_Payment_Programs_2_DESC { get; set; }

    [AeLabel(isDropDown: true, placeholder: "Please Select", row: "16", column: "1")][AeFormCategory("Program Tags to be Updated by Sector", 50)][MaxLength(400)] public string Transfer_Payment_Programs_3_DESC { get; set; }

    [AeLabel(isDropDown: true, placeholder: "Please Select", row: "17", column: "1")][AeFormCategory("Program Tags to be Updated by Sector", 50)][MaxLength(400)] public string Transfer_Payment_Programs_4_DESC { get; set; }

    [AeLabel(isDropDown: true, placeholder: "Please Select", row: "18", column: "1")][AeFormCategory("Program Tags to be Updated by Sector", 50)][MaxLength(400)] public string Transfer_Payment_Programs_5_DESC { get; set; }

    [AeLabel(isDropDown: true, placeholder: "Please Select", row: "19", column: "1")][AeFormCategory("Program Tags to be Updated by Sector", 50)][MaxLength(400)] public string Transfer_Payment_Programs_6_DESC { get; set; }

    [AeLabel(isDropDown: true, placeholder: "Please Select", row: "20", column: "1")][AeFormCategory("Program Tags to be Updated by Sector", 50)][MaxLength(400)] public string Transfer_Payment_Programs_7_DESC { get; set; }
    [AeLabel(isDropDown: true, placeholder: "Please Select", row: "20", column: "1")][AeFormCategory("Program Tags to be Updated by Sector", 50)][MaxLength(400)] public string Transfer_Payment_Programs_8_DESC { get; set; }

    [AeLabel(isDropDown: true, placeholder: "Please Select", row: "21", column: "1")][AeFormCategory("Program Tags to be Updated by Sector", 50)][MaxLength(400)] public string Transfer_Payment_Programs_Less5_1_DESC { get; set; }

    [AeLabel(isDropDown: true, placeholder: "Please Select", row: "22", column: "1")][AeFormCategory("Program Tags to be Updated by Sector", 50)][MaxLength(400)] public string Transfer_Payment_Programs_Less5_2_DESC { get; set; }

    [AeLabel(isDropDown: true, placeholder: "Please Select", row: "23", column: "1")][AeFormCategory("Program Tags to be Updated by Sector", 50)][MaxLength(400)] public string Transfer_Payment_Programs_Less5_3_DESC { get; set; }

    [AeLabel(isDropDown: true, placeholder: "Please Select", row: "24", column: "1")][AeFormCategory("Program Tags to be Updated by Sector", 50)][MaxLength(400)] public string Horizontal_Initiative_1_DESC { get; set; }

    [AeLabel(isDropDown: true, placeholder: "Please Select", row: "25", column: "1")][AeFormCategory("Program Tags to be Updated by Sector", 50)][MaxLength(400)] public string Horizontal_Initiative_2_DESC { get; set; }

    [AeLabel(isDropDown: true, placeholder: "Please Select", row: "26", column: "1")][AeFormCategory("Program Tags to be Updated by Sector", 50)][MaxLength(400)] public string Horizontal_Initiative_3_DESC { get; set; }

    [AeFormCategory("Program Tags to be Updated by Sector", 50)]
    [MaxLength(4000)]
    [AeLabel(placeholder: "Please Select", row: "27", column: "1")]
    public string Related_Program_Or_Activities { get; set; }






    [AeLabel(isDropDown: true, placeholder: "Please Select", row: "28", column: "1")]
    [AeFormCategory("Program Tags as per TBS Database and GCInfoBase", 51)]

    [MaxLength(400)]
    public string Departmental_Result_1_CD { get; set; }
    [AeLabel(isDropDown: true, placeholder: "Please Select", row: "28", column: "2")]
    [AeFormCategory("Program Tags as per TBS Database and GCInfoBase", 51)]
    [MaxLength(400)]
    public string Departmental_Result_2_CD { get; set; }
    [AeLabel(isDropDown: true, placeholder: "Please Select", row: "28", column: "3")]
    [AeFormCategory("Program Tags as per TBS Database and GCInfoBase", 51)]
    [MaxLength(400)]
    public string Departmental_Result_3_CD { get; set; }


    [AeLabel(isDropDown: true, placeholder: "Please Select", row: "29", column: "1")][AeFormCategory("Program Tags as per TBS Database and GCInfoBase", 51)][MaxLength(400)] public string Method_Of_Intervention_1_DESC { get; set; }
    [AeLabel(isDropDown: true, placeholder: "Please Select", row: "29", column: "2")][AeFormCategory("Program Tags as per TBS Database and GCInfoBase", 51)][MaxLength(400)] public string Method_Of_Intervention_2_DESC { get; set; }

    [AeLabel(isDropDown: true, placeholder: "Please Select", row: "30", column: "1")][AeFormCategory("Program Tags as per TBS Database and GCInfoBase", 51)][MaxLength(400)] public string Target_Group_1_DESC { get; set; }
    [AeLabel(isDropDown: true, placeholder: "Please Select", row: "30", column: "2")][AeFormCategory("Program Tags as per TBS Database and GCInfoBase", 51)][MaxLength(400)] public string Target_Group_2_DESC { get; set; }

    [AeLabel(isDropDown: true, placeholder: "Please Select", row: "31", column: "1")][AeFormCategory("Program Tags as per TBS Database and GCInfoBase", 51)][MaxLength(400)] public string Target_Group_3_DESC { get; set; }
    [AeLabel(isDropDown: true, placeholder: "Please Select", row: "31", column: "2")][AeFormCategory("Program Tags as per TBS Database and GCInfoBase", 51)][MaxLength(400)] public string Target_Group_4_DESC { get; set; }

    [AeLabel(isDropDown: true, placeholder: "Please Select", row: "32", column: "1")][AeFormCategory("Program Tags as per TBS Database and GCInfoBase", 51)][MaxLength(400)] public string Target_Group_5_DESC { get; set; }

    [AeLabel(isDropDown: true, placeholder: "Please Select", row: "33", column: "1")][AeFormCategory("Program Tags as per TBS Database and GCInfoBase", 51)][MaxLength(400)] public string Government_Of_Canada_Activity_Tags_DESC { get; set; }

    [AeLabel(isDropDown: true, placeholder: "Please Select", row: "34", column: "1")][AeFormCategory("Program Tags as per TBS Database and GCInfoBase", 51)][MaxLength(4000)] public string Canadian_Classification_Of_Functions_Of_Government_DESC { get; set; }



    [AeLabel(isDropDown: true, placeholder: "Please Select")]
    [AeFormCategory("GBA Plus", 56)]
    [MaxLength(50)]
    public string Does_Indicator_Enable_Program_Measure_Equity_Option { get; set; }

    [AeFormCategory("GBA Plus", 56)]
    [AeLabel(isDropDown: true, placeholder: "Please Select", validValues: new[] { "Yes", "No"})]
    [MaxLength(10)]
    public string Collecting_Data { get; set; }

    [AeFormCategory("GBA Plus", 56)]
    [AeLabel(isDropDown: true, placeholder: "Please Select", validValues: new[] { "Yes", "No" })]
    [MaxLength(10)]
    public string Disaggregated_Data { get; set; }


    [AeFormCategory("GBA Plus", 56)]
    [AeLabel(isDropDown: true, placeholder: "Please Select")]
    [MaxLength(1000)]
    public string Is_Equity_Seeking_Group { get; set; }

    [AeFormCategory("GBA Plus", 56)]
    [AeLabel(isDropDown: true, placeholder: "Please Select")]
    [MaxLength(1000)]
    public string Is_Equity_Seeking_Group2 { get; set; }

    [AeFormCategory("GBA Plus", 56)]
    [AeLabel(placeholder: "If others, please add comment here ")]
    [MaxLength(4000)]
    public string Is_Equity_Seeking_Group_Other { get; set; }

    [AeFormCategory("GBA Plus", 56)]        
    [MaxLength(4000)]
    public string Disaggregated_Data_Information { get; set; }




    [AeFormCategory("GBA Plus", 56)]
    [MaxLength(8000)]
    [AeFormIgnore]
    public string Does_Indicator_Enable_Program_Measure_Equity { get; set; }

    [AeFormCategory("GBA Plus", 56)]
    [MaxLength(4000)]
    [AeFormIgnore]
    public string No_Equity_Seeking_Group { get; set; }


    [AeFormIgnore]
    [AeLabel(row: "40", column: "1")]
    [AeFormCategory("Date of PIP Approval", 57)] public DateTime? Approval_By_Program_Offical_DT { get; set; }
    [AeFormIgnore]
    [AeLabel(row: "40", column: "2")]
    [AeFormCategory("Date of PIP Approval", 57)] public DateTime? Consultation_With_The_Head_Of_Evaluation_DT { get; set; }
    [AeFormIgnore]
    [AeLabel(row: "40", column: "3")]
    [AeFormCategory("Date of PIP Approval", 57)] public DateTime? Functional_SignOff_DT { get; set; }


    [AeLabel(row: "41", column: "1")]
    [AeFormCategory("Program Notes", 57)]
    [MaxLength(4000)]
    public string Program_Notes { get; set; }



    [AeFormIgnore]
    public string Last_Updated_UserId { get; set; }

    [AeLabel(row: "42", column: "1")]
    [AeFormCategory("Latest Update Information", 60)]
    [NotMapped]
    [Editable(false)] public string Last_Updated_UserName { get; set; }



    [AeLabel(row: "42", column: "2")]        
    [AeFormCategory("Latest Update Information", 60)]
    public DateTime Date_Updated_DT { get; set; }
        
    [NotMapped]
    [AeFormIgnore]
    public RenderFragment PowerBiUrl { get; set; }


    [AeFormIgnore]
    public DateTime Last_Updated_DT { get; set; }

    [AeFormIgnore]
    [Timestamp]
    public byte[] Timestamp { get; set; }
        
    public PIP_FiscalYears FiscalYear { get; set; }
        
    [AeFormIgnore]
    public string EditingUserId { get; set; }



    [AeFormIgnore]
    public bool IsProgramInformationLocked { get; set; }
    [AeFormIgnore]
    public bool IsSpendingLocked { get; set; }
    [AeFormIgnore]
    public bool IsSectorProgramTagsLocked { get; set; }
    [AeFormIgnore]
    public bool IsGCInfoBaseProgramTagsLocked { get; set; }
    [AeFormIgnore]
    public bool IsGBALocked { get; set; }
    [AeFormIgnore]
    public bool IsDateOfPipApprovalLocked { get; set; }
    [AeFormIgnore]
    public bool IsLatestUpdateInformationLocked { get; set; }
    [AeFormIgnore]
    public bool IsProgramNotesLocked { get; set; }
}