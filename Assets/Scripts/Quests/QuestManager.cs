using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json.Linq;

using System.IO;

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

    public QuestList basicQuestList;
    public QuestList gatherQuestList;
    public void ReadJSON()
    {
        JObject jObject = JObject.Parse(questData.text);
        JArray jArray = (JArray)jObject["quest"];
        int count = jArray.Count;
        for(int i = 0; i < count; i++)
        {
            JToken jToken = jObject["quest"][i]["type"];
            if(jToken.ToString() == "Basic")
            {

                Quest quest = JsonUtility.FromJson<Quest>(jObject["quest"][i].ToString());
                basicQuestList.quest.Add(quest);
            }
            else if (jToken.ToString() == "Gathering")
            {
                Quest quest = JsonUtility.FromJson<Quest>(jObject["quest"][i].ToString());
                gatherQuestList.quest.Add(quest);
            }
        }

        //questList = JsonUtility.FromJson<QuestList>(questData.text);
    }
}
