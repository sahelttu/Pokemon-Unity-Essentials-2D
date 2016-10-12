using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PauseMenuController : MonoBehaviour {

    private Image pauseBox;
    private Image pauseArrow;
    private GameObject pauseText;
    private int numMenuItems;
    private int arrowPos;


    void Start() {
        numMenuItems = 0;
        arrowPos = 0;
        Sprite[] arrowSprites = Resources.LoadAll<Sprite>("Graphics/Pictures/arrows");
        Image[] tempImages = GetComponentsInChildren<Image>(true);
        foreach (Image image in tempImages) {
            if (image.name.Equals("Pause Skin")) {
                pauseBox = image;
                pauseBox.enabled = true;
            } else if (image.name.Equals("Pause Selector")) {
                image.GetComponent<UnityEngine.UI.Image>().sprite = arrowSprites[1];
                pauseArrow = image;
                pauseArrow.enabled = true;
            }
        }
        foreach (Transform child in transform) {
            if (child.name.Equals("Pause Text")) {
                pauseText = child.gameObject;
                break;
            }
        }
        positionPauseMenu();
        fillPauseText();
    }

    void Update() {
        if (Input.GetKeyUp(KeyCode.Space)) {
            pauseBox.enabled = false;
            pauseArrow.enabled = false;
            pauseText.GetComponent<Text>().enabled = false;
            Destroy(this);
        } else if (Input.GetKeyDown(KeyCode.G)) { //Down
            ++arrowPos;
            if (arrowPos==numMenuItems) {
                arrowPos = 0;
            }
            if (Screen.width == 1024) {
                pauseArrow.GetComponent<RectTransform>().anchoredPosition = new Vector3(-275, 22 - arrowPos * 52, 0);
            } else {
                pauseArrow.GetComponent<RectTransform>().anchoredPosition = new Vector3(-292, 42 - arrowPos * 52, 0);
            }
        } else if (Input.GetKeyDown(KeyCode.H)) { //Up
            --arrowPos;
            if (arrowPos <0) {
                arrowPos = numMenuItems-1;
            }
            if (Screen.width == 1024) {
                pauseArrow.GetComponent<RectTransform>().anchoredPosition = new Vector3(-275, 22 - arrowPos * 52, 0);
            } else {
                pauseArrow.GetComponent<RectTransform>().anchoredPosition = new Vector3(-292, 42 - arrowPos * 52, 0);
            }
        }
    }

    void positionPauseMenu() {
        if (Screen.width == 1024) {
            pauseBox.GetComponent<RectTransform>().sizeDelta = new Vector2(290, 300);
            pauseBox.GetComponent<RectTransform>().anchoredPosition = new Vector3(-15, 50, 0);
            pauseText.GetComponent<RectTransform>().sizeDelta = new Vector2(120, 280);
            pauseText.GetComponent<RectTransform>().anchoredPosition = new Vector3(-140, 25, 0);
            pauseArrow.GetComponent<RectTransform>().sizeDelta = new Vector2(16, 32);
            pauseArrow.GetComponent<RectTransform>().anchoredPosition = new Vector3(-275, 22, 0);
        } else {
            pauseBox.GetComponent<RectTransform>().sizeDelta = new Vector2(290, 300);
            pauseBox.GetComponent<RectTransform>().anchoredPosition = new Vector3(-35, 70, 0);
            pauseText.GetComponent<RectTransform>().sizeDelta = new Vector2(120, 280);
            pauseText.GetComponent<RectTransform>().anchoredPosition = new Vector3(-155, 45, 0);
            pauseArrow.GetComponent<RectTransform>().sizeDelta = new Vector2(16, 32);
            pauseArrow.GetComponent<RectTransform>().anchoredPosition = new Vector3(-292, 42, 0);
        }
    }

    void fillPauseText() {
        pauseText.GetComponent<Text>().text = "";
        //Add each menu item
        pauseText.GetComponent<Text>().text += "Pokédex";
        pauseText.GetComponent<Text>().text += "\nPokémon";
        pauseText.GetComponent<Text>().text += "\nBag";
        numMenuItems = 3;
        pauseText.GetComponent<Text>().enabled = true;
    }

}
