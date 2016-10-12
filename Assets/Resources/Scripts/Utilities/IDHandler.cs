using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using System.IO;

[System.Serializable]
public struct MapIDInfo {
    public int mapID;
    public List<int> eventIDs;
    public MapIDInfo(int p_madID) {
        mapID = p_madID;
        eventIDs = new List<int>();
    }
}


public class IDHandler : UnityEditor.AssetModificationProcessor {

    public static string idHandlerPath = "Assets/Resources/Editor/IDHandler";

    public static Dictionary<string, List<MapIDInfo>> idDictionary;

    //Saving scene in Unity will save this info to file
    static string[] OnWillSaveAssets(string[] paths) {
        IDHandler.SaveIDs();
        return paths;
    }

    public static void SaveIDs() {
        if (idDictionary == null) {
            IDHandler.LoadIDs();
        }

        FileInfo idFile;
        if ((new FileInfo(idHandlerPath)).Exists) {
            // Get file info
            idFile = new FileInfo(idHandlerPath);
            // Remove the hidden attribute of the file
            idFile.Attributes &= ~FileAttributes.Hidden;
        }

        using (Stream stream = File.Open(idHandlerPath, FileMode.Create)) {
            var binaryFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
            binaryFormatter.Serialize(stream, idDictionary);
        }
        // Hide the file.
        idFile = new FileInfo(idHandlerPath);
        idFile.Attributes |= FileAttributes.Hidden;
    }

    public static void LoadIDs() {
        idDictionary = new Dictionary<string, List<MapIDInfo>>();

        FileInfo idFile;
        if ((new FileInfo(idHandlerPath)).Exists) {
            // Get file info
            idFile = new FileInfo(idHandlerPath);
            // Remove the hidden attribute of the file
            idFile.Attributes &= ~FileAttributes.Hidden;
        } else {
            return;
        }

        using (Stream stream = File.Open(idHandlerPath, FileMode.Open)) {
            var binaryFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
            idDictionary = (Dictionary<string, List<MapIDInfo>>)binaryFormatter.Deserialize(stream);
        }

        // Hide the file.
        idFile = new FileInfo(idHandlerPath);
        idFile.Attributes |= FileAttributes.Hidden;
    }

    public static void addScene(string sceneName) {
        if (idDictionary == null) {
            IDHandler.LoadIDs();
        }
        if (idDictionary.ContainsKey(sceneName)) {
            idDictionary.Add(sceneName, new List<MapIDInfo>());
        }
    }

    public static int getNextMapID(string sceneName) {
        if (idDictionary == null) {
            IDHandler.LoadIDs();
        }
        if (IDHandler.idDictionary.ContainsKey(sceneName)) {
            int tempInt = 1;
            while (IDHandler.idDictionary[sceneName].Exists(id => id.mapID == tempInt)) {
                ++tempInt;
            }
            IDHandler.idDictionary[sceneName].Add(new MapIDInfo(tempInt));
            Debug.Log(sceneName + " " + tempInt);
            return tempInt;
        } else {
            
            IDHandler.idDictionary.Add(sceneName, new List<MapIDInfo>());
            IDHandler.idDictionary[sceneName].Add(new MapIDInfo(1));
            return 1;
        }
    }

    public static int getNextEventID(string sceneName, int mapID) {
        if (idDictionary == null) {
            IDHandler.LoadIDs();
        }
        if (IDHandler.idDictionary.ContainsKey(sceneName)) {
            if (IDHandler.idDictionary[sceneName].Exists(id => id.mapID == mapID)) {
                int tempInt = 1;
                while (IDHandler.idDictionary[sceneName][mapID].eventIDs.Contains(tempInt)) {
                    ++tempInt;
                }
                IDHandler.idDictionary[sceneName][mapID].eventIDs.Add(tempInt);
                return tempInt;
            } else {
                IDHandler.idDictionary[sceneName].Add(new MapIDInfo(mapID));
                IDHandler.idDictionary[sceneName][mapID].eventIDs.Add(1);
                return 1;
            }
        } else {
            IDHandler.idDictionary.Add(sceneName, new List<MapIDInfo>());
            IDHandler.idDictionary[sceneName].Add(new MapIDInfo(mapID));
            IDHandler.idDictionary[sceneName][mapID].eventIDs.Add(1);
            return 1;
        }
    }

    public static void removeMapID(string sceneName, int targetMapID) {
        if (idDictionary == null) {
            IDHandler.LoadIDs();
        }
        if (IDHandler.idDictionary.ContainsKey(sceneName)) {
            IDHandler.idDictionary[sceneName].RemoveAll(x => x.mapID == targetMapID);
        }
    }

    public static void removeEventID(string sceneName, int mapId, int targetEventID) {
        if (idDictionary == null) {
            IDHandler.LoadIDs();
        }
        if (IDHandler.idDictionary.ContainsKey(sceneName) && IDHandler.idDictionary[sceneName].Exists(x => x.mapID == mapId)) {
            IDHandler.idDictionary[sceneName][mapId].eventIDs.RemoveAll(x => x.Equals(targetEventID));
        }
    }
}
