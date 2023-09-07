using UnityEngine;

[System.Serializable]
public class Transition : MonoBehaviour
{
    [System.Serializable]
    public class Warp
    {
        public Location location;
        public Vector2 locationTile;
        public Location destination;
        public Vector2 destinationSpawn;
    }

    public GameManager gameManager;
    public Location location;
    public Vector2 locationTile;
    public Location destination;
    public Vector2 destinationSpawn;

    private void Start()
    {
        gameManager = GameManager.instance;
        locationTile = this.transform.position;
        
    }

    public void OnCollisionEnter2D(Collision2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            gameManager.sceneLoader.LoadToScene(destination.locationName, destinationSpawn);
        }
    }

}
