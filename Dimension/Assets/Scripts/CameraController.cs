using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

//This is the object that the camera will follow
    public Transform lookAt;

// This is the offset of the camera's location in relation to the target lookAt
    public Vector3 offset = new Vector3(0, 0, -9.0f);

//This is the object that the camera will follow
	public Vector3 startPos;

//This controls the speed of our lerp
	public float lerpSpeed = 0.2f;


//***************************************************************************************
//	LateUpdate() - This function simply sets the position of the camera to be the object lookAt + our offset.
//  Lerp between current position adn cameras new position
//
//
//
//***************************************************************************************
	private void LateUpdate(){
		//Starting position to calculate from
		startPos = transform.position;
		//Lerp to new position
		transform.position = Vector3.Lerp (startPos, lookAt.transform.position + offset, lerpSpeed * Time.deltaTime);
    }
}
