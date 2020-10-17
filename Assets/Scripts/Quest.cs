using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest
{
	// Fields
	private string name;
	private string description;
	private GameObject startingNPC;
	private GameObject endingNPC;
	private int points;
	private bool isStarted;
	private bool isCompleted;

	// Properties
	public string Name { get { return name; } }
	public string Description { get { return description; } }
	public GameObject StartingNPC { get { return startingNPC; } }
	public GameObject EndingNPC { get { return endingNPC; } }
	public int Points { get { return points; } }
	public bool Started { get { return isStarted; } }
	public bool Completed { get { return isCompleted; } }

	// Constructors
	/// <summary>
	/// Creates a Quest object 
	/// </summary>
	/// <param name="name">The name of the quest</param>
	/// <param name="description">The details of the quest</param>
	/// <param name="startingNPC">The NPC that gives the quest</param>
	/// <param name="endingNPC">The NPC that completes the quest</param>
	/// <param name="points">The points awarded when this quest is completed</param>
	public Quest(string name, string description, GameObject startingNPC, GameObject endingNPC, int points)
	{
		this.name = name;
		this.description = description;
		this.startingNPC = startingNPC;
		this.endingNPC = endingNPC;
		this.points = points;
		isStarted = false;
		isCompleted = false;
	}
	/// <summary>
	/// Creates a Quest object
	/// </summary>
	/// <param name="name">The name of the quest</param>
	/// <param name="startingNPC">The NPC that gives the quest</param>
	/// <param name="endingNPC">The NPC that completes the quest</param>
	/// <param name="points">The points awarded when this quest is completed</param>
	public Quest(string name, GameObject startingNPC, GameObject endingNPC, int points)
	{
		this.name = name;
		this.startingNPC = startingNPC;
		this.endingNPC = endingNPC;
		description = "Talk to " + this.endingNPC;
		this.points = points;
		isStarted = false;
		isCompleted = false;
	}

	// Methods
	public void QuestStated()
	{
		isStarted = true;
		Debug.Log(name + " started!");
	}

	public void QuestCompleted()
	{
		isCompleted = true;
		Debug.Log(name + " completed!");
	}
}
