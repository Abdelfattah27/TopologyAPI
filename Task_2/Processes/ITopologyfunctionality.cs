using System.Collections.Generic;
using TopologyAPI.DATA;

namespace TopologyAPI.Processes
{
    interface ITopologyfunctionality
    {
        /// <summary>
        /// this function takes a json file, read it ans store it in the memory in a text file
        /// Or take a json String 
        /// </summary>
        /// <param name="FileName">The Json File Path , leave the Data Parameter Empty</param>
        /// <param name="Data">Put a Empty string in File , Put the json string here to store it in the memory</param>
        /// <returns>Topology</returns>

        Topology ReadJSON(string FileName , string Data = "" );
        /// <summary>
        /// Usd To write a Json in the emmory by using it's TopologyID
        /// </summary>
        /// <param name="TopologyID">The ID of the Topology</param>
        /// <returns>Bool indicates if the process success or not</returns>
        bool WriteJSON(string TopologyID);
        /// <summary>
        /// Used to get all the Topology you have 
        /// </summary>
        /// <returns>List of Topologies</returns>
        List<Topology> QueryTopologies();
        /// <summary>
        /// used to delete spacific topology by it's Id
        /// </summary>
        /// <param name="TopologyID">the Id of the topology</param>
        /// <returns>Bool indicates if the process success or not</returns>
        bool DeleteTopology(string TopologyID);
        /// <summary>
        /// Used to get all the devices you have in your topology
        /// </summary>
        /// <param name="TopologyID">the Id of the topology</param>
        /// <returns>List of devices</returns>
        List<Component> QueryDevices(string TopologyID);
        /// <summary>
        /// Get all devicess connect in a netlist bin 
        /// </summary>
        /// <param name="TopologyID">the id of the topology</param>
        /// <param name="NetlistNodeID">the node which the devices connect</param>
        /// <returns>List of devices</returns>
        List<Component> QueryDevicesWithNetlistNode(string TopologyID, string NetlistNodeID);



    }
}
