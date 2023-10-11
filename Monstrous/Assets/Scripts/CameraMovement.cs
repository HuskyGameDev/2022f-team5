using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Monstrous.Camera{

	public class CameraMovement : MonoBehaviour 
	{
		// all can be assigned in unity or code.
		public Transform target;
		public float smoothing;

		
		void FixedUpdate () 
		{
			moveCamera();
		}

		void moveCamera()
		{
			//Debug.Log(transform.position);
			if(transform.position != target.position)
			{
				//sets cameras goal position to the target
				Vector3 targetPosition = new Vector3(target.position.x,target.position.y, transform.position.z);
				
				//moves the camera to the desired position
				transform.position = Vector3.Lerp(transform.position,targetPosition, smoothing);
				//return Vector3.Lerp(transform.position,targetPosition, smoothing);
			}
		}

		public void knockCam()
		{
			//transform.position = new Vector3(transform.position.x-0.5f, transform.position.y, transform.position.z);
			//Debug.Log("knock camera");
		}

		public IEnumerator knockCam2(float dur)
		{
			Vector3 original = transform.position;

			float time = 0.3f;
			while(time < dur)
			{
				transform.position = new Vector3(transform.position.x-0.5f, transform.position.y, transform.position.z);
				time += Time.deltaTime;
				yield return null;
			}

			transform.position = original;
		}
	}
}	