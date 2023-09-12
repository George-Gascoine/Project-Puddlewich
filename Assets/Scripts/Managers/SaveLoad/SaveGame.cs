using System;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SaveGame : MonoBehaviour 
{
    public SavePlayer player;
    public string saveJSON;
    public GameObject pausePanel;
    
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

    public class SaveClass
    {
        public SavePlayer savePlayer;
        public int saveTime;
    }
    public void Save()
    {
        SaveClass saveClass = new();
        saveClass.savePlayer = new SavePlayer()
        {
            playerName = GameManager.instance.player.playerName,
            pennies = GameManager.instance.player.pennies,
            inventory = GameManager.instance.player.inventory,
            bodyParts = GameManager.instance.player.bodyParts,
            position = GameManager.instance.player.transform.position,
            saveTime = DateTime.Now.ToString(),
        };
        saveClass.saveTime = GameManager.instance.GetComponent<DayNightCycle>().gameTimer;
        string json = JsonUtility.ToJson(saveClass);
        Debug.Log(json);
        saveJSON = json;
        
        string test = $@"C:\GitHub\Potioneer\Puddlewich\Project Potioneer\Project-Puddlewich\Saves/{saveClass.savePlayer.playerName}_{GameManager.instance.seed}.txt";
        //if (File.Exists("C:\\GitHub\\Potioneer\\Puddlewich\\Project Potioneer\\Project-Puddlewich\\Saves" + "/save.txt"))
        //{
            File.WriteAllText(test, json);
       // }
        //plantedCrops = GameManager.instance.player.GetComponent<Farming>().plantedCrops;
        //wateredTiles = GameManager.instance.player.GetComponent<Farming>().wateredTiles;
        //soilTiles = GameManager.instance.player.GetComponent<Farming>().soilTiles;
        //plantedDays = GameManager.instance.player.GetComponent<Farming>().plantedDays;
        //plantedObjects = GameManager.instance.player.GetComponent<Farming>().plantedObjects;
    }
}

