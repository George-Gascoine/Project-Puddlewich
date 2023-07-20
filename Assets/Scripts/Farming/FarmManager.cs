using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarmManager : MonoBehaviour
{
    // Generate Grid of Farmland
    // Make initial tiles grass -> soil -> water
    // When plant is planted set tile value to 1
    // Reset watered status at days conclusion + increment plant growth stage until max
    // When plant is harvested set tile value to 0 



    //public Crop[] crops;
    //public Collectable[] seeds;
    //private Dictionary<Crop.CropType, GameObject> cropsDict = new();
    //public Dictionary<Collectable.ItemType, GameObject> seedCropDict = new();

    //public void Awake()
    //{
    //    int i = -1;
    //    foreach (Crop crop in crops)
    //    {
    //        i++;
    //        AddCrop(crop);
    //    }
    //    i = 0;
    //    foreach (Collectable seed in seeds)
    //    {
    //        i++;
    //        AddSeed(seed);
    //    }
    //}
    //private void AddCrop(Crop crop)
    //{
    //    if (!cropsDict.ContainsKey(crop.cropType))
    //    {
    //        cropsDict.Add(crop.cropType, crop);
    //    }
    //}

    //public void AddSeed(Collectable seed)
    //{
    //    if(!seedCropDict.ContainsKey(seed.type))
    //    {
    //        seedCropDict.Add(seed.type, seed.crop);
    //        //Debug.Log(seed.crop);
    //    }
    //}
    //public Crop GetCropByType(Crop.CropType type)
    //{
    //    if (cropsDict.ContainsKey(type))
    //    {
    //        return cropsDict[type];
    //    }
    //    return null;
    //}
}
