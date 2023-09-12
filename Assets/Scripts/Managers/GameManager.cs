using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public List<GameObject> menus;
    public int seed;
    public Player player;
    public DayNightCycle dayNightCycle;
    public static GameManager instance;
    public ItemManager itemManager;
    public FarmManager farmManager;
    public Grid farmGrid;
    public Dictionary<Vector2, Tile> farmTiles = new();
    public Grid worldGrid;
    public PlayerCamera cam;
    public GameObject baseItem;
    public bool load;

    public LoadScene sceneLoader;
    public List<Location> gameLocations;
    public List<Collider2D> sceneColliders;
    public List<Transition> sceneTransitions;
    public Dictionary<string, UpdatedGrid2D> sceneGrids = new();
    public NPC npc;
    public List <NPC> npcs = new();
    public string playerName;
    public Sprite[] bodyParts;
    private UnityEngine.SceneManagement.Scene scene;

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

        // Store the creating scene as the scene to trigger start
        scene = SceneManager.GetActiveScene();
    }

    public void GameStart()
    {
        var rand = new System.Random((int)DateTime.Now.Ticks);
        seed = rand.Next(100000, 1000000);
        //DontDestroyOnLoad(this.gameObject);
        foreach (Location location in gameLocations)
        {
            location.warps.Clear();
            sceneLoader.SceneLoader(location.locationName, location);
        }
        StartCoroutine(dayNightCycle.GameTime());
        sceneLoader.LoadToScene("World", new Vector2(0,5));
        
        StartCoroutine(NPCStart(load));

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

    IEnumerator NPCStart(bool load)
    {
        yield return new WaitForSeconds(0.5f);
        player.gameObject.SetActive(true);
        if(load == false)
        {
            player.inventory.CreateInventory(18);
        }
        foreach (GameObject menu in menus)
        {
            menu.SetActive(true);
        } 
        player.speed = 6;
        Screen.SetResolution(1920, 1080, true);
        player.SetPlayer();
        foreach (NPC npc in npcs)
        {
            npc.gameObject.SetActive(true);
            npc.ReadJSON();
        }
    }

}
