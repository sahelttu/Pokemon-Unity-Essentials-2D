using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;


//Base class for choice boxes
public class ChoiceController : MonoBehaviour {

    protected Image imageBox, imageArrow;
    protected GameObject textObject;
    protected int numMenuItems;
    protected int arrowPos;
    protected string arrowLocation, imageBoxName, imageArrowName, textObjectName;
    protected List<string> menuOptions = new List<string>();

    //values for arrow movement
    protected int arrow1024X, arrow1024Y, arrow1024Spacing;
    protected int arrowOtherX, arrowOtherY, arrowOtherSpacing;
    //values for object positioning
    protected int imageBox1024Width, imageBox1024X, imageBox1024Y;
    protected int imageArrow1024Width, imageArrow1024Height, imageArrow1024X, imageArrow1024Y;
    protected int textObject1024Width, textObject1024Height, textObject1024X, textObject1024Y;
    protected int imageBoxOtherWidth, imageBoxOtherX, imageBoxOtherY;
    protected int imageArrowOtherWidth, imageArrowOtherHeight, imageArrowOtherX, imageArrowOtherY;
    protected int textObjectOtherWidth, textObjectOtherHeight, textObjectOtherX, textObjectOtherY;

    // Use this for initialization
    protected void Start() {
        numMenuItems = 0;
        arrowPos = 0;
        Sprite[] arrowSprites = Resources.LoadAll<Sprite>(arrowLocation);
        Image[] tempImages = GetComponentsInChildren<Image>(true);
        foreach (Image image in tempImages) {
            if (image.name.Equals(imageBoxName)) {
                imageBox = image;
                imageBox.enabled = true;
            } else if (image.name.Equals(imageArrowName)) {
                image.GetComponent<UnityEngine.UI.Image>().sprite = arrowSprites[1];
                imageArrow = image;
                imageArrow.enabled = true;
            }
        }
        foreach (Transform child in transform) {
            if (child.name.Equals(textObjectName)) {
                textObject = child.gameObject;
                break;
            }
        }
        positionChoiceBoxElements();
    }

    // Update is called once per frame
    public void Update() {
        bool arrowPosChanged = false;
        if (Input.GetKeyUp(KeyCode.Alpha0)) {
            imageBox.enabled = false;
            imageArrow.enabled = false;
            textObject.GetComponent<Text>().enabled = false;
            Destroy(this);
        } else if (Input.GetKeyDown(KeyCode.G)) { //Down
            ++arrowPos;
            if (arrowPos == menuOptions.Count) {
                arrowPos = 0;
            }
            arrowPosChanged = true;
        } else if (Input.GetKeyDown(KeyCode.H)) { //Up
            --arrowPos;
            if (arrowPos < 0) {
                arrowPos = menuOptions.Count - 1;
            }
            arrowPosChanged = true;
        }
        if (arrowPosChanged) {
            if (Screen.width == 1024) {
                imageArrow.GetComponent<RectTransform>().anchoredPosition = new Vector3(arrow1024X, arrow1024Y - (arrowPos * arrow1024Spacing), 0);
            } else {
                imageArrow.GetComponent<RectTransform>().anchoredPosition = new Vector3(arrowOtherX, arrowOtherY - (arrowPos * arrowOtherSpacing), 0);
            }
        }
    }

    protected void positionChoiceBoxElements() {
        if (Screen.width == 1024) {
            imageBox.GetComponent<RectTransform>().sizeDelta = new Vector2(imageBox1024Width, menuOptions.Count*60);
            imageBox.GetComponent<RectTransform>().anchoredPosition = new Vector3(imageBox1024X, imageBox1024Y, 0);
            imageArrow.GetComponent<RectTransform>().sizeDelta = new Vector2(imageArrow1024Width, imageArrow1024Height);
            imageArrow.GetComponent<RectTransform>().anchoredPosition = new Vector3(imageArrow1024X, imageArrow1024Y, 0);
            textObject.GetComponent<RectTransform>().sizeDelta = new Vector2(textObject1024Width, textObject1024Height);
            textObject.GetComponent<RectTransform>().anchoredPosition = new Vector3(textObject1024X, textObject1024Y, 0);
        } else {
            imageBox.GetComponent<RectTransform>().sizeDelta = new Vector2(imageBoxOtherWidth, menuOptions.Count*60);
            imageBox.GetComponent<RectTransform>().anchoredPosition = new Vector3(imageBoxOtherX, imageBoxOtherY, 0);
            imageArrow.GetComponent<RectTransform>().sizeDelta = new Vector2(imageArrowOtherWidth, imageArrowOtherHeight);
            imageArrow.GetComponent<RectTransform>().anchoredPosition = new Vector3(imageArrowOtherX, imageArrowOtherY, 0);
            textObject.GetComponent<RectTransform>().sizeDelta = new Vector2(textObjectOtherWidth, textObjectOtherHeight);
            textObject.GetComponent<RectTransform>().anchoredPosition = new Vector3(textObjectOtherX, textObjectOtherY, 0);
        }
        //Fill menu text
        textObject.GetComponent<Text>().enabled = false;
        textObject.GetComponent<Text>().text = "";
        for (int i = 0; i<menuOptions.Count-1; ++i ) {
            textObject.GetComponent<Text>().text += menuOptions[i] + "\n";
        }
        textObject.GetComponent<Text>().text += menuOptions[menuOptions.Count-1];
        textObject.GetComponent<Text>().enabled = true;
    }

}
