using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.Rendering;

[System.Serializable]
public class Quest
{
    //public string type;
    public string title;
    public string description;
    public MethodInfo solution;
    public bool complete;
    public int reward;
    public string nextquest;
}
