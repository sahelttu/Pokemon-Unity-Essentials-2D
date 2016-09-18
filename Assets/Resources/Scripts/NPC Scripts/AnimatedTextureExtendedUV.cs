using UnityEngine;
using System.Collections;


public class AnimatedTextureExtendedUV : MonoBehaviour
{
	public int colCount = 4;
	private string sheetname;
    private string oldSheetname;
    private Sprite[] sprites;
    private string[] names;
	private SpriteRenderer spRend;
	private Sprite sprite;
	private int hIndex = 0;
	private int vIndex = 0;
	private string charName;
	private int updateCount = 0;
	private Transform spTransform;
	public bool isGamePlayer = false;

	public int facing;
    private bool is_running;
    private float updateSpeed = 8f;


	void Start() {
        spRend = GetComponent<SpriteRenderer>();
        spTransform = GetComponent<Transform>();
        oldSheetname = spRend.sprite.name.Remove(spRend.sprite.name.LastIndexOf('_'));
        updateSpritesheetSet();
    }

    void updateSpritesheetSet() {
        sprites = Resources.LoadAll<Sprite>("Graphics/Characters/" + sheetname);
        names = new string[sprites.Length];

        for (int i = 0; i < names.Length; i++) {
            names[i] = sprites[i].name;
        }
    }

    public void changeSpriteSheet(string newSpriteSheetEnd, float newupdateSpeed = 8f) {
        sheetname = oldSheetname + newSpriteSheetEnd;
        updateSpritesheetSet();
        updateSpeed = newupdateSpeed;
    }

    public void revertSpritesheet() {
        sheetname = oldSheetname;
        updateSpritesheetSet();
        updateSpeed = 8f;
    }


	//SetSpriteAnimation
	public IEnumerator UpdateSpriteAnimation() {
		// split into horizontal and vertical index

		int counter = 0;

		for (int i = 0; i<2; i++) {
			hIndex += 1;
			counter+=1;

			if (hIndex>3)
				hIndex = 0;

			if (facing==1) {
					vIndex = 3;
			} else if (facing == -1) {
					vIndex = 0;
			} else if (facing == 2) {
					vIndex = 2;
			} else if (facing == -2) {
					vIndex = 1;
			}

			ChangeSprite(4*vIndex + hIndex);
			yield return new WaitForSeconds(1f/updateSpeed);
		}
		ChangeSprite(4*vIndex + hIndex);
	}

	void ChangeSprite( int index ) {
        spRend.sprite = sprites[index];
    }

    void ChangeSpriteByName( string name ) {
        spRend.sprite = sprites[System.Array.IndexOf(names, name)];
    }


	public void setFacing(int p_direction) {
		facing = p_direction;
		if (facing==1) {
				vIndex = 3;
		} else if (facing == -1) {
				vIndex = 0;
		} else if (facing == 2) {
				vIndex = 2;
		} else if (facing == -2) {
				vIndex = 1;
		}
		ChangeSprite(4*vIndex + hIndex);
	}

}
