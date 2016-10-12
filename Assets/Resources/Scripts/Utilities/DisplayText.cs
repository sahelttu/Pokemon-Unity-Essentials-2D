using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DisplayText : MonoBehaviour {

	float letterPauseTime = 0.02f;
	string words = "";
	private int curChar = 0;
	private string currentSentence = "";

	private bool isTyping = false;
	private bool finishedTyping = false;

	private Image textBox;
	private Image textBG;
	private Text displayText;
	private GameObject textObj;

	void Start() {

		Image[] tempImages = GetComponentsInChildren<Image>(true);
		foreach (Image image in tempImages) {
			if (image.name.Equals("Message Skin")) {
				textBox = image;
				textBox.enabled = true;
			}
			if (image.name.Equals("Message BG")) {
				textBG = image;
				textBG.enabled = true;
			}
		}

		foreach (Transform child in transform) {
			if (child.name.Equals("Message Text")) {
			 	textObj = child.gameObject;
				break;
			}
		}

		positionTextBox();
		InvokeRepeating ("updateText", 0, letterPauseTime);
	}

	//Positioning is different depending on screen size, reposition when drawn
	//Only 512x384 and 1024x786 supported
	void positionTextBox() {
        textObj.GetComponent<Text>().font.material.mainTexture.filterMode = FilterMode.Point;
        if (Screen.width == 1024) {
            textBox.GetComponent<RectTransform>().anchoredPosition = new Vector3( -8, 56, 0);
            textBG.GetComponent<RectTransform>().anchoredPosition = new Vector3( 14.15f, 70, 0);
            textObj.GetComponent<RectTransform>().anchoredPosition = new Vector3( 40, 115, 0);
            textObj.GetComponent<Text>().rectTransform.sizeDelta = new Vector2( 860, 96);
        } else {
            textBox.GetComponent<RectTransform>().anchoredPosition = new Vector3(-25, 106, 0);

            textBG.GetComponent<RectTransform>().anchoredPosition = new Vector3(-4, 120, 0);

            textObj.GetComponent<RectTransform>().anchoredPosition = new Vector3(20, 165, 0);
            textObj.GetComponent<Text>().rectTransform.sizeDelta = new Vector2(445, 96);
        }

    }

	void updateText() {
		if (!isTyping && !finishedTyping && !waitingForMoreLines) {
			TypeText();
		}
        if (waitingForMoreLines && Input.GetButton("Enter")) {
            waitingForMoreLines = false;
            currentSentence = "";
            TypeText();
        } else if (finishedTyping && Input.GetButton("Enter")) {
			textBox.enabled = false;
			textBG.enabled = false;
            displayText.text = "";
            displayText.enabled = false;
			CancelInvoke ("updateText");
			Destroy(this);
		}
	}

	public void SetText(string newText)
	{
        displayText = GetComponentInChildren<Text>();
		displayText.enabled = true;
		if (FontManager.hasFont("Power Green")) {
			displayText.font = FontManager.getFont("Power Green");
		}
		words = newText;


	}

	private bool onNewWord = true;
	private string curLine = "";
    private int numLines = 1;
    private bool waitingForMoreLines = false;

	void TypeText() {
		char[] tempWords = words.ToCharArray();


		string tempLine = curLine;
		int tempChar = curChar;
		bool newLine = false;

		if (onNewWord) {
			while (tempChar<tempWords.Length && tempWords [tempChar] != ' ') {

                if (displayText.rectTransform.sizeDelta.x > displayText.cachedTextGeneratorForLayout.GetPreferredWidth (tempLine, displayText.GetGenerationSettings (displayText.GetComponent<RectTransform> ().rect.size))) {             
					tempLine += tempWords [tempChar];
					tempChar++;
				} else {
					newLine = true;
					break;
				}
			}
			onNewWord = false;
		}


		if (newLine) {
            if (numLines > 1) {
                numLines = 1;
                curLine = "";
                waitingForMoreLines = true;
                return;
            } else {
                currentSentence += '\n';
                curLine = "";
                numLines += 1;
            }
        }


		currentSentence+= tempWords[curChar];
		curLine += tempWords [curChar];
		if (tempWords [curChar] == ' ') {
			onNewWord = true;
		}
		curChar++;



		if (curChar>=tempWords.Length) {
			isTyping = false;
			curChar = 0;
			finishedTyping = true;
		}

	}


	void OnGUI() {
		displayText.text = currentSentence;
	}



}
