using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class QuestManager : MonoBehaviour
{
	public int score;
	public List<Quest> quests;
	public Quest currentQuest;
	public int numOfQuests;
	
	// Start is called before the first frame update
    void Start()
    {
		score = 0;
		quests = new List<Quest>();

		// Add quests
		CreateRandomQuests(numOfQuests);
		
		// Adds Crucificition
		quests.Add(new Quest(
			"Crucifiction",
			"Make the ultimate sacrifice of the GDD Jesus, but get supplies from Tori first",
			gameObject.GetComponent<GameManager>().npcs["Tori"],
			gameObject.GetComponent<GameManager>().npcs["Chase"],
			100));

		// Set current Quest to the first quest in the list
		currentQuest = quests[0];
		currentQuest.NextNPC.transform.Find("questMarker").gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
		if(Input.GetKeyDown(KeyCode.E))
			TalkToNPC();

		if(currentQuest != null
			&& currentQuest.Name == "Crucifiction")
			MoveChase();
	}

	/// <summary>
	/// Checks if the player has progressed the current quest by talking to an NPC
	/// </summary>
	void TalkToNPC()
	{
		if(RightNPC()) {
			// Deactivate the (now) old quest marker
			currentQuest.NextNPC.transform.Find("questMarker").gameObject.SetActive(false);
			if(currentQuest.Started) {
				// The quest is completed
				currentQuest.QuestCompleted();
				score += currentQuest.Points;
				NextQuest();
			}
			else
				currentQuest.QuestStarted();
			// Set the quest marker active in the scene
			if(currentQuest != null)
				currentQuest.NextNPC.transform.Find("questMarker").gameObject.SetActive(true);
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

		// The NPC is the correct NPC if they are the 
		// next NPC the player has to go to
		if(adjNPC.Equals(currentQuest.NextNPC))
			return true;

		return false;
	}

	/// <summary>
	/// Increments the game to the next quest, if there 
	/// are no more quests, then the game is won
	/// </summary>
	void NextQuest()
	{
		int currentQuestIndex = quests.IndexOf(currentQuest);
		if(currentQuestIndex != quests.Count - 1)
			currentQuest = quests[++currentQuestIndex];
		else {
			currentQuest = null;
		}
	}

	/// <summary>
	/// Creates a number of quests and adds them to the quests list
	/// </summary>
	/// <param name="numOfQuests">The number of quests being created</param>
	void CreateRandomQuests(int numOfQuests)
	{
		for(int i = 0; i < numOfQuests; i++) {
			// Finds 2 random NPCs
			GameObject npc1 = gameObject.GetComponent<GameManager>().GetRandomNPC();
			GameObject npc2 = gameObject.GetComponent<GameManager>().GetRandomNPC();
			// If the 2 found NPCs are the same, it randomizes the second NPC again
			while(npc2.Equals(npc1)) {
				npc2 = gameObject.GetComponent<GameManager>().GetRandomNPC();
			}
			int questNum = quests.Count + 1;
			// Creates a new quest and adds it to the list of quests
			quests.Add(new Quest(
				"Quest " + questNum,
				npc1,
				npc2,
				(int)Random.Range(5, 11) * 10));
		}
	}

	void MoveChase()
	{
		GameObject chase = gameObject.GetComponent<GameManager>().npcs["Chase"];
		GameObject cross = GameObject.Find("cross");
		Vector3 crossPos = new Vector3(
			cross.transform.position.x + 1.0f, 
			cross.transform.position.y + 2.0f, 
			cross.transform.position.z);
		float crossYRot = 180.0f;

		chase.transform.rotation = new Quaternion();
		chase.transform.position = crossPos;
		chase.transform.Rotate(0, crossYRot, 0);
	}
}
