using System.Collections.Generic;

namespace Datahub.Core.Data.ExternalSearch.OpenData;

public class OpenDataResultWrapper
{
    public string Help { get; set; }
    public bool Success { get; set; }
    public OpenDataResult Result { get; set; }
}

public class OpenDataResult
{
    public int Count { get; set; }
    public string Sort { get; set; }
    //TODO facets; search_facets; facet_ranges
    public IList<OpenDataItem> Results { get; set;}
}