using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;
using static UnityEngine.Rendering.DebugUI;

public class GameManager : MonoBehaviour
{
    public Player player;
    public static GameManager instance;
    public ItemManager itemManager;
    public FarmManager farmManager;
    public Grid farmGrid;
    public Dictionary<Vector2, Tile> farmTiles = new();
    public Grid worldGrid;

    public LoadScene sceneLoader;
    public List<Location> gameLocations;
    public List<Collider2D> sceneColliders;
    public List<Transition> sceneTransitions;
    public Dictionary<string, UpdatedGrid2D> sceneGrids = new();
    public NPC npc;

    public void Start()
    {
        //DontDestroyOnLoad(this.gameObject);
        foreach (Location location in gameLocations)
        {
            location.warps.Clear();
            sceneLoader.SceneLoader(location.locationName, location);
        }

        StartCoroutine(NPCStart());

        DayNightCycle.gameTimer = 0;
        player = Instantiate(player, new Vector3(4, 0, 0), Quaternion.identity);
        player.speed = 60;
        Screen.SetResolution(1920, 1080, true);
        DontDestroyOnLoad(FindObjectOfType<Player>());
    }
    public void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }

        //DontDestroyOnLoad(this.gameObject);
        itemManager = GetComponent<ItemManager>();
        farmManager = GetComponent<FarmManager>();
    }

    private void Update()
    {
        
    }
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("OnSceneLoaded: " + scene.name);
        Debug.Log(mode);

    }
    public void GenerateGridWithCollisions(Location location)
    {

        //location.locationGrid.locationColliders = sceneColliders;
        //location.locationGrid.GenerateGrid();
        UpdatedGrid2D grid = new()
        {
            locationColliders = sceneColliders
        };
        grid.GenerateGrid();
        sceneGrids.Add(location.locationName, grid);
    }

    IEnumerator NPCStart()
    {
        yield return new WaitForSeconds(3);
        //npc.StartPath(this);
    }

}
