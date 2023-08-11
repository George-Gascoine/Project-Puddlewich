using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WorldManager : MonoBehaviour
{
    public NPCRoutine npc;
    public bool morning = false;
    public bool evening = false;
    public Dictionary<Range, NPCRoutine> NPCs = new(); 
    void Start()
    {
        NPCs.Add(new Range(0,17), npc);
        foreach(KeyValuePair<Range, NPCRoutine> time in NPCs)
        {
            Range range= time.Key;
            if(DayNightCycle.gameTimer < time.Key.End.Value) 
            {
                //Instantiate(npc, new Vector3(-2.34f, 4.19f, 0), Quaternion.identity);
                morning = true;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (DayNightCycle.gameTimer < 17)
        {
            if (morning == false)
            {
                Instantiate(npc, new Vector3(-2.34f, 4.19f, 0), Quaternion.identity);
                morning = true;
            }
        }
        else if(DayNightCycle.gameTimer > 37)
        {
            if (evening == false)
            {
                Instantiate(npc, (Vector2)GameObject.Find("CauldronShop").transform.position, Quaternion.identity);
                evening = true;
            }
        }
    }
}
