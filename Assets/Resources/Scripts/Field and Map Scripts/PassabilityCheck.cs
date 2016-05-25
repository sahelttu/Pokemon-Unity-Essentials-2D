﻿using UnityEngine;
using System.Collections;

public class PassabilityCheck : MonoBehaviour {



	public static bool canPass(Rigidbody2D player, Vector2 target, float distance){
		bool ret = true;

		RaycastHit2D hit = Physics2D.Raycast(player.transform.position, target);


		//reverse raycast, to determine if we're inside a collider
		RaycastHit2D hitReverse = Physics2D.Raycast(target, player.transform.position);


		if (hit.collider != null){
			if (hit.distance <= distance){
				if (hit.collider.gameObject.GetComponent<TerrainTagChecker>() != null) {
					TerrainType type = hit.collider.gameObject.GetComponent<TerrainTagChecker>().getTerrainType();
					Debug.Log(type);
				} else if (hit.distance <= distance){
					return false;
				}
			}
		} else if (hit.rigidbody != null) {
			Debug.Log(hit.rigidbody.gameObject.name);
		}

		return ret;
	}

}
