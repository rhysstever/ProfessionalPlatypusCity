using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPC : MonoBehaviour
{
	public string npcName;
    Canvas canvas;
	
	// Start is called before the first frame update
    void Start()
    {
        // Sets up canvas and displays the NPC's name
        canvas = gameObject.transform.Find("Canvas").GetComponent<Canvas>();
        canvas.transform.Find("Text").gameObject.GetComponent<Text>().text = npcName;

		// Add it to the Dictionary in the GameManager script
		GameObject.FindWithTag("GameManager").GetComponent<NPCManager>().npcs.Add(npcName, gameObject);
    }

    // Update is called once per frame
    void Update()
    {
		canvas.transform.LookAt(
            GameObject.FindWithTag("GameManager").GetComponent<CameraManager>().cameras[
                GameObject.FindWithTag("GameManager").GetComponent<CameraManager>().currentCamIndex
                ].transform.position);
    }
}
