using System;

namespace WebPortal.Data.FGP
{
    public class GeoCoreItem
    {
        public Guid Id { get; set; }
        public DateTime Published { get; set; }
        public string Organisation { get; set; }
        public string Type { get; set; }
        public string TopicCategory { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Keywords { get; set; } // TODO turn it into a list
        public string SpatialRepresentation { get; set; }
        public string Language { get; set; } // json source: "eng; CAN" -> maybe turn it into a CultureInfo
        public string TemporalExtent { get; set; } // TODO some processing? (pair of month dates)
        public string Coordinates { get; set; } // TODO some processing (list of points)
        public string Created { get; set; } // TODO some processing (month date)

        public string Options { get; set; } // TODO lots of processing (JSON w/ multiple escape, multiple quotes)
        public string GraphicOverview { get; set; } // TODO lots of processing (same)
        public string Contact { get; set; } // TODO lots of processing (same)



        /*
    "id": "000183ed-8864-42f0-ae43-c4313a860720",
    "published": "2020-02-27",
    "organisation": "Government of Canada; Natural Resources Canada; Lands and Minerals Sector",
    "type": "series; s\u00e9rie",
    "topicCategory": "economy",
    "title": "Principal Mineral Areas, Producing Mines, and Oil and Gas Fields (900A)",
    "description": "[bunch of words]",
    "keywords": "[bunch of words, comma separated]",
    "spatialRepresentation": "vector; vecteur",
    "language": "eng; CAN",
    "temporalExtent": "{begin=2020-01, end=2020-12}",
    "coordinates": "[[[-141.003, 41.6755], [-52.6174, 41.6755], [-52.6174, 83.1139], [-141.003, 83.1139], [-141.003, 41.6755]]]",
    "created": "2021-02"
    
    "contact": "[json string]",
    "options": "[json string]",
    "graphicOverview": "[json string]",

    "total": "1",
    "row_num": "1",
        */
    }
}