using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	public GameObject player;
	public GameObject[] npcsInScene;
	public Dictionary<string, GameObject> npcs;
	public GameObject adjacentNPC;
	
	// Start is called before the first frame update
    void Start()
    {
		adjacentNPC = null;
		npcs = new Dictionary<string, GameObject>();

		for(int i = 0; i < npcsInScene.Length; i++)
			npcs.Add(npcsInScene[i].GetComponent<NPC>().npcName, npcsInScene[i]);
    }

    // Update is called once per frame
    void Update()
    {
		FindNearbyNPC();
	}

	void FindNearbyNPC()
	{
		foreach(KeyValuePair<string, GameObject> npc in npcs)
		{
			if(Vector3.Distance(player.transform.position, npc.Value.transform.position) < 10.0f)
			{
				adjacentNPC = npc.Value;
				npc.Value.transform.LookAt(player.transform);
			}
		}
	}
}
