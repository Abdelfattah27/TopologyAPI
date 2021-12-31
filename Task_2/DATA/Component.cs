using Newtonsoft.Json;
using System.Collections.Generic;

namespace TopologyAPI.DATA
{
    public class Component : Commons
    {
        public string Type { get; set; }
   
        public Data ComponentData { get; set; }
        
        public Dictionary<string ,  string>  Netlist { get; set; }

        [JsonIgnore]
        public string DataNameOfComponent { get; set; }
        public class Data
        {
            public double Default { get; set; }
            public double Min { get; set; }
            public double Max { get; set; }
        }
    }
}
