using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;
using static UnityEngine.PlayerLoop.PreLateUpdate;

public class DayNightCycle : MonoBehaviour
{
    public Farming farm;
    public Gradient _gradient;
    public Light2D _light;


    public static float gameTimer;
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
        //gameTimer = 0f;
    }
    // Start is called before the first frame update
    void Start()
    {
        gameTimer = 0f;
        day = Day.Sunday.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        gameTimer += Time.deltaTime;
        //Debug.Log(gameTimer);
        if (minute > 0)
        {
            minute = minute - Time.deltaTime;
            if (SceneManager.GetActiveScene().name == "World")
            {
                _light.color = _gradient.Evaluate(minute / 10);
            }
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
        foreach(Crop crop in farm.plantedCrops)
        {
            Destroy(crop.gameObject);
        }
        farm.plantedCrops.Clear();
        foreach (CropData crop in farm.cropData)
        { 
            Crop plantThis = Instantiate(crop.cropType, crop.cropCellCentre, Quaternion.identity);
            farm.plantedCrops.Add(plantThis);
            if (crop.cropIsWatered && crop.cropCurrentGrowthStage < crop.cropType.cropMaxGrowthStage)
            {
                crop.cropIsWatered = false;
                crop.cropCurrentGrowthStage++;
                plantThis.cropCurrentGrowthStage = crop.cropCurrentGrowthStage;
                plantThis.CheckSprite();
            }
            else
            {
                plantThis.cropCurrentGrowthStage = crop.cropCurrentGrowthStage;
                plantThis.CheckSprite();
            }
        }
    }


    void DrySoil()
    {
        foreach (Vector3Int soil in farm.wateredTiles)
        {
            farm.farmMap.SetTile(soil, farm.soilTile);
        }
        farm.wateredTiles.Clear();
    }
}
