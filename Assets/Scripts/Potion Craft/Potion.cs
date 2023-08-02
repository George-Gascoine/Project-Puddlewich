using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Potion", fileName = "Potion")]
public class Potion : ScriptableObject
{
    public string potionName;
    public string potionEffect;
    public string potionDescription;
    public Sprite potionSprite;
    public float cost;
    public float quality;
}
