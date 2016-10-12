using UnityEngine;
using System.Collections;

public class UIManager : MonoBehaviour {

    public static GameObject uiManager;
    public static GameObject messageSystem;
    public static GameObject pauseMenu;


    public static void displayText(string textToDisplay) {
        if (uiManager == null) {
            uiManager = GameObject.FindGameObjectWithTag("UIManager");
        }
        if (messageSystem == null) {
            UIManager.generateMessageSystem();
        }
        if (messageSystem.GetComponent<DisplayText>() == null) {
            messageSystem.AddComponent<DisplayText>();
        }
        messageSystem.GetComponent<DisplayText>().SetText(textToDisplay);
    }

    public static void showPauseMenu() {
        if (uiManager == null) {
            uiManager = GameObject.FindGameObjectWithTag("UIManager");
        }
        if (pauseMenu == null) {
            UIManager.generatePauseMenu();
        }
        pauseMenu.AddComponent<PauseMenuController>();
    }

    //Generate Message System objects via code
    public static void generateMessageSystem() {
        //Create Message System object
        messageSystem = new GameObject("Message System");
        messageSystem.transform.parent = uiManager.transform;
        messageSystem.tag = "MessageSystem";
        messageSystem.layer = SortingLayer.GetLayerValueFromName("UI");
        //add canvas
        messageSystem.AddComponent<Canvas>();
        messageSystem.GetComponent<Canvas>().renderMode = RenderMode.ScreenSpaceOverlay;
        messageSystem.GetComponent<Canvas>().pixelPerfect = true;
        //add canvas scaler
        messageSystem.AddComponent<UnityEngine.UI.CanvasScaler>();
        messageSystem.GetComponent<UnityEngine.UI.CanvasScaler>().uiScaleMode = UnityEngine.UI.CanvasScaler.ScaleMode.ScaleWithScreenSize;
        messageSystem.GetComponent<UnityEngine.UI.CanvasScaler>().referenceResolution = new Vector2(1024, 768);
        messageSystem.GetComponent<UnityEngine.UI.CanvasScaler>().screenMatchMode = UnityEngine.UI.CanvasScaler.ScreenMatchMode.Expand;
        messageSystem.GetComponent<UnityEngine.UI.CanvasScaler>().referencePixelsPerUnit = 16;
        //Add graphics raycaster
        messageSystem.AddComponent<UnityEngine.UI.GraphicRaycaster>();
        messageSystem.AddComponent<CanvasRenderer>();

        //Create Message Skin object
        GameObject messageSkin = new GameObject("Message Skin");
        messageSkin.transform.parent = messageSystem.transform;
        messageSkin.layer = SortingLayer.GetLayerValueFromName("UI");
        //adjust Rect Transform
        messageSkin.AddComponent<RectTransform>();
        messageSkin.GetComponent<RectTransform>().position = new Vector3(-8, 56, 0);
        messageSkin.GetComponent<RectTransform>().sizeDelta = new Vector2(1012, 128);
        messageSkin.GetComponent<RectTransform>().anchorMin = new Vector2(0, 0);
        messageSkin.GetComponent<RectTransform>().anchorMax = new Vector2(0, 0);
        messageSkin.GetComponent<RectTransform>().pivot = new Vector2(0, 0);
        messageSkin.AddComponent<CanvasRenderer>();
        messageSkin.AddComponent<UnityEngine.UI.Image>();
        messageSkin.GetComponent<UnityEngine.UI.Image>().enabled = false;
        messageSkin.GetComponent<UnityEngine.UI.Image>().sprite = Resources.Load("Graphics/WindowSkins/speech hgss 2", typeof(Sprite)) as Sprite;
        messageSkin.GetComponent<UnityEngine.UI.Image>().type = UnityEngine.UI.Image.Type.Sliced;
        //add canvas
        messageSkin.AddComponent<Canvas>();
        messageSkin.GetComponent<Canvas>().pixelPerfect = true;

        //Create Message BG object
        GameObject messageBG = new GameObject("Message BG");
        messageBG.transform.parent = messageSystem.transform;
        messageBG.layer = SortingLayer.GetLayerValueFromName("UI");
        //adjust Rect Transform
        messageBG.AddComponent<RectTransform>();
        messageBG.GetComponent<RectTransform>().position = new Vector3(14.15f, 70, 0);
        messageBG.GetComponent<RectTransform>().sizeDelta = new Vector2(945.7f, 100);
        messageBG.GetComponent<RectTransform>().anchorMin = new Vector2(0, 0);
        messageBG.GetComponent<RectTransform>().anchorMax = new Vector2(0, 0);
        messageBG.GetComponent<RectTransform>().pivot = new Vector2(0, 0);
        messageBG.AddComponent<CanvasRenderer>();
        messageBG.AddComponent<UnityEngine.UI.Image>();
        messageBG.GetComponent<UnityEngine.UI.Image>().enabled = false;
        messageBG.GetComponent<UnityEngine.UI.Image>().color = Color.white;
        messageBG.GetComponent<UnityEngine.UI.Image>().raycastTarget = true;
        messageBG.GetComponent<UnityEngine.UI.Image>().preserveAspect = false;
        //add canvas
        messageBG.AddComponent<Canvas>();
        messageBG.GetComponent<Canvas>().pixelPerfect = true;

        //Create Message Text object
        //Create Message BG object
        GameObject messageText = new GameObject("Message Text");
        messageText.transform.parent = messageSystem.transform;
        messageText.layer = SortingLayer.GetLayerValueFromName("UI");
        //adjust Rect Transform
        messageText.AddComponent<RectTransform>();
        messageText.GetComponent<RectTransform>().position = new Vector3(40, 115, 0);
        messageText.GetComponent<RectTransform>().sizeDelta = new Vector2(860, 96);
        messageText.GetComponent<RectTransform>().anchorMin = new Vector2(0, 0);
        messageText.GetComponent<RectTransform>().anchorMax = new Vector2(0, 0);
        messageText.GetComponent<RectTransform>().pivot = new Vector2(0, 0.5f);
        messageText.AddComponent<CanvasRenderer>();
        messageText.AddComponent<UnityEngine.UI.Text>();
        messageText.GetComponent<UnityEngine.UI.Text>().font = FontManager.getFont("Power Clear");
        messageText.GetComponent<UnityEngine.UI.Text>().fontSize = 42;
        messageText.GetComponent<UnityEngine.UI.Text>().lineSpacing = 1.7f;
        messageText.GetComponent<UnityEngine.UI.Text>().supportRichText = true;
        messageText.GetComponent<UnityEngine.UI.Text>().horizontalOverflow = HorizontalWrapMode.Overflow;
        messageText.GetComponent<UnityEngine.UI.Text>().color = Color.black;
    }

    //Generate Pause Menu Objects via code
    public static void generatePauseMenu() {
        //Create Pause Menu object
        pauseMenu = new GameObject("Pause Menu");
        pauseMenu.transform.parent = uiManager.transform;
        pauseMenu.layer = SortingLayer.GetLayerValueFromName("UI");
        //add canvas
        pauseMenu.AddComponent<Canvas>();
        pauseMenu.GetComponent<Canvas>().renderMode = RenderMode.ScreenSpaceOverlay;
        pauseMenu.GetComponent<Canvas>().pixelPerfect = true;
        //add canvas scaler
        pauseMenu.AddComponent<UnityEngine.UI.CanvasScaler>();
        pauseMenu.GetComponent<UnityEngine.UI.CanvasScaler>().uiScaleMode = UnityEngine.UI.CanvasScaler.ScaleMode.ScaleWithScreenSize;
        pauseMenu.GetComponent<UnityEngine.UI.CanvasScaler>().referenceResolution = new Vector2(1024, 768);
        pauseMenu.GetComponent<UnityEngine.UI.CanvasScaler>().screenMatchMode = UnityEngine.UI.CanvasScaler.ScreenMatchMode.Expand;
        pauseMenu.GetComponent<UnityEngine.UI.CanvasScaler>().referencePixelsPerUnit = 16;
        //Add graphics raycaster
        pauseMenu.AddComponent<UnityEngine.UI.GraphicRaycaster>();
        pauseMenu.AddComponent<CanvasRenderer>();

        //Create Pause Skin object
        GameObject pauseSkin = new GameObject("Pause Skin");
        pauseSkin.transform.parent = pauseMenu.transform;
        pauseSkin.layer = SortingLayer.GetLayerValueFromName("UI");
        //adjust Rect Transform
        pauseSkin.AddComponent<RectTransform>();
        pauseSkin.GetComponent<RectTransform>().anchorMin = new Vector2(1, 1);
        pauseSkin.GetComponent<RectTransform>().anchorMax = new Vector2(1, 1);
        pauseSkin.GetComponent<RectTransform>().pivot = new Vector2(1, 1);
        pauseSkin.AddComponent<CanvasRenderer>();
        pauseSkin.AddComponent<UnityEngine.UI.Image>();
        pauseSkin.GetComponent<UnityEngine.UI.Image>().enabled = false;
        pauseSkin.GetComponent<UnityEngine.UI.Image>().sprite = Resources.Load("Graphics/WindowSkins/choice 1", typeof(Sprite)) as Sprite;
        pauseSkin.GetComponent<UnityEngine.UI.Image>().type = UnityEngine.UI.Image.Type.Sliced;
        //add canvas
        pauseSkin.AddComponent<Canvas>();
        pauseSkin.GetComponent<Canvas>().pixelPerfect = true;
        //Create Message BG object
        GameObject pauseText = new GameObject("Pause Text");
        pauseText.transform.parent = pauseMenu.transform;
        pauseText.layer = SortingLayer.GetLayerValueFromName("UI");
        //adjust Rect Transform
        pauseText.AddComponent<RectTransform>();
        pauseText.GetComponent<RectTransform>().anchorMin = new Vector2(1, 1);
        pauseText.GetComponent<RectTransform>().anchorMax = new Vector2(1, 1);
        pauseText.GetComponent<RectTransform>().pivot = new Vector2(1, 1);
        pauseText.AddComponent<CanvasRenderer>();
        pauseText.AddComponent<UnityEngine.UI.Text>();
        pauseText.GetComponent<UnityEngine.UI.Text>().font = FontManager.getFont("Power Clear");
        pauseText.GetComponent<UnityEngine.UI.Text>().fontSize = 42;
        pauseText.GetComponent<UnityEngine.UI.Text>().lineSpacing = 2.0f;
        pauseText.GetComponent<UnityEngine.UI.Text>().supportRichText = true;
        pauseText.GetComponent<UnityEngine.UI.Text>().horizontalOverflow = HorizontalWrapMode.Overflow;
        pauseText.GetComponent<UnityEngine.UI.Text>().color = Color.black;
        //Create Pause Arrow object
        GameObject pauseArrow = new GameObject("Pause Selector");
        pauseArrow.transform.parent = pauseMenu.transform;
        pauseArrow.layer = SortingLayer.GetLayerValueFromName("UI");
        //adjust Rect Transform
        pauseArrow.AddComponent<RectTransform>();
        pauseArrow.GetComponent<RectTransform>().anchorMin = new Vector2(1, 1);
        pauseArrow.GetComponent<RectTransform>().anchorMax = new Vector2(1, 1);
        pauseArrow.GetComponent<RectTransform>().pivot = new Vector2(1, 1);
        pauseArrow.AddComponent<CanvasRenderer>();
        pauseArrow.AddComponent<UnityEngine.UI.Image>();
        pauseArrow.GetComponent<UnityEngine.UI.Image>().enabled = false;
        pauseArrow.GetComponent<UnityEngine.UI.Image>().type = UnityEngine.UI.Image.Type.Sliced;
        //add canvas
        pauseArrow.AddComponent<Canvas>();
        pauseArrow.GetComponent<Canvas>().pixelPerfect = true;
    }
}
