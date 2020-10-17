using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
	public Camera[] cameras;
	public int currentCamIndex;
	
	// Start is called before the first frame update
    void Start()
    {
		// Sets up camera array and adds each camera attached to the player
		cameras = new Camera[2];
		cameras[0] = gameObject.GetComponent<GameManager>().player.transform.Find("Camera_FirstPOV").gameObject.GetComponent<Camera>();
		cameras[1] = gameObject.GetComponent<GameManager>().player.transform.Find("Camera_ThirdPOV").gameObject.GetComponent<Camera>();

		currentCamIndex = 0;
		
		for(int i = 0; i < cameras.Length; i++)
			if(i != currentCamIndex)
				cameras[i].gameObject.SetActive(false);
	}

    // Update is called once per frame
    void Update()
    {
		if(Input.GetKeyDown(KeyCode.C))
			ChangeCamera();
    }

	void ChangeCamera()
	{
		cameras[currentCamIndex].gameObject.SetActive(false);
		if(currentCamIndex == cameras.Length - 1)
			currentCamIndex = 0;
		else
			currentCamIndex++;
		cameras[currentCamIndex].gameObject.SetActive(true);
	}
}
