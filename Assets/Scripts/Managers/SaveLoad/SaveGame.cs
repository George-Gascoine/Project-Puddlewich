using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class SaveGame : MonoBehaviour 
{
    public SavePlayer player;
    public string saveJSON;
    public GameObject pausePanel;
    public UnityEngine.UI.Button loadButton;
    public List<GameObject> loadedGames;
    //public List<Crop.CropData> plantedCrops = new();
    //public List<Vector3Int> wateredTiles = new();
    //public List<Vector3Int> soilTiles = new();
    //public List<int> plantedDays = new();
    //public List<GameObject> plantedObjects = new();

    [System.Serializable]
    public class SavePlayer
    {
        public string playerName;
        public float pennies;
        public Inventory inventory;
        public Sprite[] bodyParts;
        public Vector3 position;
        public string saveTime;
    }
    public void Save()
    {
        player = new SavePlayer()
        {
            playerName = GameManager.instance.player.playerName,
            pennies = GameManager.instance.player.pennies,
            inventory = GameManager.instance.player.inventory,
            bodyParts = GameManager.instance.player.bodyParts,
            position = GameManager.instance.player.transform.position,
            saveTime = DateTime.Now.ToString(),
        };
        string json = JsonUtility.ToJson(player);
        Debug.Log(json);
        saveJSON = json;
        //if (File.Exists("C:\\GitHub\\Potioneer\\Puddlewich\\Project Potioneer\\Project-Puddlewich\\Saves" + "/save.txt"))
        //{
            File.WriteAllText("C:\\GitHub\\Potioneer\\Puddlewich\\Project Potioneer\\Project-Puddlewich\\Saves" + "/save.txt", json);
       // }
        //plantedCrops = GameManager.instance.player.GetComponent<Farming>().plantedCrops;
        //wateredTiles = GameManager.instance.player.GetComponent<Farming>().wateredTiles;
        //soilTiles = GameManager.instance.player.GetComponent<Farming>().soilTiles;
        //plantedDays = GameManager.instance.player.GetComponent<Farming>().plantedDays;
        //plantedObjects = GameManager.instance.player.GetComponent<Farming>().plantedObjects;
    }

    public void PopulateLoads()
    {
        SavePlayer playerLoad = new SavePlayer();
        DirectoryInfo dir = new DirectoryInfo("C:\\GitHub\\Potioneer\\Puddlewich\\Project Potioneer\\Project-Puddlewich\\Saves");
        FileInfo[] info = dir.GetFiles("save.txt");

        int i = 0;
        foreach (FileInfo f in info)
        {
            i++;
            Debug.Log(f);
            UnityEngine.UI.Button newButton = Instantiate(loadButton);
            newButton.transform.SetParent(pausePanel.transform, false);
            var ty = newButton.GetComponentInChildren<TextMeshProUGUI>();
            var pos = newButton.GetComponent<RectTransform>();
            pos.anchoredPosition = new Vector2(360, 80 - i * 40);
            string saveText = File.ReadAllText("C:\\GitHub\\Potioneer\\Puddlewich\\Project Potioneer\\Project-Puddlewich\\Saves" + "/save.txt");
            playerLoad = JsonUtility.FromJson<SavePlayer>(saveText);
            ty.text = playerLoad.playerName + " " + playerLoad.saveTime;
            loadedGames.Add(newButton.gameObject);
            newButton.onClick.AddListener(() => { Load(playerLoad); });
        }
    }

    public void Load(SavePlayer playerLoad)
    {
        //if (File.Exists(Application.dataPath + "/save.txt"))
        //{
        //    string saveText = File.ReadAllText(Application.dataPath + "/save.txt");
        //    playerLoad = JsonUtility.FromJson<SavePlayer>(saveText);
        //    GameManager.instance.player.transform.position = playerLoad.position;
        //}       
    }
}

