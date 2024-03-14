using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace AplicationGoogle
{
    public class Result
    {
        public List<ResultItem> results { get; set; }
    }

    public class ResultItem
    {
        public Geometry geometry { get; set; }
        // Otros campos si es necesario
    }

    public class Geometry
    {
        public Location location { get; set; }
      
    }

    public class Location
    {
        public double lat { get; set; }
        public double lng { get; set; }
    }

}