﻿using UnityEngine;
using System.Collections;

public class NPCController : MonoBehaviour {

	private bool is_walking;
	public int facing = -1;  //start down

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

	}

	public int getFacing(){
		return facing;
	}

	public bool getWalking(){
		return is_walking;
	}

}
