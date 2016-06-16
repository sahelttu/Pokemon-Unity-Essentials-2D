﻿using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

	Vector3 pos;
	Vector3 oldPos;                        // For movement
	float speed = 1.0f;                         // Speed of movement
	float distance = 0.32f;
	private Rigidbody2D rbody;
	private Animator anim;


	private float[] movingTimes = {0.0f, 0.0f, 0.0f, 0.0f};
	private Vector3[] movingVectors;
	private int[,] xyCoords = new int[,] { {0,1}, {-1,0}, {0,-1}, {1,0}  };

	public int facing = 0;


	private string strDirection = "";
	private int intDirection = 0;
	bool transferred = false;

	MapPositionWatcher positionWatcher;


	// Use this for initialization
	void Start () {
		pos = transform.position;          // Take the initial position
		oldPos = pos;
		rbody = GetComponent<Rigidbody2D> ();
		anim = GetComponent<Animator> ();
		positionWatcher = GetComponent<MapPositionWatcher>();

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
			for (int i = 0; i < movingTimes.Length; i++){
				movingTimes[i] = 0.0f;
			}
			anim.SetBool ("is_walking", false);
		}
		else {
			 //prefer button player is currently holding down first, determine which button is being pushed
			 if (strDirection.Equals("") || !Input.GetButton(strDirection)){
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
					 for (int i = 0; i < movingTimes.Length; i++){
						 movingTimes[i] = 0.0f;
					 }
				 }
			 }



			 if(!strDirection.Equals("") && Input.GetButton(strDirection) && transform.position == pos) {  //move in predetermined direction
					for (int i = 0; i < movingTimes.Length; i++){
						if (i == intDirection){
							movingTimes[i] += Time.deltaTime;
						} else {
							movingTimes[i] = 0.0f;
						}
					}
					if ( movingTimes[intDirection] > 0.1f ) {
						if (PassabilityCheck.canPass(rbody, movingVectors[intDirection], distance)){
							pos += movingVectors[intDirection];
							anim.SetBool ("is_walking", true);
						} else {
							anim.SetBool ("is_walking", false);
							AudioController.playSE("bump.mp3");
						}
					} else {
						anim.SetBool ("is_walking", false);
					}
					anim.SetFloat("input_x", xyCoords[intDirection, 0]);
					anim.SetFloat("input_y", xyCoords[intDirection, 1]);
			 } else if (transform.position == pos){
				 anim.SetBool ("is_walking", false);
			 }
		 }

		 transform.position = Vector3.MoveTowards(transform.position, pos, Time.deltaTime * speed);    // Move there


		 if(oldPos!=pos && Vector3.Distance(transform.position, pos) == 0.0f) {
			 if (strDirection.Equals("") || !Input.GetButton(strDirection)){
			 	anim.SetBool ("is_walking", false);
			 }
			 positionWatcher.updatePosition();
			 oldPos = pos;
		 }

		 if (Input.GetKey(KeyCode.Space)){
			 UIManager.displayText("This is also a test.  In fact, this is a really long test that will extend to the next line.  Or maybe it won't.");
		 }
	}

	public void setTransferred(bool hasTransferred, int setDirection){
		transferred = hasTransferred;
		if (setDirection>=-2 && setDirection<=2 && setDirection!=0){
			Debug.Log(setDirection);
			facing = setDirection;
			updateFacing();
		}
	}

	void updateFacing(){
		anim.SetFloat("input_x", 0);
		anim.SetFloat("input_y", 0);
		//face character based on facing var
		if (facing == 1 || facing == -1){
			anim.SetFloat("input_x", facing);
		}
		if (facing == 2 || facing == -2) {
			anim.SetFloat("input_y", facing);
		}
	}




}
