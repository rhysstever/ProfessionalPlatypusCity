using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Canvas mainCanvas;
    public GameObject mainMenuUIParent;
    public GameObject gameUIParent;
    public Text scoreText;
    public GameObject controlsPanel;
    public GameObject pausePanel;
    public Text questNameText;
    public Toggle questStep1;
    public Toggle questStep2;
    public GameObject endPanel;
    public Text gameWonText;

    // Start is called before the first frame update
    void Start()
    {
        ClearToggles();

        // Button onClicks setup
        // Menu Buttons: Play, Controls, and Quit
        mainMenuUIParent.transform.Find("playButton").gameObject.GetComponent<Button>().onClick.AddListener(delegate { 
            gameObject.GetComponent<GameManager>().ChangeMenuState(MenuState.game); });
        mainMenuUIParent.transform.Find("controlsButton").gameObject.GetComponent<Button>().onClick.AddListener(delegate { 
            gameObject.GetComponent<GameManager>().ChangeMenuState(MenuState.controls); });
        mainMenuUIParent.transform.Find("quitButton").gameObject.GetComponent<Button>().onClick.AddListener(Application.Quit);

        // Controls Button: Back (to either main menu or pause, wherever the user got to the controls menu from)
        controlsPanel.transform.Find("backButton").gameObject.GetComponent<Button>().onClick.AddListener(ControlsBackButton);

        // Pause Buttons: Resume, Controls, and Main Menu
        pausePanel.transform.Find("resumeButton").gameObject.GetComponent<Button>().onClick.AddListener(delegate { 
            gameObject.GetComponent<GameManager>().ChangeMenuState(MenuState.game); } );
        pausePanel.transform.Find("controlsButton").gameObject.GetComponent<Button>().onClick.AddListener(delegate { 
            gameObject.GetComponent<GameManager>().ChangeMenuState(MenuState.controls); });
        pausePanel.transform.Find("backButton").gameObject.GetComponent<Button>().onClick.AddListener(delegate { 
            gameObject.GetComponent<GameManager>().ChangeMenuState(MenuState.main); });

        // Game End Buton: Main Menu
        endPanel.transform.Find("backButton").gameObject.GetComponent<Button>().onClick.AddListener(delegate {
            gameObject.GetComponent<GameManager>().ChangeMenuState(MenuState.main); });
    }

    // Update is called once per frame
    void Update()
    {
        if(gameObject.GetComponent<GameManager>().currentMenuState == MenuState.game)
            UpdateGameUI();
    }

    public void ChangeMenuUI(MenuState currentMenuState)
    {
        DeactivateAllUI();
        switch(currentMenuState) {
            case MenuState.main:
                mainMenuUIParent.SetActive(true);
                break;
            case MenuState.controls:
                controlsPanel.SetActive(true);
                break;
            case MenuState.game:
                gameUIParent.SetActive(true);
                break;
            case MenuState.pause:
                pausePanel.SetActive(true);
                break;
            case MenuState.gameOver:
                gameWonText.text = "GAME WON!\n" + scoreText.text;
                endPanel.SetActive(true);
                break;
        }
    }

    public void ControlsBackButton()
	{
        if(gameObject.GetComponent<GameManager>().isFromMainMenu)
            gameObject.GetComponent<GameManager>().ChangeMenuState(MenuState.main);
        else
            gameObject.GetComponent<GameManager>().ChangeMenuState(MenuState.pause);
    }

    /// <summary>
    /// Updates the UI during the Game MenuState, such as score text
    /// </summary>
    void UpdateGameUI()
	{
        // Score
        scoreText.text = "Score: " + gameObject.GetComponent<QuestManager>().score;
        
        // Quest
        if(gameObject.GetComponent<QuestManager>().currentQuest != null) {
            Quest currentQuest = gameObject.GetComponent<QuestManager>().currentQuest;
            questNameText.text = "Current Quest: " + currentQuest.Name;
            questStep1.transform.Find("questStep1Text").gameObject.GetComponent<Text>().text = "Talk to " + currentQuest.StartingNPC.GetComponent<NPC>().npcName;
            questStep2.transform.Find("questStep2Text").gameObject.GetComponent<Text>().text = "Talk to " + currentQuest.EndingNPC.GetComponent<NPC>().npcName;
        }
	}

    /// <summary>
    /// Sets both toggles to false
    /// </summary>
    public void ClearToggles()
	{
        questStep1.isOn = false;
        questStep2.isOn = false;
    }

    /// <summary>
    /// Loops through the canvas and sets all child gameObjects to false
    /// </summary>
    public void DeactivateAllUI()
    {
        for(int i = 0; i < mainCanvas.transform.childCount; i++) {
            mainCanvas.transform.GetChild(i).gameObject.SetActive(false);
        }
    }
}
