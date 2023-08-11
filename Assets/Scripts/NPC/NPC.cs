using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.U2D;

public class NPC : MonoBehaviour
{
    public GameManager gameManager;
    public Vector2 currentPosition;
    public Location location;
    public Location destination;
    public UpdatedPathFinding pathFinding;
    // Start is called before the first frame update
    void Start()
    {
        pathFinding = gameObject.GetComponent<UpdatedPathFinding>();
    }

    // Update is called once per frame
    void Update()
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
        //Debug.Log(location);
    }


    public void StartPath(GameManager gameManager)
    {
        pathFinding = gameObject.GetComponent<UpdatedPathFinding>();
        pathFinding.gameManager = gameManager;
        pathFinding.npc = this;
        Tile start = gameManager.sceneGrids[location.locationName].tiles[currentPosition];
        Tile target = gameManager.sceneGrids[destination.locationName].tiles[new Vector2(5, 0)];
        pathFinding.currentLocation = location;
        pathFinding.grid = gameManager.sceneGrids[location.locationName];
        pathFinding.currentPos = currentPosition;
        pathFinding.finalDestination = gameManager.sceneGrids[destination.locationName].tiles[new Vector2(5, 0)];
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
