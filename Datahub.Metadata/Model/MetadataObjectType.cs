using System.ComponentModel;

namespace Datahub.Metadata.Model
{
    public enum MetadataObjectType : byte
    {
        File,
        PowerBIWorkspace,
        PowerBIDataset,
        PowerBIReport,
        FileUrl,
        GeoObject,
        Database,
        DatasetUrl
    }
}
