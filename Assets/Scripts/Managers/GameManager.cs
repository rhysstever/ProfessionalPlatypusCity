using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public enum MenuState
{
	main,
	controls,
	game,
	pause,
	gameOver
}

public class GameManager : MonoBehaviour
{
	public GameObject player;
	public Dictionary<string, GameObject> npcs;
	public GameObject adjacentNPC;
	public MenuState currentMenu;
	
	// Start is called before the first frame update
    void Awake()
    {
		adjacentNPC = null;
		npcs = new Dictionary<string, GameObject>();
		ChangeMenuState(MenuState.main);
    }

    // Update is called once per frame
    void Update()
    {
		FindNearbyNPC(10.0f);

		switch(currentMenu)
		{
			case MenuState.main:
				break;
			case MenuState.controls:
				break;
			case MenuState.game:
				// To pause the game
				if(Input.GetKeyDown(KeyCode.Escape))
					ChangeMenuState(MenuState.pause);
				// To gameOver
				if(gameObject.GetComponent<QuestManager>().currentQuest != null)
					ChangeMenuState(MenuState.gameOver);
				break;
			case MenuState.pause:
				// Back to game
				if(Input.GetKeyDown(KeyCode.Escape))
					ChangeMenuState(MenuState.game);
				break;
			case MenuState.gameOver:
				break;
		}
	}

	void ChangeMenuState(MenuState newMenuState)
	{
		currentMenu = newMenuState;
		switch(newMenuState)
		{
			case MenuState.main:
				SceneManager.LoadScene("MainMenu");
				break;
			case MenuState.controls:
				SceneManager.LoadScene("ControlsMenu");
				break;
			case MenuState.game:
				SceneManager.LoadScene("Game");
				break;
			case MenuState.pause:
				SceneManager.LoadScene("PauseMenu");
				break;
			case MenuState.gameOver:
				SceneManager.LoadScene("EndMenu");
				break;
		}
	}

	/// <summary>
	/// Finds any nearby NPCS, based on a given distance
	/// </summary>
	/// <param name="nearbyDist">The distance that an NPC is 
	/// considered nearby if the player moves within in</param>
	void FindNearbyNPC(float nearbyDist)
	{
		adjacentNPC = null;
		foreach(KeyValuePair<string, GameObject> npc in npcs) {
			if(Vector3.Distance(player.transform.position, npc.Value.transform.position) < nearbyDist) {
				// If there already is an adjacent NPC, the closest one is the adjacent NPC
				if(adjacentNPC != null) {
					if(Vector3.Distance(player.transform.position, npc.Value.transform.position) 
						< Vector3.Distance(player.transform.position, adjacentNPC.transform.position))
						adjacentNPC = npc.Value;
				}
				else
					adjacentNPC = npc.Value;

				// Has the adjacent NPC look at the player
				Vector3 lookAtPos = new Vector3(
					player.transform.position.x,
					player.GetComponent<BoxCollider>().size.y / 2,
					player.transform.position.z);

				npc.Value.transform.LookAt(lookAtPos);
			}
		}
	}

	/// <summary>
	/// Finds a random NPC from the dictionary
	/// </summary>
	/// <returns>A random NPC GameObject</returns>
	public GameObject GetRandomNPC()
	{
		List<string> keys = new List<string>(npcs.Keys);
		string key = keys[Random.Range(0, npcs.Keys.Count)];
		return npcs[key];
	}
}
