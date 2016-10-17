using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PauseMenuController : ChoiceController {

    void Start() {
        //Set desired objects/sprites to use (already created, and children of the game object this is attatched to)
        arrowLocation = "Graphics/Pictures/arrows";
        imageBoxName = "Pause Skin";
        imageArrowName = "Pause Selector";
        textObjectName = "Pause Text";
        //Set desired positioning
        arrow1024X = -275;
        arrow1024Y = 22;
        arrow1024Spacing = arrowOtherSpacing = 52;
        arrowOtherX = -292;
        arrowOtherY = 42;
        imageBox1024Width = imageBoxOtherWidth = 290;
        imageBox1024X = -15;
        imageBox1024Y = 50;
        imageBoxOtherX = -35;
        imageBoxOtherY = 70;
        imageArrow1024Width = imageArrow1024Height = 16;
        imageArrow1024Height = imageArrowOtherHeight = 32;
        imageArrow1024X = -275;
        imageArrow1024Y = 22;
        imageArrowOtherX = -292;
        imageArrowOtherY = 42;
        textObject1024Width = textObjectOtherWidth = 120;
        textObject1024Height = textObjectOtherHeight = 280;
        textObject1024X = -140;
        textObject1024Y = 25;
        textObjectOtherX = -155;
        textObjectOtherY = 45;
        updateMenuText();

        base.Start();     
    }

    void updateMenuText() {
        menuOptions.Clear();
        //Add each menu item
        menuOptions.Add("Pokédex");
        menuOptions.Add("Pokémon");
        menuOptions.Add("Bag");
    }

}
