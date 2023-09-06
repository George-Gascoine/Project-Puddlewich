using System;
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
    public CrossFade screenFade;
    //[SerializeField] public List<string> sceneList;
    //Log last time the player was in a scene
    //If the player is not in the same room as the NPC then the routine doesnt matter
    //If the player is in the same room the animation/pathfinding will play
    //

    // Start is called before the first frame update
    void Start()
    {
        //screenFade.ScreenFadeOut();
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
        screenFade.ScreenFadeOut();   
        StartCoroutine(ChangeScene(scene, 0.5f, spawn));
        //screenFade.ScreenFadeIn();
    }
    IEnumerator ChangeScene(string scene, float delay, Vector2 spawn)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(scene, LoadSceneMode.Additive);
        SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());
        gameManager.player.transform.position = spawn;
        screenFade.ScreenFadeIn();
        yield return null;
        SceneManager.SetActiveScene(SceneManager.GetSceneByName(scene));
        if (scene == "World")
        {
            GameManager.instance.player.GetComponent<Farming>().ResetFarm();
        }
    }
}
