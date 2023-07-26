using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldManager : MonoBehaviour
{
    public NPCRoutine npc;
    public bool morning = false;
    public bool evening = false;
    void Start()
    {
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
        else if(DayNightCycle.gameTimer > 34)
        {
            if (evening == false)
            {
                Instantiate(npc, (Vector2)GameObject.Find("CauldronShop").transform.position, Quaternion.identity);
                evening = true;
            }
        }
    }
}
