using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Canvas inventory, quest, questLog;

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

    public LoadScene sceneLoader;
    public List<Location> gameLocations;
    public List<Collider2D> sceneColliders;
    public List<Transition> sceneTransitions;
    public Dictionary<string, UpdatedGrid2D> sceneGrids = new();
    public NPC npc;
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
        player.gameObject.SetActive(true);
        npc.gameObject.SetActive(true);
        inventory.gameObject.SetActive(true);
        quest.gameObject.SetActive(true);
        questLog.gameObject.SetActive(true);   
        player.speed = 6;
        Screen.SetResolution(1920, 1080, true);
        DontDestroyOnLoad(player);
        player.SetPlayer();
        npc.ReadJSON();
    }

}
