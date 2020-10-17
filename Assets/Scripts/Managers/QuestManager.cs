using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
	public int score;
	public List<Quest> quests;
	public Quest currentQuest;
	
	// Start is called before the first frame update
    void Start()
    {
		score = 0;
		quests = new List<Quest>();

		// Add quests
		quests.Add(new Quest(
			"Crucifiction",
			gameObject.GetComponent<GameManager>().npcs["Tori"],
			gameObject.GetComponent<GameManager>().npcs["Chase"],
			100));

		// Set current Quest to the first quest in the list
		currentQuest = quests[0];
    }

    // Update is called once per frame
    void Update()
    {
		if(Input.GetKeyDown(KeyCode.E))
			TalkToNPC();
	}

	/// <summary>
	/// Checks if the player has progressed the current quest by talking to an NPC
	/// </summary>
	void TalkToNPC()
	{
		if(RightNPC())
		{
			if(currentQuest.Started)
			{
				// The quest is completed
				currentQuest.QuestCompleted();
				score += currentQuest.Points;
				NextQuest();
			}
			else
			{
				// The quest is started
				currentQuest.QuestStated();
				score += currentQuest.Points / 2;
			}
		}
	}

	/// <summary>
	/// Checks if the adjacent NPC is the correct NPC for the current quest
	/// </summary>
	/// <returns></returns>
	bool RightNPC()
	{
		GameObject adjNPC = gameObject.GetComponent<GameManager>().adjacentNPC;

		// Ensures there is a nearby NPC
		if(adjNPC == null)
			return false;

		// The NPC is the correct NPC if they give the quest and the quest hasn't 
		// yet started OR they return the quest and the quest has not been completed yet
		if((currentQuest.Started && adjNPC.Equals(currentQuest.EndingNPC))
			|| (!currentQuest.Started && adjNPC.Equals(currentQuest.StartingNPC)))
				return true;

		return false;
	}

	void NextQuest()
	{
		int currentQuestIndex = quests.IndexOf(currentQuest);
		if(currentQuestIndex != quests.Count - 1)
			currentQuest = quests[currentQuestIndex++];
		else
			Debug.Log("Game Won");
	}
}
