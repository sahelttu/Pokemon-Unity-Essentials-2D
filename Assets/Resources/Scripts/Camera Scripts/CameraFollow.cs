using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

	public float dampTime = 0.01f;
  private Vector3 velocity = Vector3.zero;
  public Transform target;
	public Vector3 offset;
	private Camera mainCam;

	void Start() {
		mainCam = GetComponent<Camera> ();
	}

  // Update is called once per frame
  void LateUpdate ()
  {
     if (target)
     {
         Vector3 point = mainCam.WorldToViewportPoint(target.position);
         Vector3 delta = target.position - mainCam.ViewportToWorldPoint(new Vector3(0.5f, 0.55f, point.z));
         Vector3 destination = transform.position + delta;
         transform.position = Vector3.SmoothDamp(transform.position, destination + offset, ref velocity, dampTime);
     }

  }
}
