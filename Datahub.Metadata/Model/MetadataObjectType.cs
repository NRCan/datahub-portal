﻿namespace Datahub.Metadata.Model
{
    public enum MetadataObjectType : byte
    {
        File,
        PowerBIWorkspace,
        PowerBIDataset,
        PowerBIReport,
        FileUrl,
        GeoObject,
        Database
    }

    public enum MetadataClassificationType : byte
    { 
        Unclassified,
        ProtectedA,
        ProtectedB  
    }
}
