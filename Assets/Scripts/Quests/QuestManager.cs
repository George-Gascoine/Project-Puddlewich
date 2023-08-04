using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using static PotionCraftManager;

public class QuestManager : MonoBehaviour
{
    public TextAsset questData;
    public Player player;
    private void Start()
    {
        player = FindObjectOfType<Player>();
        ReadJSON();
    }
    [System.Serializable]
    public class QuestList
    {
        public List<Quest> quest;
    }

    public QuestList questList;
    public void ReadJSON()
    {
        questList = JsonUtility.FromJson<QuestList>(questData.text);
    }
}
