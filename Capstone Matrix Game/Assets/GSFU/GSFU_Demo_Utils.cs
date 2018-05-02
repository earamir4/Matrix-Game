using UnityEngine;
using System.Collections.Generic;

public static class GSFU_Demo_Utils
{
    [System.Serializable]
    public struct PlayerInfo
    {
        public string name;
        public float q11;
        public float q12;
        public float q13;
        public float q21;
        public float q22;
        public float q23;
        public float q31;
        public float q32;
        public float q33;
        public float q41;
        public float q42;
        public float q43;

    }

    public static PlayerInfo player;
    private static string tableName = "PlayerInfo";
    public static string playername;
    public static float completetime;

    public static void namechange(string n)
    {
        Debug.Log("<color=yellow>Updating Player name.</color>");
        bool runtime = true;
        player = new PlayerInfo();
        playername = n;
        player.name = playername;
        player.q11 = 0;
        player.q12 = 0;
        player.q13 = 0;
        player.q21 = 0;
        player.q22 = 0;
        player.q23 = 0;
        player.q31 = 0;
        player.q32 = 0;
        player.q33 = 0;
        player.q41 = 0;
        player.q42 = 0;
        player.q43 = 0;

        string jsonPlayer = JsonUtility.ToJson(player);

        Debug.Log("<color=yellow>Sending following player to the cloud: \n</color>" + jsonPlayer);
        //string jsonPlayer = JsonUtility.ToJson(player);


        // Save the object on the cloud, in a table called like the object type.
        CloudConnectorCore.CreateObject(jsonPlayer, tableName, runtime);


    }

    // Might not include this. this is used for debugging atm. We should just have a static table created b4 hand and just update it.
    //That would be better than creating a new table everytime. Would allow us to keep progress of players.
    public static void CreatePlayerTable(bool runtime)
    {
        Debug.Log("<color=yellow>Creating a table in the cloud for players data.</color>");

        // Creting an string array for field (table headers) names.
        string[] fieldNames = new string[12];
        fieldNames[0] = "name";
        fieldNames[1] = "q1";
        fieldNames[2] = "q2";
        fieldNames[3] = "q3";
        fieldNames[4] = "q4";
        fieldNames[5] = "q5";
        fieldNames[6] = "q6";
        fieldNames[7] = "q7";
        fieldNames[8] = "q8";
        fieldNames[9] = "q9";
        fieldNames[10] = "q10";
        fieldNames[11] = "q11";

        // Request for the table to be created on the cloud.
        CloudConnectorCore.CreateTable(fieldNames, tableName, runtime);
    }

    public static void createPlayer(string n)
    {
        RetrievePlayer(n);
        playername = n;
    }



    // example update command. can either have one of these and have a way to change it for every question. Or have several.
    //This is a general use to change names. It is as simpls as 
    //  (the table being changed, the first column, game object in first column to be edited, the column in which you want to edit, the change you want to make, runtime)
    public static void UpdatePlayerName(bool runtime)
    {
        Debug.Log("<color=yellow>Updating cloud data: player names " + playername + ".</color>");

        // Look in the 'PlayerInfo' table, for an object of name 'Mithrandir', and set its completion time to a given amount.
        CloudConnectorCore.UpdateObjects(tableName, "name", playername, "name", "Cameron Root", runtime);
    }


    //here is an example on how to update with a seperate method for every question
    public static void UpdatePlayerQuestion1(bool runtime)
    {
        Debug.Log("<color=yellow>Updating cloud data: player named " + playername + " finished question 1 in " + completetime + " seconds.</color>");

        // Look in the 'PlayerInfo' table, for an object of name 'Cameron Root, and set its level to 100.
        CloudConnectorCore.UpdateObjects(tableName, "name", playername, "q1", completetime.ToString(), runtime);
    }


    // Need work. Hint system. Figure out how to pull a row w/o first column if possible. Make a sheet for hints and broadcasts.
    public static void RetrieveGandalf(bool runtime)
    {
        Debug.Log("<color=yellow>Retrieving player of name Mithrandir from the Cloud.</color>");

        // Get any objects from table 'PlayerInfo' with value 'Mithrandir' in the field called 'name'.
        CloudConnectorCore.GetObjectsByField(tableName, "name", "Mithrandir", runtime);
    }

    public static void RetrievePlayer(string n)
    {
        Debug.Log("<color=yellow>Retrieving player of name Mithrandir from the Cloud.</color>");

        // Get any objects from table 'PlayerInfo' with value 'Mithrandir' in the field called 'name'.
        CloudConnectorCore.GetObjectsByField(tableName, "name", n, true);
    }

    // This will be used for broadcasts. One sheet will only have one column, broadcasts. This will pull it.
    public static void GetAllPlayers(bool runtime)
    {
        Debug.Log("<color=yellow>Retrieving all players from the Cloud.</color>");

        // Get all objects from table 'PlayerInfo'.
        CloudConnectorCore.GetTable(tableName, runtime);
    }

    // Haven't figured out if wed use this one yet.
    public static void GetAllTables(bool runtime)
    {
        Debug.Log("<color=yellow>Retrieving all data tables from the Cloud.</color>");

        // Get all objects from table 'PlayerInfo'.
        CloudConnectorCore.GetAllTables(runtime);
    }



    //every thing after is used for sending. Nothing directly used.


    // Parse data received from the cloud.
    public static void ParseData(CloudConnectorCore.QueryType query, List<string> objTypeNames, List<string> jsonData)
    {
        for (int i = 0; i < objTypeNames.Count; i++)
        {
            Debug.Log("Data type/table: " + objTypeNames[i]);
        }

        // First check the type of answer.
        if (query == CloudConnectorCore.QueryType.getObjects)
        {
            // In the example we will use only the first, thus '[0]',
            // but may return several objects depending the query parameters.

            // Check if the type is correct.
            if (string.Compare(objTypeNames[0], tableName) == 0)
            {
                try
                {
                    PlayerInfo[] players = GSFUJsonHelper.JsonArray<PlayerInfo>(jsonData[0]);
                    player = players[0];
                }
                catch
                {
                    namechange(playername);
                }
            }
        }

        // First check the type of answer.
        if (query == CloudConnectorCore.QueryType.getTable)
        {
            // Check if the type is correct.
            if (string.Compare(objTypeNames[0], tableName) == 0)
            {
                // Parse from json to the desired object type.
                PlayerInfo[] players = GSFUJsonHelper.JsonArray<PlayerInfo>(jsonData[0]);

                string logMsg = "<color=yellow>" + players.Length.ToString() + " objects retrieved from the cloud and parsed:</color>";
                for (int i = 0; i < players.Length; i++)
                {
                    logMsg += "\n" +
                        "<color=blue>Name: " + players[i].name + "</color>\n"; //+
                                                                               //"Level: " + players[i].level + "\n" +
                                                                               //"Health: " + players[i].health + "\n" +
                                                                               //"Role: " + players[i].role + "\n";				
                }
                Debug.Log(logMsg);
            }
        }

        // First check the type of answer.
        if (query == CloudConnectorCore.QueryType.getAllTables)
        {
            // Just dump all content to the console, sorted by table name.
            string logMsg = "<color=yellow>All data tables retrieved from the cloud.\n</color>";
            for (int i = 0; i < objTypeNames.Count; i++)
            {
                logMsg += "<color=blue>Table Name: " + objTypeNames[i] + "</color>\n"
                    + jsonData[i] + "\n";
            }
            Debug.Log(logMsg);
        }
    }
}

// Helper class: because UnityEngine.JsonUtility does not support deserializing an array...
// http://forum.unity3d.com/threads/how-to-load-an-array-with-jsonutility.375735/
public class GSFUJsonHelper
{
    public static T[] JsonArray<T>(string json)
    {
        string newJson = "{ \"array\": " + json + "}";
        Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(newJson);
        return wrapper.array;
    }

    [System.Serializable]
    private class Wrapper<T>
    {
        public T[] array = new T[] { };
    }
}
