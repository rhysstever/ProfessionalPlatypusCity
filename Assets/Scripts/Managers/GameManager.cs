using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	public GameObject player;
	public Dictionary<string, GameObject> npcs;
	public GameObject adjacentNPC;
	
	// Start is called before the first frame update
    void Awake()
    {
		adjacentNPC = null;
		npcs = new Dictionary<string, GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
		FindNearbyNPC();
	}

	void FindNearbyNPC()
	{
		adjacentNPC = null;
		foreach(KeyValuePair<string, GameObject> npc in npcs)
		{
			if(Vector3.Distance(player.transform.position, npc.Value.transform.position) < 10.0f)
			{
				adjacentNPC = npc.Value;
				Vector3 lookAtPos = new Vector3(
					player.transform.position.x,
					player.GetComponent<BoxCollider>().size.y / 2,
					player.transform.position.z);
				npc.Value.transform.LookAt(lookAtPos);
			}
		}
	}
}
