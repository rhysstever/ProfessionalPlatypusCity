using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Canvas mainCanvas;
    public Text scoreText;
    public Text questInfoText;
    public Text gameWonText;

    // Start is called before the first frame update
    void Start()
    {
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
            questInfoText.text = "Current Quest: " + gameObject.GetComponent<QuestManager>().currentQuest.Name 
                + "\n\n" + gameObject.GetComponent<QuestManager>().currentQuest.Description;
        }
        // Display gameWon text when all quests are completed
        else {
            scoreText.gameObject.SetActive(false);
            questInfoText.gameObject.SetActive(false);
            gameWonText.text = "GAME WON!\n" + scoreText.text;
            gameWonText.gameObject.SetActive(true);
        }
    }
}
