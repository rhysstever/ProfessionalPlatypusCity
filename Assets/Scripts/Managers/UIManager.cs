using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Canvas mainCanvas;
    public Text scoreText;
    public Text questNameText;
    public Toggle questStep1;
    public Toggle questStep2;
    Text questStep1Text;
    Text questStep2Text;
    public Text gameWonText;

    // Start is called before the first frame update
    void Start()
    {
        questStep1Text = questStep1.transform.Find("questStep1Text").gameObject.GetComponent<Text>();
        questStep2Text = questStep2.transform.Find("questStep2Text").gameObject.GetComponent<Text>();

        ClearToggles();

        // Hide gameWon text
        gameWonText.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        // Score
        scoreText.text = "Score: " + gameObject.GetComponent<QuestManager>().score;

        // Current Quest
        if(gameObject.GetComponent<QuestManager>().currentQuest != null) {
            questNameText.text = "Current Quest: " + gameObject.GetComponent<QuestManager>().currentQuest.Name;
            questStep1Text.text = "Talk to " + gameObject.GetComponent<QuestManager>().currentQuest.StartingNPC.GetComponent<NPC>().npcName;
            questStep2Text.text = "Talk to " + gameObject.GetComponent<QuestManager>().currentQuest.EndingNPC.GetComponent<NPC>().npcName;
        }
        // Display gameWon text when all quests are completed
        else {
            scoreText.gameObject.SetActive(false);
            questNameText.gameObject.SetActive(false);
            questStep1.gameObject.SetActive(false);
            questStep2.gameObject.SetActive(false);
            gameWonText.text = "GAME WON!\n" + scoreText.text;
            gameWonText.gameObject.SetActive(true);
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
}
