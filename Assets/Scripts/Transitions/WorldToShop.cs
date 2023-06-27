using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Make sure to add this, or you can't use SceneManager
using UnityEngine.SceneManagement;


public class WorldToShop : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;
        
        if (collision.gameObject.CompareTag("Player"))
        {
            if(sceneName == "World")
            {
                SceneManager.LoadScene(1);
            }
            if(sceneName == "Player's Shop")
            {
                SceneManager.LoadScene(0);
            }
        }
    }
}