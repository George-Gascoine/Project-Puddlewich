using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveGame : MonoBehaviour
{
    public SaveGame.Player player;
    public List<Crop.CropData> farmData = new List<Crop.CropData>();
    [System.Serializable]
    public class Player
    {
        public string playerName;
        public float pennies;
        public Inventory inventory;
    }

    public void SavePlayer()
    {
    }
}
