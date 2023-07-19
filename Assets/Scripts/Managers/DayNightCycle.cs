using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.Tilemaps;
using static UnityEngine.PlayerLoop.PreLateUpdate;

public class DayNightCycle : MonoBehaviour
{
    public Farming farm;
    public Gradient _gradient;
    public Light2D _light;


    public float hour;
    public float minute = 10f; 
    public string day;
    public string season;
    public enum Day
    {
        Sunday,
        Monday,
        Tuesday,
        Wednesday,
        Thursday,
        Friday,
        Saturday
    }
    public enum Season
    {
        Spring,
        Summer,
        Autumn,
        Winter
    }
    public void Awake()
    {
        
    }
    // Start is called before the first frame update
    void Start()
    {
        day = Day.Sunday.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if (minute > 0)
        {
            minute = minute - Time.deltaTime;
            _light.color = _gradient.Evaluate(minute/10);
        }
        else
        {
            hour += 1;
            minute = 10f;
            var currentDay = (Day)hour;
            day = currentDay.ToString();
            UpdateCrops();
        }
    }

    void UpdateCrops()
    {
        DrySoil();
        foreach (KeyValuePair<Vector3Int, Crop> crop in farm.plantedCropDict)
        {
            if (crop.Value.cropIsWatered == true && crop.Value.cropCurrentGrowthStage < crop.Value.cropMaxGrowthStage)
            {
                crop.Value.cropIsWatered = false;
                crop.Value.cropCurrentGrowthStage += 1;
                crop.Value.ChangeSprite(crop.Value.growthStages[crop.Value.cropCurrentGrowthStage]);
            }
        }
    }


    void DrySoil()
    {
        for (int x = farm.farmMap.cellBounds.xMin; x < farm.farmMap.cellBounds.xMax; x++)
        {
            for (int y = farm.farmMap.cellBounds.yMin; y < farm.farmMap.cellBounds.yMax; y++)
            {
                for (int z = farm.farmMap.cellBounds.zMin; z < farm.farmMap.cellBounds.zMax; z++)
                {
                    Vector3Int tilePosition = new Vector3Int(x, y, z);
                    if (farm.farmMap.GetTile(tilePosition) == farm.wateredTile)
                    {
                        farm.farmMap.SetTile(tilePosition, farm.soilTile);
                    }
                }
            }
        }

    }
}
