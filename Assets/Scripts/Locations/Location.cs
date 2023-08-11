using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;
using static UnityEditor.FilePathAttribute;

public class Location : MonoBehaviour
{
    public string locationName;
    public List<Transition> transitions;
    public List<Location> connectedLocations;
    [SerializeField] public List<BoxCollider2D> colliders;
    public Grid2D locationGrid;

    // Start is called before the first frame update
    void Start()
    {

    }

    public void GenerateTransitions()
    {
        foreach (Location loc in connectedLocations)
        {
            Transition newTran = new()
            {
                location = this,
                destination = loc,
                destinationSpawn = new Vector2(2, 2),
                locationTile = new Vector2(20, 20)
            };
            transitions.Add(newTran);
        }
    }
}