using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour {
	// all can be assigned in unity or code.
	public Transform target;
	public float smoothing;

	
	void FixedUpdate () 
	{
		//Debug.Log(transform.position);
		if(transform.position != target.position)
		{
			//sets cameras goal position to the target
			Vector3 targetPosition = new Vector3(target.position.x,target.position.y, transform.position.z);
			
			//moves the camera to the desired position
			transform.position = Vector3.Lerp(transform.position,targetPosition, smoothing);
		}
}

}
	
	