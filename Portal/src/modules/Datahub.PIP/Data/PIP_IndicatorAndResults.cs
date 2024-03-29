﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using Elemental.Components;

namespace Datahub.PIP.Data;

public class PIP_IndicatorAndResults
{
    [Key]
    [AeFormIgnore]
    public int IndicatorAndResult_ID { get; set; }

    [MaxLength(20)]
    [AeFormIgnore]
    public string IndicatorCode { get; set; }

    [AeFormIgnore]
    public int FiscalYearId { get; set; }
    
    [AeFormCategory("Fiscal Year", 1)]
    [NotMapped]
    public string FiscalYearString => FiscalYear.FiscalYear;

    [AeFormCategory("Indicator Status", 1)]
    [MaxLength(100)]
    [AeLabel(row: "1", column: "1", isDropDown: true, validValues: new[] { "Input Required", "DRR Results Required" })]
    public string Indicator_Status { get; set; }

    [AeFormCategory("Indicator Details", 5)]
    [AeLabel(row: "1", column: "2", isDropDown: true, placeholder: "Please Select")]
    [MaxLength(400)]
    public string Outcome_Level_DESC { get; set; }

    [AeFormCategory("Indicator Details", 5)]
    [AeLabel(row: "1", column: "2")]
    [MaxLength(1000)]
    public string Program_Output_Or_Outcome_DESC { get; set; }

    [AeFormCategory("Indicator Details", 5)]
    [AeLabel(row: "3", column: "1")]
    [MaxLength(4000)]
    public string Indicator_DESC { get; set; }

    [AeFormCategory("Indicator Details", 5)]
    [AeLabel(row: "4", column: "1", isDropDown: true, placeholder: "Please Select")]
    [MaxLength(1000)]
    public string Source_Of_Indicator_DESC { get; set; }

    [AeFormCategory("Indicator Details", 5)]
    [AeLabel(row: "4", column: "2", isDropDown: true, placeholder: "Please Select")]
    [MaxLength(1000)]
    public string Source_Of_Indicator2_DESC { get; set; }

    [AeFormCategory("Indicator Details", 5)]
    [AeLabel(row: "4", column: "3", isDropDown: true, placeholder: "Please Select")]
    [MaxLength(1000)]
    public string Source_Of_Indicator3_DESC { get; set; }

    [AeFormCategory("Indicator Details", 5)]
    [AeLabel(row: "7", column: "1", isDropDown: true, placeholder: "Please Select")]
    [MaxLength(50)]
    public string Can_Report_On_Indicator { get; set; }

    [AeFormCategory("Indicator Details", 5)]
    [AeLabel(row: "7", column: "2", isDropDown: true, placeholder: "Please Select")]
    [MaxLength(100)]
    public string Cannot_Report_On_Indicator { get; set; }

    [AeFormCategory("Indicator Details", 5)]
    [AeLabel(row: "8", column: "1")]
    [MaxLength(4000)]
    public string DRF_Indicator_No { get; set; }

        
    [AeFormCategory("Indicator Details", 5)]
    [AeLabel(row: "9", column: "1")]
    [MaxLength(1000)]
    public string Branch_Optional_DESC { get; set; }

    [AeFormCategory("Indicator Details", 5)]
    [AeLabel(row: "9", column: "2")]
    [MaxLength(1000)]
    public string Sub_Program { get; set; }

    [AeFormCategory("Indicator Details", 5)]
    [AeLabel(row: "10", column: "1", isDropDown: true, placeholder: "Please Select")]
    [MaxLength(1000)]
    public string Indicator_Category_DESC { get; set; }

    [AeFormCategory("Indicator Details", 5)]
    [AeLabel(row: "10", column: "2", isDropDown: true, placeholder: "Please Select")]
    [MaxLength(1000)]
    public string Indicator_Direction_DESC { get; set; }


    [AeLabel(row: "11", column: "1")]
    [MaxLength(4000)]
    [AeFormCategory("Methodology", 10)]
    public string Indicator_Rationale_DESC { get; set; }

    [AeLabel(row: "12", column: "1")]
    [AeFormCategory("Methodology", 10)]
    [MaxLength(2000)]
    public string Indicator_Calculation_Formula_NUM { get; set; }

    [AeLabel(row: "13", column: "1")]
    [AeFormCategory("Methodology", 10)]
    [MaxLength(4000)]
    public string Measurement_Strategy { get; set; }

    [AeLabel(row: "14", column: "1")]
    [AeFormCategory("Methodology", 10)]
    [MaxLength(1000)]
    public string Baseline_DESC { get; set; }
    [AeLabel(row: "14", column: "2")]
    [AeFormCategory("Methodology", 10)]
    [MaxLength(100)]
    public string Date_Of_Baseline_DT { get; set; }

    [AeLabel(row: "15", column: "1")]
    [AeFormCategory("Methodology", 10)]
    [MaxLength(4000)]
    public string Notes_Definitions { get; set; }

    [AeLabel(row: "16", column: "1")]
    [AeFormCategory("Methodology", 10)]
    [MaxLength(1000)]
    public string Data_Source_DESC { get; set; }
    [AeLabel(row: "16", column: "2")]
    [AeFormCategory("Methodology", 10)]
    [MaxLength(1000)]
    public string Data_Owner_NAME { get; set; }


    [AeLabel(row: "17", column: "1", isDropDown: true, placeholder: "Please Select")]
    [MaxLength(1000)]
    [AeFormCategory("Methodology", 10)]
    public string Frequency_DESC { get; set; }
    [AeLabel(row: "17", column: "2", isDropDown: true, placeholder: "Please Select")]
    [MaxLength(1000)]
    [AeFormCategory("Methodology", 10)]
    public string Data_Type_DESC { get; set; }

    [AeLabel(row: "18", column: "1")]
    [MaxLength(8000)]
    [AeFormCategory("Methodology", 10)]
    public string Methodology_How_Will_The_Indicator_Be_Measured { get; set; }

    [AeLabel(row: "19", column: "1", isDropDown: true, placeholder: "Please Select")]
    [MaxLength(1000)]
    [AeFormCategory("Target", 20)]
    public string Target_Type_DESC { get; set; }
    [AeLabel(row: "19", column: "2")]
    [AeFormCategory("Target", 20)]
    [MaxLength(4000)]
    public string Target_DESC { get; set; }
    [AeLabel(row: "19", column: "3")]
    [AeFormCategory("Target", 20)]
    public DateTime? Date_To_Achieve_Target_DT { get; set; }


    [AeLabel(row: "20", column: "1")]
    [AeFormCategory("Actual Results", 30)]
    [MaxLength(500)]
    public string Result_DESC { get; set; }
    [AeLabel(row: "20", column: "2")]
    [AeFormCategory("Actual Results", 30)]
    public DateTime? Date_Result_Collected { get; set; }
    [AeLabel(row: "20", column: "3", isDropDown: true, placeholder: "Please Select")]
    [MaxLength(1000)]
    [AeFormCategory("Actual Results", 30)]
    public string Target_Met { get; set; }


    [AeLabel(row: "21", column: "1")]
    [MaxLength(8000)]
    [AeFormCategory("Actual Results", 30)]
    public string Explanation { get; set; }

    [MaxLength(8000)]
    [AeLabel(row: "22", column: "1")]
    [AeFormCategory("Actual Results", 30)]
    public string Midyear_Results { get; set; }

    [AeFormIgnore]
    [MaxLength(8000)]
    [AeFormCategory("Actual Results", 30)]
    public string Trend_Rationale { get; set; }


    [AeFormIgnore]
    public bool IsDeleted { get; set; }

    [AeFormIgnore]
    public string UserIdWhoDeleted { get; set; }


    [AeFormIgnore]
    public string Last_Updated_UserId { get; set; }

    [AeLabel(row: "28", column: "1")]
    [AeFormCategory("Latest Update Information", 60)]
    [NotMapped]
    [Editable(false)] public string Last_Updated_UserName { get; set; }




    [AeLabel(row: "28", column: "2")]
    [AeFormCategory("Latest Update Information", 60)]
    [Required] public DateTime Date_Updated_DT { get; set; }


    public PIP_Tombstone PIP_Tombstone { get; set; }

        

    public PIP_FiscalYears FiscalYear { get; set; }

    [AeFormIgnore]
    public DateTime Last_Updated_DT { get; set; }

    [AeFormIgnore]
    [Timestamp]
    public byte[] Timestamp { get; set; }

    [AeFormIgnore]
    public bool IsMethodologyLocked { get; set; }
    [AeFormIgnore]
    public bool IsTargetLocked { get; set; }
    [AeFormIgnore]
    public bool IsActualResultsLocked { get; set; }
    [AeFormIgnore]
    public bool IsLatestUpdateLocked { get; set; }

    [AeFormIgnore]
    public bool IsIndicatorStatusLocked { get; set; }
    [AeFormIgnore]
    public bool IsIndicatorDetailsLocked { get; set; }
    [AeFormIgnore]
    public bool IsFiscalYearLocked { get; set; }
    [AeFormIgnore]
    public string EditingUserId { get; set; }
    [AeFormIgnore]
    public string SourceFileName { get; set; }
    [AeFormIgnore]
    public DateTime? SourceFileUploadDate { get; set; }
    [AeFormIgnore]
    public int DuplicateCount { get; set; }
    [AeFormIgnore]
    [MaxLength(256)]
    public string DataFactoryRunId { get; set; }


}