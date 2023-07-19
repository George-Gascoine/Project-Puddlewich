using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;

public class GameManager : MonoBehaviour
{
    public Player player;
    public static GameManager instance;
    public ItemManager itemManager;
    public FarmManager farmManager;
    public Grid farmGrid;
    public Dictionary<Vector2, Tile> farmTiles = new();
    public Grid worldGrid;

    public void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        Instantiate(player, new Vector3(0, 0, 0), Quaternion.identity);
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

        DontDestroyOnLoad(this.gameObject);
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

}
