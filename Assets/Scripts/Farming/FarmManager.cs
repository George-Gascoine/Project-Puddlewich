using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Item;
using static ItemManager;

public class FarmManager : MonoBehaviour
{
    public static FarmManager instance { get; private set; }
    public TextAsset cropData;
    [System.Serializable]
    public class CropList
    {
        public List<Crop.CropData> crop;
    }

    public CropList cropList;
    public void Awake()
    {
        ReadJSON();
    }

    public void ReadJSON()
    {
        cropList = JsonUtility.FromJson<CropList>(cropData.text);
    }
}
