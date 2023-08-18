using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.U2D;
using System.Linq;

public class NPC : MonoBehaviour
{
    public GameManager gameManager;
    public Vector2 currentPosition;
    public TextAsset routineData;
    public List<Routine> routines;
    public Location location;
    public Location destination;
    public UpdatedPathFinding pathFinding;
    public bool onWay;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        location = gameManager.gameLocations[0];
        pathFinding = gameObject.GetComponent<UpdatedPathFinding>();
        routines = Routine.FromJson(routineData.text, "winter");  
    }

    private int nextUpdate = 1;
    // Update is called once per frame
    private void Update()
    {
        //grid.GetTileAtPosition(grid.TilePosition(new Vector2(transform.localPosition.x - 0.5f, transform.localPosition.y)));
        //Debug.Log(currentPosition.y);
        if (SceneManager.GetActiveScene().name == location.locationName)
        {
            gameObject.GetComponent<SpriteRenderer>().enabled = true;
            transform.position = currentPosition;
        }
        else
        {
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
        }

        // If the next update is reached
        if (Time.time >= nextUpdate && onWay == false)
        {
            Debug.Log(Time.time + ">=" + nextUpdate);
            // Change the next update (current second+1)
            nextUpdate = Mathf.FloorToInt(Time.time) + 1;
            // Call your fonction
            CheckRoutine(nextUpdate-1);
        }

    }

    public void CheckRoutine(int time)
    {
        switch (DayNightCycle.season)
        {
            case "winter":
                foreach (var routine in routines)
                {
                    if (time == routine.startTime)
                    {
                        onWay = true;
                        destination = gameManager.gameLocations.First(Location => Location.locationName == routine.destination);
                        StartPath(gameManager, routine.target);
                    }
                }
                break;
        }
    }

    public void StartPath(GameManager gameManager, Vector2 targetTile)
    {
        //Debug.Log(gameManager.sceneGrids.Count);
        pathFinding = gameObject.GetComponent<UpdatedPathFinding>();
        pathFinding.gameManager = gameManager;
        pathFinding.npc = this;
        Tile start = gameManager.sceneGrids[location.locationName].tiles[currentPosition];
        Tile target = gameManager.sceneGrids[destination.locationName].tiles[targetTile];
        pathFinding.currentLocation = location;
        pathFinding.grid = gameManager.sceneGrids[location.locationName];
        pathFinding.currentPos = currentPosition;
        pathFinding.finalDestination = gameManager.sceneGrids[destination.locationName].tiles[targetTile];
        pathFinding.FindPath(start, target);
        pathFinding.StartCoroutine("FollowPath", 0);
        //SceneManager.LoadScene(location.locationName, LoadSceneMode.Additive);
        StartCoroutine(ActivateScene(location.locationName));
    }
    IEnumerator ActivateScene(string scene)
    {
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(location.locationName, LoadSceneMode.Additive);
        yield return null;
        SceneManager.SetActiveScene(SceneManager.GetSceneByName(scene));
    }
}
