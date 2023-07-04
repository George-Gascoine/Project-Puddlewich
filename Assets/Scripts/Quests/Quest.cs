using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

[System.Serializable]
public class Quest
{
    public bool isActive;

    public Player player;
    public string title;
    public string description;
    public string prerequisites;
    public int pennieReward;

    public void Complete()
    {
        player.pennies += pennieReward;
        isActive = false;
    }
}
