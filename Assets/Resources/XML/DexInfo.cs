using UnityEngine;
using System.Collections.Generic;

public class Dex {
    public string dexInternalName;
    public string dexDisplayName;
    public int maxValue;
    public bool givenToPlayer;
    public Dex(string p_Internal, string p_Display, int p_Max = 0, bool p_givenToPlayer = false) {
        dexInternalName = p_Internal;
        dexDisplayName = p_Display;
        maxValue = p_Max;
        givenToPlayer = p_givenToPlayer;
    }
}
//A dex with a max of 0 does not impose a limit, and will either fill itself with regional Pokémon
//If no regional Pokémon exist for the dex, it will fill itself will all (National Dex)

//Dexes are defined here, since there's only a couple they can be hard coded
public class DexInfo {

    public bool dexInitialized = false;
    //Make sure numDexes reflects the number of dexes you hardcode.  If different, it will refill all the dexes
    private int numDexes = 3;
    public List<Dex> dexList = new List<Dex>();
    public Dex currentDex;

    public DexInfo() {
        dexList.Add(new Dex("NATIONALDEX", "National Dex"));
        dexList.Add(new Dex("KANTODEX", "Kanto Dex", 151));
        dexList.Add(new Dex("JOHTODEX", "Johto Dex"));
    }

    public List<Dex> getDexes() {
        return dexList;
    }

    void updateDexes() {

    }

}

