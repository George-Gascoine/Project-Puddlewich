using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using UnityEngine;
using UnityEngine.Rendering.Universal;
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
            foreach (KeyValuePair<Vector3Int, Crop> crop in farm.plantedCropDict)
            {
                if(crop.Value.cropIsWatered == true)
                {
                    crop.Value.cropIsWatered = false;
                    farm.farmMap.SetTile(crop.Key, farm.soilTile);
                }
            }
        }
    }
}
