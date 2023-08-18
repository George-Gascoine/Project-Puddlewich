using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build.Content;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.Experimental.Rendering;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    public GameManager gameManager;
    //[SerializeField] public List<string> sceneList;
    //Log last time the player was in a scene
    //If the player is not in the same room as the NPC then the routine doesnt matter
    //If the player is in the same room the animation/pathfinding will play
    //

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //player.transform.position += new Vector3(1 * Time.deltaTime, 0, 0);
    }

    public void SceneLoader(string scene, Location location)
    {
        SceneManager.LoadScene(scene, LoadSceneMode.Additive);
        StartCoroutine(ActivateScene(scene, location));
    }
    IEnumerator ActivateScene(string scene, Location location)
    {
        yield return null;
        SceneManager.SetActiveScene(SceneManager.GetSceneByName(scene));
        gameManager.sceneColliders = GameObject.Find(scene + "Manager").GetComponent<Location>().colliders;
        foreach(Transition transition in GameObject.Find(scene + "Manager").GetComponent<Location>().transitions)
        {
            Transition.Warp warp = new()
            {
                location = location,
                locationTile = transition.locationTile,
                destination = transition.destination,
                destinationSpawn = transition.destinationSpawn
            };
            Debug.Log(warp.locationTile);
            gameManager.gameLocations.Find(x => x.locationName == scene).warps.Add(warp);//Work out why this is broken
        }
        gameManager.GenerateGridWithCollisions(location);
        SceneManager.UnloadSceneAsync(scene);
        //SceneManager.MoveGameObjectToScene(player,SceneManager.GetSceneByName("Persistent Scene"));
    }

    public void LoadToScene(string scene, Vector2 spawn)

    {
        SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());
        SceneManager.LoadScene(scene, LoadSceneMode.Additive);
        gameManager.player.transform.position = spawn;
        StartCoroutine(ChangeScene(scene));
    }
    IEnumerator ChangeScene(string scene)
    {
        yield return null;
        SceneManager.SetActiveScene(SceneManager.GetSceneByName(scene));
    }
}
