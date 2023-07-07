using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarmManager : MonoBehaviour
{
    public Crop[] crops;
    private Dictionary<Crop.CropType, Crop> cropsDict = new();

    public void Awake()
    {
        int i = -1;
        foreach (Crop crop in crops)
        {
            i++;
            AddCrop(crop);
        }
    }
    private void AddCrop(Crop crop)
    {
        if (!cropsDict.ContainsKey(crop.cropType))
        {
            cropsDict.Add(crop.cropType, crop);
        }
    }
    public Crop GetCropByType(Crop.CropType type)
    {
        if (cropsDict.ContainsKey(type))
        {
            return cropsDict[type];
        }
        return null;
    }
}
