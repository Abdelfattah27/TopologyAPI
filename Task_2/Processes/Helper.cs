using System;
using System.Collections.Generic;
using System.Linq;
using TopologyAPI.DATA;

namespace TopologyAPI.Processes
{
    public static class Helper
    {
        public static List<string> JsonReorder(string jsonStr, out string reorderdJson)
        {
            var ListOfAttributes = jsonStr.Replace(Environment.NewLine, string.Empty).Replace(" ", string.Empty).Split("\":").ToList().Select(x => x.Remove(0, x.LastIndexOf('"') + 1)).ToList();
            var ListOfComponentDataNames = ListOfAttributes.Where((x, idx) =>
             (idx != ListOfAttributes.Count - 1)
             && ((ListOfAttributes[idx + 1].ToLower() == "default") || (ListOfAttributes[idx + 1].ToLower() == "deafult"))).ToList();
            reorderdJson = jsonStr;
            foreach (var item in ListOfComponentDataNames)
            {
                reorderdJson = reorderdJson.Replace(item, "componentData");
            }
            return ListOfComponentDataNames;
        }
        public static string ReorderJsonToWrite(Topology T, string jsonNotReordered)
        {

            var ActualDataNames = T.Components.Select(x => x.DataNameOfComponent).ToList();
            foreach (var item in ActualDataNames)
            {
                jsonNotReordered = jsonNotReordered.Remove(jsonNotReordered.IndexOf("ComponentData"))
                    + item
                    + jsonNotReordered[(jsonNotReordered.IndexOf("ComponentData") + "ComponentData".Length)..];
            }
            return jsonNotReordered;


        }
    }
}
