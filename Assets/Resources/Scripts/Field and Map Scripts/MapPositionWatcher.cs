using UnityEngine;
using System.Collections;
using System.Collections.Generic;


//This class loads as a reference to all "maps"
//For now, all it does is know when you've made a map change

public class MapPositionWatcher : MonoBehaviour {


	List<MapInfo> loadedMaps = new List<MapInfo> ();
	MapInfo curMap;
    //top yvalue of maps
    public float highestYValue;
    private bool hasSetHighestValue = false;


	void Awake () {
		 GameObject[] tempObjects = Object.FindObjectsOfType<GameObject>() ;
		 foreach (GameObject go in tempObjects) {
			 if (go.activeInHierarchy && go.GetComponent<Tiled2Unity.TiledMap>() != null) {
				 loadedMaps.Add(new MapInfo(go, go.transform.position.x, go.transform.position.y,
				 				go.GetComponent<Tiled2Unity.TiledMap>().GetMapWidthInPixelsScaled(), go.GetComponent<Tiled2Unity.TiledMap>().GetMapHeightInPixelsScaled()));
                if (!hasSetHighestValue) {
                    highestYValue = go.transform.position.y;
                    hasSetHighestValue = true;
                } else if (go.transform.position.y < highestYValue) { //a lower y-value will be higher on an X-Y plane
                    highestYValue = go.transform.position.y;
                }
			 }
		 }
		 updatePosition();
	}

    //PlayerMovement calls this each time the player takes a step
    //update to what current map the player is on
    public void updatePosition() { 
		foreach (MapInfo map in loadedMaps) {
			if (gameObject.transform.position.x >= map.getX() && gameObject.transform.position.x < (map.getX() + map.getWidth())) {
				if (gameObject.transform.position.y <= map.getY() && gameObject.transform.position.y > (map.getY() - map.getHeight())) {
					if (map != curMap) {
						if (curMap != null) {
							curMap.getObjectMap().GetComponent<MetadataSettings>().enabled = false;
						}
						curMap = map;
						curMap.getObjectMap().GetComponent<MetadataSettings>().enabled = true;
						break;
					}
				}
			}
		}
	}

	public MapInfo currentMap() {
		return curMap;
	}


}
