namespace Datahub.LanguageTraining.Data;

public class Language_Constants
{
    public static readonly string[] YESNO = { "Yes", "No" };

    public static readonly string[] PROVINCES = { "NL",
        "PEI",
        "NS",
        "NB",
        "QUE",
        "ONT",
        "MAN",
        "SASK",
        "ALTA",
        "BC",
        "YT",
        "NWT",
        "NVT"
    };

    public static readonly string[] MANAGERDECISIONS = { "Awaiting Approval", "Approved", "Declined" };

    public static readonly string[] LANGUAGE_TRAINING = { "French Language Training", "English Language Training"};

    public static readonly string[] LANGUAGE_TRAINING_PROVIDERS = { "NRCan Language School", "External Provider" };

    public static readonly string[] EMPLOYMENT_STATUS = { "Indeterminate Employee", "Term Employee" };

    public static readonly string[] SLESTATUS = { "X", "A", "B", "C", "E", "Not Applicable" };

    public static readonly string[] QUARTERS = { "Q1 - April to June", "Q2 - July to September", "Q3 – October to December", "Q4 – January to March" };

    //public static readonly string[] TRAININGTYPEFRENCH = { "Group Training - Part-time Training", "Group Training – Full-time Training (French program only)", "Group Training – Part-time Training (EX Program)" };
    public static readonly string[] TRAININGTYPEFRENCH = { "In-Person Group Training – Part-time Training (588 Booth, Ottawa)", "Virtual Group Training – Part-time Training", "In-Person Group Training – Full-time Training, French program only (588 Booth, Ottawa)", "Virtual Group Training – Full-time Training, French program only", "Group Training – Part-time Training (EX Program)" };
    public static readonly string[] TRAININGTYPEENGLISH = { "Group training : Part-time (6 hours of class + 2 hours self-learning)", "Group training : Part-time (3 hours of class + 1 hour self-learning)", "Group training : Part-time (1.5 hours of class + 0 hours self-learning)*", "Private training: Coaching**" };

    public static readonly string[] CLASSES_E = {
        "Beginner 1",
        "Beginner 2",
        "Beginner 3",
        "Intermediate 1 (BBB)",
        "Intermediate 2 (BBB)",
        "Intermediate 3 (BBB)",
        "Intermediate 4 (BBB)",
        "Intermediate 5 (BBB)",
        "Intermediate 6 (BBB)",
        "Preparation B",
        "Maintenance B",
        "Consolidation B",
        "Advanced 1 (CBC)",
        "Advanced 2 (CBC)",
        "Advanced 3 (CBC)",
        "Advanced 4 (CBC)",
        "Advanced 5 (CBC)",
        "Preparation C",
        "Maintenance C"
    };

    public static readonly string[] CLASSES_E_FRENCH = {
        "Débutant 1",
        "Débutant 2",
        "Débutant 3",
        "Intermédiaire 1 (BBB)",
        "Intermédiaire 2 (BBB)",
        "Intermédiaire 3 (BBB)",
        "Intermédiaire 4 (BBB)",
        "Intermédiaire 5 (BBB)",
        "Intermédiaire 6 (BBB)",
        "Préparation B",
        "Maintien B",
        "Consolidation B",
        "Enrichi 1 (CBC)",
        "Enrichi 2 (CBC)",
        "Enrichi 3 (CBC)",
        "Enrichi 4 (CBC)",
        "Enrichi 5 (CBC)",
        "Préparation C",
        "Maintien C"
    };

    public static readonly string[] CLASSES_F = {   "Intermediate 1 (BBB)",
        "Intermediate 2 (BBB)",
        "Intermediate 3 (BBB)",
        "Intermediate 4 (BBB)",
        "Intermediate 5 (BBB)",
        "Intermediate 6 (BBB) - Test Oral Proficiency – B level",
        "Advanced 1 (CBC)",
        "Advanced 2 (CBC)",
        "Advanced 3 (CBC)",
        "Advanced 4 (CBC)",
        "Advanced 5 (CBC)",
        "Advanced 6 (CBC) - Test Oral Proficiency – C level"                
    };

    public static readonly string[] LANGUAGE_SCHOOL_SELECTION = {
        "Training accepted",
        "Requires LETP assessment",
        "Insufficient interest at level",
        "Demand exceeds capacity",
        "Late application"
    };

    public static readonly string[] SECTORSANDBRANCHES = {
        "NOTSELECTED",
        "AADMO - Associate Assistant Deputy Minister's Office",
        "AEB - Audit and Evaluation Branch",
        "CFS – Canadian Forest Sector",
        "CMSS - Corporate Management and Services Sector",
        "CPS - Communications and Portfolio Sector",
        "DMO - Deputy Minister Office",
        "EETS – Energy, Efficiency & Technology Sector",
        "ESS – Energy System Sector",
        "FS – Fuel Sector",
        "IDEA - Office of Inclusion, Diversity, Equity and Accessibility",
        "LMS – Lands & Minerals Sector",
        "LS – Legal Services",
        "Nòkwewashk",
        "OCS – Office of the Chief Scientist",
        "SPI – Strategic Policy and Innovation Sector",
    };

    public static readonly string[] SECTORSANDBRANCHES_OLD = { "AEB Audit Governance",
        "AEB Audit Operations",
        "AEB Chief Audit & Eval Exe Off",
        "AEB Evaluation",
        "CFS ADM's Office",
        "CFS Atlantic Forestry Centre",
        "CFS Cdn Wood Fibre Centre",
        "CFS Great Lakes Forestry Centre",
        "CFS Laurentian Forestry Centre",
        "CFS Northern Forestry Centre",
        "CFS Pacific Forestry Ctre",
        "CFS Plan., Ops. & Info",
        "CFS Science Pol & Integration",
        "CFS Trade Economics & Industry",
        "CMSS ADM's Office",
        "CMSS Chief Info Off & Security",
        "CMSS Finance & Procurement",
        "CMSS Human Resources Branch",
        "CMSS Planning & Operations",
        "CMSS Real Prop & Workpl Serv",
        "CPS ADM's Office",
        "CPS Engagement & Digital Comms",
        "CPS Management Services",
        "CPS Portfol Mgmt & Corp Secr",
        "CPS Public Affairs Branch",
        "DMO",
        "ETS ADM's Office",
        "ETS CanmetENERGY Ottawa",
        "ETS CanmetENERGY Varennes",
        "ETS CanmetMATERIALS",
        "ETS Office of Energy R&D",
        "ETS Planning & Results",
        "IARS",
        "LCES International Energ",
        "LCES ADM's Office",
        "LCES Clean Fuels",
        "LCES Electricity Resources",
        "LCES Energy Policy & Intl Affa",
        "LCES Office Energy Efficiency",
        "LMS ADM's Office",
        "LMS Bus Mgmt Serv & Data",
        "LMS CanmetMINING",
        "LMS Explos Safety & Secur",
        "LMS Geological Survey Canada",
        "LMS Hazards, Adaptation & Ops",
        "LMS Policy & Economics",
        "LMS Surveyor General Branch",
        "LS Legal Services",
        "MPMO ADM's Office",
        "MPMO Ind Part Off West",
        "MPMO Operations",
        "MPMO Strategic Pol & Planning",
        "OCS Environmental Assessment",
        "OCS Impact Assess Sci Capacity",
        "OCS Office of Chief Scientist",
        "SPI ADM's Office",
        "SPI Can Ctr Map & Earth Obs",
        "SPI Econ Anal, Data & Res",
        "SPI Ext Policy & Partner",
        "SPI Innovation Branch",
        "SPI PARDP",
        "SPI Plan Delivery & Results",
        "SPI Plan, Deliv & Results",
        "SPI Strategic Policy",
        "SPPIO ADM's Office",
        "SPPIO CanmetENERGY-Devon",
        "SPPIO Petroleum Resources",
        "TMX - Special Projects",
        "Other"
    };

}