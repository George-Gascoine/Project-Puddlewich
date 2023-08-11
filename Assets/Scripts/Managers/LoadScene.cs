using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build.Content;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    public GameManager gameManager;
    [SerializeField] public List<string> sceneList;
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
        //Debug.Log("Activate");
        SceneManager.SetActiveScene(SceneManager.GetSceneByName(scene));

        gameManager.sceneColliders = GameObject.Find(scene + " " + "Manager").GetComponent<Location>().colliders;
        gameManager.GenerateGridWithCollisions(location);
        SceneManager.UnloadSceneAsync(scene);
        //SceneManager.MoveGameObjectToScene(player,SceneManager.GetSceneByName("Persistent Scene"));
    }
}
