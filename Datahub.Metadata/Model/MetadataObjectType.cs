﻿namespace Datahub.Metadata.Model
{
    public enum MetadataObjectType : byte
    {
        File,
        PowerBIWorkspace,
        PowerBIDataset,
        PowerBIReport,
        FileUrl,
        GeoObject
    }

    public enum MetadataClassificationType : byte
    { 
        Unclassified,
        ProtectedA,
        ProtectedB  
    }
}
