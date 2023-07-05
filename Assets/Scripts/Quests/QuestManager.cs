using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class QuestManager : MonoBehaviour
{
    public TextAsset textAssetData;
    public List<Quest> quests = new();
    Player player;
    private void Start()
    {
        player = FindObjectOfType<Player>();
        ReadCSV();
    }

    void ReadCSV()
    {
        string[] data = textAssetData.text.Split(new string[] { ",", "\n" }, StringSplitOptions.None);

        int tableSize = data.Length / 4 - 1;
        for (int i = 0; i < tableSize; i++)
        {
            Quest newQuest = new Quest();
            newQuest.player = player;
            newQuest.title = data[4 * (i + 1)];
            newQuest.description = data[4 * (i + 1) + 1];
            newQuest.pennieReward = int.Parse(data[4 * (i + 1) + 2]);
            newQuest.prerequisites = data[4 * (i + 1) + 3];
            quests.Add(newQuest);
        }
    }
}
