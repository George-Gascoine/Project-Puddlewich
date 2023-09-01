using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;
using Newtonsoft.Json.Linq;
using static UnityEngine.Rendering.DebugUI;
using static DayNightCycle;

public class NPC : MonoBehaviour
{
    public TextAsset NPCData;
    public string npcName;
    public string age;
    public string manners;
    public string gender;
    public bool datable;
    public List<int> likes;
    public List<int> dislikes;
    public Birthday birthday;
    public Position defPosition;

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
        //gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        this.location = gameManager.gameLocations[0];
        this.pathFinding = gameObject.GetComponent<UpdatedPathFinding>();
        this.routines = Routine.FromJson(routineData.text, "winter");
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
        if (gameManager.dayNightCycle.gameTimer >= nextUpdate && onWay == false)
        {
            Debug.Log(gameManager.dayNightCycle.gameTimer + ">=" + nextUpdate);
            // Change the next update (current second+1)
            this.nextUpdate = gameManager.dayNightCycle.gameTimer + 1;
            // Call your fonction
            CheckRoutine(nextUpdate - 1);
        }

    }
    [System.Serializable]
    public class Birthday
    {
        public string Season;
        public int Day;

        public Birthday(string season, int day)
        {
            this.Season = season;
            this.Day = day;
        }
    }
    [System.Serializable]
    public class Position
    {
        public string Loc;
        public Tile Tile;

        public Position(string locName, Vector2 tilePos)
        {
            this.Loc = locName;
            this.Tile = GameManager.instance.sceneGrids[locName].tiles[tilePos];
        }
    }
    public void ReadJSON()
    {
        JObject jObject = JObject.Parse(NPCData.text);
        this.npcName = (string)jObject["npcName"];
        this.age = (string)jObject["age"];
        this.manners = (string)jObject["manners"];
        this.gender = (string)jObject["gender"];
        this.datable = (bool)jObject["datable"];
        this.likes = jObject["likes"].ToObject<List<int>>();
        this.dislikes = jObject["dislikes"].ToObject<List<int>>();

        string jBirthday = (string)jObject["birthday"];
        string[] bParts = jBirthday.Split(' ');
        string season = bParts[0];
        int day = int.Parse(bParts[1]);
        this.birthday = new Birthday(season, day);

        string dLocation = (string)jObject["defLocation"];
        string[] lParts = dLocation.Split(' ');
        string location = lParts[0];
        Vector2 tilePos = new Vector2(float.Parse(lParts[1]), float.Parse(lParts[2]));
        this.defPosition = new Position(location, tilePos);
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
                        this.onWay = true;
                        this.destination = gameManager.gameLocations.First(Location => Location.locationName == routine.destination);
                        StartPath(gameManager, routine.target);
                    }
                }
                break;
        }
    }

    public void StartPath(GameManager gameManager, Vector2 targetTile)
    {
        //Debug.Log(gameManager.sceneGrids.Count);
        this.pathFinding = gameObject.GetComponent<UpdatedPathFinding>();
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
        //StartCoroutine(ActivateScene(location.locationName));
    }
    IEnumerator ActivateScene(string scene)
    {
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(location.locationName, LoadSceneMode.Additive);
        yield return null;
        SceneManager.SetActiveScene(SceneManager.GetSceneByName(scene));
    }
}
