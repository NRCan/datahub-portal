﻿using Datahub.Metadata.Model;
using System;

namespace Datahub.Metadata.DTO;

public class CatalogObjectResult
{
    public long CatalogObjectId { get; set; }
    public long ObjectMetadataId { get; set; }
    public string MetadataObjectId_TXT { get; set; }
    public MetadataObjectType DataType { get; set; }
    public string Name_English { get; set; }
    public string Name_French { get; set; }
    public string Location { get; set; }
    public int Sector { get; set; }
    public int Branch { get; set; }
    public string Contact { get; set; }
    public ClassificationType ClassificationType {  get; set; }
    public AreasOfBusiness BusinessArea { get; set; }
    public string Url_English { get; set; }
    public string Url_French { get; set; }
    public CatalogObjectLanguage Language { get; init; }
    public bool IsCatalogComplete { get; set; } = true;
    public bool IsFrench { get; set; }
    public Guid? GroupId { get; set; }
    public int? ProjectId { get; set; }
    
    public string WorkspaceId { get; set; }
    public FieldValueContainer Metadata { get; set; }

    public string DatasetUri => $"https://app.powerbi.com/groups/{WorkspaceId}/datasets/{MetadataObjectId_TXT}?experience=power-bi";
    
}