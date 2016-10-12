using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

	Vector3 pos;
	Vector3 oldPos;
	float speed = 1.2f;                   	// Speed of movement
	float distance = 0.307355f;				//Distance between tiles, don't change this value
	Rigidbody2D rbody;
	public AnimatedTextureExtendedUV anim;	//animates the player using the charset
    SpriteRenderer spRend;
	float[] movingTimes = {0.0f, 0.0f, 0.0f, 0.0f};
	Vector3[] movingVectors;
	int[,] xyCoords = new int[,] { {0,1}, {-2,0}, {0,-1}, {2,0}  };
    string strDirection = "";
    int intDirection = 0;
    bool transferred = false;
    bool isRunning = false;
    public int facing = 0;


	
    float runningSpeed = 2.0f;
    float runningFrameSpeed = 10f;


    MapPositionWatcher positionWatcher;


	// Use this for initialization
	void Start () {
		pos = transform.position;          // Take the initial position
		oldPos = pos;
		rbody = GetComponent<Rigidbody2D> ();
		positionWatcher = GetComponent<MapPositionWatcher>();
        spRend = GetComponent<SpriteRenderer>();

		//used to choose the movement destination
		movingVectors = new Vector3[4] {
			new Vector3(0.0f, distance, 0.0f),
			new Vector3(-1*distance, 0.0f, 0.0f),
			new Vector3(0.0f, -1*distance, 0.0f),
			new Vector3(distance, 0.0f, 0.0f)
		};

		updateFacing();
	}


	// Update is called once per frame
	void Update () {
		if (transferred) {
			transferred = false;
			pos = transform.position;
			for (int i = 0; i < movingTimes.Length; i++) {
				movingTimes[i] = 0.0f;
			}
		}
		else {
			 //prefer button player is currently holding down first, determine which button is being pushed
			 if (strDirection.Equals("") || !Input.GetButton(strDirection)) {
				 if (Input.GetButton("Up")) {
					 strDirection = "Up";
					 intDirection = 0;
				 } else if (Input.GetButton("Left")) {
					 strDirection = "Left";
					 intDirection = 1;
				 } else if (Input.GetButton("Down")) {
					 strDirection = "Down";
					 intDirection = 2;
				 } else if (Input.GetButton("Right")) {
					 strDirection = "Right";
					 intDirection = 3;
				 } else {
					 strDirection = "";
					 for (int i = 0; i < movingTimes.Length; i++) {
						 movingTimes[i] = 0.0f;
					 }
                }
			 }



		    if(!strDirection.Equals("") && Input.GetButton(strDirection) && transform.position == pos) {  //move in predetermined direction
                //If holding shift, start running
                if (Input.GetButton("Shift")) {
                    anim.changeSpriteSheet("_run", runningFrameSpeed);
                    isRunning = true;
                } else {
                    anim.revertSpritesheet();
                    isRunning = false;
                }
		        for (int i = 0; i < movingTimes.Length; i++) {
			        if (i == intDirection) {
				        movingTimes[i] += Time.deltaTime;
			        } else {
				        movingTimes[i] = 0.0f;
			        }
		        }
		        //must be pressing down the directional button a bit to move, to distinguish between turning and walking
		        if ( movingTimes[intDirection] > 0.1f) {
			        //check if the destination will have
			        if (PassabilityCheck.canPass(rbody, movingVectors[intDirection], distance)) {
				        StartCoroutine(anim.UpdateSpriteAnimation());
				        pos += movingVectors[intDirection];
			        } else {
				        AudioController.playSE("bump.mp3");
			        }
		        } 
		        if (xyCoords[intDirection, 0]!=0) {
			        anim.setFacing(xyCoords[intDirection, 0]);
		        } else {
			        anim.setFacing(xyCoords[intDirection, 1]);
		        }
		    } 
		 }

         if (isRunning) {
            transform.position = Vector3.MoveTowards(transform.position, pos, runningSpeed * Time.deltaTime);    // Move there
        } else {
            transform.position = Vector3.MoveTowards(transform.position, pos, speed * Time.deltaTime);    // Move there
        }


		if(oldPos!=pos && Vector3.Distance(transform.position, pos) == 0.0f) {
            //only update Z position if moving up or down
            if (strDirection.Equals("Up") || strDirection.Equals("Down")) {
                positionWatcher.updatePosition();
                updatePlayerZPosition();
            }
            oldPos = pos;
            anim.revertSpritesheet();
            isRunning = false;
        }

		if (Input.GetKeyUp(KeyCode.Space)) {
            UIManager.showPauseMenu();
            //UIManager.displayText("This is also a test.  In fact, this is a really long testsss that will extend to the next line.  Or maybe it won't.  Yeah, this is a super long sentece, it will probably never end.");
        }
	}

	public void setTransferred(bool hasTransferred, int setDirection) {
		transferred = hasTransferred;
		if (setDirection>=-2 && setDirection<=2 && setDirection!=0) {
			Debug.Log(setDirection);
			facing = setDirection;
			updateFacing();
		}
	}

	void updateFacing() {
		anim.setFacing(facing);
	}

	public void setSpeed(float p_speed) {
		speed = p_speed;
	}

    public void updatePlayerZPosition() {
        spRend.sortingOrder = Mathf.Abs((int)( (gameObject.transform.position.y - positionWatcher.highestYValue) / 0.307385f));
    }


}
