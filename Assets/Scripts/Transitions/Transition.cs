using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Make sure to add this, or you can't use SceneManager
using UnityEngine.SceneManagement;


public class Transition : MonoBehaviour
{
    
    public Vector3 destination;
    Scene currentScene;
    public SceneField destinationScene;
    public TransitionType transitionType;
    Player player;
    public enum TransitionType
    {
        SceneWarp,
        SceneChange
    }
    private void Start()
    {
        player = FindObjectOfType<Player>();
        currentScene = SceneManager.GetActiveScene();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Collide");
        StartTransition(collision.transform);
    }
    public void StartTransition(Transform toTransition)
    {
        switch (transitionType)
        {
            case TransitionType.SceneWarp:
                toTransition.position = destination;
                break; 
            case TransitionType.SceneChange:
                SceneManager.LoadScene(destinationScene);
                //SceneManager.UnloadSceneAsync(currentScene);
                toTransition.transform.position = destination;
                break;
        }
    }
}