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

    }

    // Update is called once per frame
    void Update()
    {

    }
}
