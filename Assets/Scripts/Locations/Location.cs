using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Animations;
using UnityEngine;
using static UnityEditor.FilePathAttribute;

public class Location : MonoBehaviour
{
    public string locationName;
    public List<Transition> transitions;
    [SerializeField] public List<Transition.Warp> warps;
    public List<Location> connectedLocations;
    [SerializeField] public List<Collider2D> colliders;
    public Grid2D locationGrid;

    // Start is called before the first frame update
    void Start()
    {

    }


    //Get manager component from each scene and copy transition list over to gameManager location list

    //public void GenerateTransitions()
    //{
    //    foreach (Location loc in connectedLocations)
    //    {
    //        Transition newTran = new()
    //        {
    //            location = this,
    //            destination = loc,
    //            destinationSpawn = new Vector2(2, 2),
    //            locationTile = new Vector2(20, 20)
    //        };
    //        //Transition newTran = loc.AddComponent<Transition>();
    //        //newTran.location = this;
    //        //newTran.destination = loc;
    //        //newTran.destinationSpawn = new Vector2(2, 2);
    //        //newTran.locationTile = new Vector2(20, 20);
    //        transitions.Add(newTran);
    //    }
    //}
}