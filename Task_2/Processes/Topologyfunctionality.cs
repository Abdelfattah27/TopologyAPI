using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TopologyAPI.DATA;

namespace TopologyAPI.Processes
{
    public class Topologyfunctionality : ITopologyfunctionality
    {

        public List<Topology> DataStoredInMemory;
        public string ErrorFilePath { get; } = string.Format(AppDomain.CurrentDomain.BaseDirectory + @"\BaseErrorFile.txt");

        public string MemoryFilePath { get; } = string.Format(AppDomain.CurrentDomain.BaseDirectory + @"\BaseDataFile.txt");
        public Topologyfunctionality()
        {
            DataStoredInMemory = new List<Topology>();
            ReadFromMemory(MemoryFilePath);
        }

        private void ReadFromMemory(string memoryFilePath)
        {
            try
            {
                if (File.Exists(memoryFilePath))
                {
                    var data = File.ReadAllLines(memoryFilePath);
                    foreach (var item in data)
                    {
                        ReadJSON("", item);
                    }
                }
                else
                {
                    File.AppendAllText(memoryFilePath, "");
                }

            }
            catch (Exception ex)
            {
                
                File.AppendAllText(ErrorFilePath ,  ex.Message + '\n');
            }
        }

        public bool DeleteTopology(string TopologyID)
        {
           return QueryTopologies().RemoveAll(x => x.Id == TopologyID) > 0;
        }

        public List<Component> QueryDevices(string TopologyID)
        {
            try
            {
                return QueryTopologies().Select(x => x.Components).ToList()[0];
            }
            catch (Exception ex)
            {
                File.AppendAllText(ErrorFilePath, ex.Message + '\n');
                return null;
            }
        }

        public List<Component> QueryDevicesWithNetlistNode(string TopologyID, string NetlistNodeID)
        {
            try
            {
                return QueryTopologies().Where(x => x.Id == TopologyID).Select(x => x.Components).ToList()[0].Where(x => x.Netlist.ContainsValue(NetlistNodeID)).ToList();
            }
            catch (Exception ex)
            {
                File.AppendAllText(ErrorFilePath, ex.Message + '\n'); 
                return null;
            }
        }

        public List<Topology> QueryTopologies()
        {
            return DataStoredInMemory;
        }

        public Topology ReadJSON(string FileName , string Data = "")
        {
          
            try
            {
                string json = "";
                if(Data == string.Empty)
                {
                     json = File.ReadAllText(FileName);
                }
                else
                {
                    json = Data;
                }
                List<string> dataComponentNames = Helper.JsonReorder(json, out string reorderesJson);

                Topology T = JsonConvert.DeserializeObject<Topology>(reorderesJson);
                for(int i = 0; i < T.Components.Count; i++)
                {
                    T.Components[i].DataNameOfComponent = dataComponentNames[i];
                }
                DataStoredInMemory.Add(T);
                 WriteJSON(T.Id);
                return T;
            }
            catch (Exception ex)
            {
                File.AppendAllText(ErrorFilePath, ex.Message + '\n');
                return null;
            }
        }
      

        public bool WriteJSON(string TopologyID)
        {
            try
            {
                var T = DataStoredInMemory.Where(x => x.Id == TopologyID).FirstOrDefault();

                string TToJson = JsonConvert.SerializeObject(T);
                string JsonStrAfterReorderIt = Helper.ReorderJsonToWrite(T, TToJson);
                string[] MemoryData = File.ReadAllLines(MemoryFilePath);
                if (!MemoryData.Contains(JsonStrAfterReorderIt))
                {
                    File.AppendAllText(MemoryFilePath, Helper.ReorderJsonToWrite(T, TToJson) + '\n');
                }
                return true;
            }
            catch (Exception ex)
            {
                File.AppendAllText(ErrorFilePath, ex.Message + '\n');
                return false;
            }
        }
       
    }
}
