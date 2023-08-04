using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestGiver : MonoBehaviour
{
    public Quest quest;
    public Player player;
    public QuestLog log;
    public GameObject questWindow;
    public TextMeshProUGUI titleText;
    public TextMeshProUGUI descriptionText;
    public TextMeshProUGUI expText;
    public TextMeshProUGUI penniesText;

    public void Awake()
    {
        player = FindObjectOfType<Player>();
    }
    public void OpenQuestWindow()
    {
        questWindow.SetActive(true);
    }

    public void OnMouseDown()
    {
        OpenQuestWindow();
    }

    public void AcceptQuest()
    {
        questWindow.SetActive(false);
        log.questLog.Add(quest);
    }
}
