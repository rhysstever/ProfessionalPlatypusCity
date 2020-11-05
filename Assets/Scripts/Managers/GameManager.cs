using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
	public MenuState currentMenuState;
	public bool isFromMainMenu;
	
	// Start is called before the first frame update
    void Start()
    {
		ChangeMenuState(MenuState.main);
    }

    // Update is called once per frame
    void Update()
    {
		switch(currentMenuState)
		{
			case MenuState.main:
				break;
			case MenuState.controls:
				break;
			case MenuState.game:
				// Game -> Pause: when ESC key is pressed
				if(Input.GetKeyDown(KeyCode.Escape))
					ChangeMenuState(MenuState.pause);
				// Game -> GameOver: when there are no more quests
				if(gameObject.GetComponent<QuestManager>().currentQuest == null)
					ChangeMenuState(MenuState.gameOver);
				break;
			case MenuState.pause:
				// Pause -> Game: When the ESC key is pressed
				if(Input.GetKeyDown(KeyCode.Escape))
					ChangeMenuState(MenuState.game);
				break;
			case MenuState.gameOver:
				break;
		}
	}

	/// <summary>
	/// Runs initial, one-time funtions when the menu state changes
	/// </summary>
	/// <param name="newMenuState">The new Menu State of the game</param>
	public void ChangeMenuState(MenuState newMenuState)
	{
		// Checks if the user is coming from the Main Menu, 
		// and updates the current MenuState
		isFromMainMenu = currentMenuState == MenuState.main;
		currentMenuState = newMenuState;
		
		// Changes UI based on the current menu
		gameObject.GetComponent<UIManager>().ChangeMenuUI(newMenuState);

		// Sets the player unable to move, unless the game is at certian states
		gameObject.GetComponent<NPCManager>().player.GetComponent<Movement>().canMove = false;

		switch(newMenuState)
		{
			case MenuState.main:
				break;
			case MenuState.controls:
				break;
			case MenuState.game:
				gameObject.GetComponent<NPCManager>().player.GetComponent<Movement>().canMove = true;
				break;
			case MenuState.pause:
				break;
			case MenuState.gameOver:
				gameObject.GetComponent<NPCManager>().player.GetComponent<Movement>().canMove = true;
				break;
		}
	}
}
