using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using UnityEngine;
using UnityEngine.Experimental.AI;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;
using static UnityEngine.PlayerLoop.PreLateUpdate;

public class DayNightCycle : MonoBehaviour
{
    //public Light2D _light;


    public int gameTimer;
    public float hour;
    public float minute; 
    public string day;
    public static string season = "winter";
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
        gameTimer = 0;
    }

    // Start is called before the first frame update
    private void Start()
    {
        gameTimer = 0;
        //StartCoroutine(GameTime());
        day = Day.Sunday.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        //if (active)
        //{
        //    gameTimer += Time.deltaTime;
        //    //Debug.Log(gameTimer);
        //    //Debug.Log(gameTimer);
        //    if (minute > 0)
        //    {
        //        minute = minute - Time.deltaTime;
        //        //if (SceneManager.GetActiveScene().name == "World")
        //        //{
        //        //    _light.color = _gradient.Evaluate(minute / 10);
        //        //}
        //    }
        //    else
        //    {
        //        hour += 1;
        //        minute = 10f;
        //        var currentDay = (Day)hour;
        //        day = currentDay.ToString();
        //        UpdateCrops();
        //    }
        //}
    }

    public IEnumerator GameTime()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            gameTimer++;
            if (gameTimer == 8)
            {
                gameTimer = 0;
                GameManager.instance.GetComponent<SaveGame>().Save();
            }
            Debug.Log(gameTimer);
            if(gameTimer % 8 == 0)
            {
                UpdateCrops();
            }
        }
    }

    void UpdateCrops()
    {
        GameObject.FindWithTag("Player").GetComponent<Farming>().wateredTiles.Clear();
        foreach (Crop.CropData crop in GameObject.FindWithTag("Player").GetComponent<Farming>().plantedCrops)
        {
            if (crop.currentGrowthStage < crop.maxGrowthStage && crop.cropIsWatered)
            {
                if (crop.growthStageDay == crop.growthStageDays[crop.currentGrowthStage])
                {
                    crop.currentGrowthStage++;
                    crop.growthStageDay = 1;
                    crop.cropIsWatered = false;
                }
                else
                {
                    crop.growthStageDay++;
                    crop.cropIsWatered = false;
                }
            }
        }
        if(SceneManager.GetActiveScene().name == "World")
        {
            GameObject.FindWithTag("Player").GetComponent<Farming>().DrySoil();
            GameObject.FindWithTag("Player").GetComponent<Farming>().ResetFarm();
        }
        
        //Player.instance.GetComponent<Farming>().plantedObjects.Clear();
        //foreach (CropData crop in farm.cropData)
        //{ 
        //    Crop plantThis = Instantiate(crop.cropType, crop.cropCellCentre, Quaternion.identity);
        //    farm.plantedCrops.Add(plantThis);
        //    if (crop.cropIsWatered && crop.cropCurrentGrowthStage < crop.cropType.cropMaxGrowthStage)
        //    {
        //        crop.cropIsWatered = false;
        //        crop.cropCurrentGrowthStage++;
        //        plantThis.cropCurrentGrowthStage = crop.cropCurrentGrowthStage;
        //        plantThis.CheckSprite();
        //    }
        //    else
        //    {
        //        plantThis.cropCurrentGrowthStage = crop.cropCurrentGrowthStage;
        //        plantThis.CheckSprite();
        //    }
        //}
    }



}
