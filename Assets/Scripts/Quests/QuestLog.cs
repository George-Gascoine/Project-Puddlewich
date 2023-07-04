using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestLog : MonoBehaviour
{
    public Dictionary<string, Quest> questNames = new();
    public List<Quest> questLog = new();
    public GameObject questLogPanel;
    public Button questButton;
    public TextMeshProUGUI questName;
    public TextMeshProUGUI questDesc;
    public Player player;

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            questLogPanel.SetActive(true);
        }
        QuestProgress();
    }
    public void DisplayQuestLog()
    {
        for(int i = 0; i < questLog.Count; i++)
        {
            questName.text = questLog[i].title;
            Button newButton = Instantiate(questButton);
            newButton.transform.SetParent(questLogPanel.transform, false);
        }
    }
    void QuestProgress()
    {
        foreach (Quest i in questLog)
        {
            if (i.title == "Pick Up Your First Item" && i.isActive)
            {
                bool check = player.inventory.CheckItem(Collectable.ItemType.ITEM, 1);
                if(check)
                {
                    i.Complete();
                }
            }
        }
    }
}
