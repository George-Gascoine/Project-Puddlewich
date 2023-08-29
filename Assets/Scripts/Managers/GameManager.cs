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
    public DayNightCycle dayNightCycle;
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
    public string playerName;
    public Sprite[] bodyParts;

    public void Start()
    {
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

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    private void Update()
    {
        
    }
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {

    }
    public void GameStart()
    {
        Debug.Log("Start");
        //DontDestroyOnLoad(this.gameObject);
        foreach (Location location in gameLocations)
        {
            location.warps.Clear();
            sceneLoader.SceneLoader(location.locationName, location);
        }
        StartCoroutine(dayNightCycle.GameTime());
        sceneLoader.LoadToScene("World", new Vector2(0,5));
        StartCoroutine(NPCStart());

        player.speed = 60;
        Screen.SetResolution(1920, 1080, true);
        DontDestroyOnLoad(player);
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
        yield return new WaitForSeconds(0.5f);
        player.SetPlayer();
    }

}
