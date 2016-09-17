using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System.IO;

[System.Serializable]
public class GameSwitches :MonoBehaviour {
    public Dictionary<string, bool> gameSwitches;

    public GameSwitches() {
      gameSwitches = new Dictionary<string, bool>();
    }
}

public class GameSwitchManager : MonoBehaviour {

    public static GameSwitches gameSwitches;

    public static void loadSwitches() {
      using (Stream stream = File.Open("Assets/Resources/Data/GameSwitches", FileMode.Open))
      {
          var binaryFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
          gameSwitches = (GameSwitches)binaryFormatter.Deserialize(stream);
      }
    }

    public static bool checkSwitch(string switchName) {
      if (gameSwitches == null ) {
        GameSwitchManager.loadSwitches();
      }
      if (gameSwitches.gameSwitches.ContainsKey(switchName)) {
        return gameSwitches.gameSwitches[switchName];
      } else {
        //switch does not exist
        return false;
      }
    }

    public static void setSwitch(string switchName, bool value) {
      if (gameSwitches == null ) {
        GameSwitchManager.loadSwitches();
      }
      if (gameSwitches.gameSwitches.ContainsKey(switchName)) {
        gameSwitches.gameSwitches[switchName] = value;
      } else {
        //switch does not exist
        gameSwitches.gameSwitches.Add(switchName, value);
      }
      return;
    }
}
