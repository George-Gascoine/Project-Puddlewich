using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ShopperSpawner : MonoBehaviour
{
    public ShopManager shopManager;
    public NPCShopper shopper;
    public float shopperNo;
    public float shopperInterval = 2.0f;
    public List<Tile> browsePoints = new List<Tile>();

    // Start is called before the first frame update
    void Start()
    {
        shopperNo = 5.0f;
        StartCoroutine(SpawnShopper(shopperNo));
    }

    IEnumerator SpawnShopper(float number)
    {
        while (number > 0)
        {
        // Spawn a new Shopper
        var newShopper = Instantiate(shopper);
        shopManager.shopperList.Add(newShopper);
        var shopperStart = FindObjectOfType<Grid2D>().tiles[new Vector2(0, 0)];
        var shopperTarget = browsePoints[Random.Range(0, browsePoints.Count)];
        browsePoints.Remove(shopperTarget);
        newShopper.browsing = true;
        newShopper.start = shopperStart;
        newShopper.target = shopperTarget;
        number--;
        // wait for seconds
        yield return new WaitForSeconds(shopperInterval);
        }
    }
}
