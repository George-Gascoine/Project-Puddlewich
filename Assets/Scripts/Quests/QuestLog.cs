using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class QuestLog : MonoBehaviour
{
    public Dictionary<string, Quest> questNames = new();
    public List<Quest> questLog = new();
    public GameObject questLogPanel;
    public Button questButton;
    public TextMeshProUGUI questDesc;
    public Player player;
    public QuestManager questManager;
    public List<GameObject> questButtons;

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            DisplayQuestLog();
        }
        QuestProgress();
    }
    public void DisplayQuestLog()
    {
        questLog = questManager.activeQuests;
        if (!questLogPanel.activeSelf)
        {
            questLogPanel.SetActive(true);
            for (int i = 0; i < questLog.Count; i++)
            {
                Button newButton = Instantiate(questButton);
                newButton.transform.SetParent(questLogPanel.transform, false);
                var ty = newButton.GetComponentInChildren<TextMeshProUGUI>();
                var pos = newButton.GetComponent<RectTransform>();
                pos.anchoredPosition = new Vector2(-100, 80 - i * 40);
                ty.text = questLog[i].title;
                int logID = i;
                newButton.onClick.AddListener(() => { DisplayQuestDesc(logID); });
                questButtons.Add(newButton.gameObject);
            }
        }
        else
        {
            for (int i = 0; i < questButtons.Count; i++)
            {
                Destroy(questButtons[i]);
            }
            questDesc.text = "";
            questLogPanel.SetActive(false);
        }
    }

    public void DisplayQuestDesc(int logID)
    {
        questDesc.text = questLog[logID].description;
    }
    void QuestProgress()
    {
        
    }
}
