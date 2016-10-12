using UnityEngine;


//Likelihood of Pokémon to be a specific gender
public enum GenderRate {

    ALWAYSMALE,
    FEMALEONEEIGHTH,
    FEMALEONEFOURTH,
    FEMALEONEHALF,
    FEMALETHREEFOURTHS,
    FEMALESEVENEIGHTS,
    ALWAYSFEMALE,
    GENDERLESS

};

//Growth Rate of Pokémon, which determines how much Exp. will be needed to level up
public enum GrowthRate {

    FAST,
    MEDIUM,
    SLOW,
    PARABOLIC,
    ERRATIC,
    FLUCTUATING

};

//Egg groups the Pokémon belong to
public enum EggGroups {
    MONSTER,
    WATER1,
    BUG,
    FLYING,
    FIELD,
    FAIRY,
    GRASS,
    HUMANLIKE,
    WATER3,
    MINERAL,
    AMORPHOUS,
    WATER2,
    DITTO,
    DRAGON,
    UNDISCOVERED
}

//Available colors. 
//Descriptions are the displayed names of the colors (used for colors with spaces, others will use a default format)
//Most enums don't use descriptions, because the information is not displayed to the user
public enum PBColors {
    
    BLACK,
    BLUE,
    BROWN,
    GRAY,
    GREEN,
    PINK,
    PURPLE,
    RED,
    WHITE,
    YELLOW,
    [System.ComponentModel.Description("Forest Green")]
    FORESTGREEN
}

//Habitats Pokémon can be found in
public enum PokemonHabitats {

    CAVE,
    FOREST,
    GRASSLAND,
    MOUNTAIN,
    RARE,
    [System.ComponentModel.Description("Rough Terrain")]
    ROUGHTERRAIN,
    SEA,
    URBAN,
    [System.ComponentModel.Description("Water's Edge")]
    WATERSEDGE

}

//The types of evolutions Pokémon can have
public enum PokemonEvolutionTypes {

    HAPPINESS,
    HAPPINESSDAY,
    HAPPINESSNIGHT,
    LEVEL,
    TRADE,
    TRADEITEM,
    ITEM,
    ATTACKGREATER,
    ATTACKDEFENSEEQUAL,
    DEFENSEGREATER,
    SILCOON,
    CASCOON,
    NINJASK,
    SHEDINJA,
    BEAUTY,
    ITEMMALE,
    ITEMFEMMALE,
    DAYHOLDITEM,
    NIGHTHOLDITEM,
    HASMOVE,
    HASINPARTY,
    LEVELMALE,
    LEVELFEMALE,
    LOCATION,
    TRADESPECIES,
    LEVELDAY,
    LEVELNIGHT,
    LEVELDARKINPARTY,
    LEVELRAIN,
    HAPPINESSMOVETYPE

}


