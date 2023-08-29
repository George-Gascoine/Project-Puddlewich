using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    public Player player;
    public ItemType type;
    public Sprite icon;
    public int itemCost;
    public Tile buyingTile;
    public ShopManager shopManager;
    public FarmManager farmManager;
    public Crop crop;
    public bool pickUp;
    public AudioSource popPickUp;
    public AudioClip clip;
    public void Awake()
    {
        shopManager = FindObjectOfType<ShopManager>();
        farmManager = FindObjectOfType<FarmManager>();
        popPickUp = GetComponent<AudioSource>();
    }

    private void Update()
    {
        //if (popPickUp != null)
        //{ popPickUp.Play();
        //}
        //else
        //{
        //    popPickUp.clip = clip;
        //    popPickUp.Play();
        //}
        if (pickUp == true)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, 0.01f);
            if (transform.position == player.transform.position)
            {
                CollectableAdd();
            }
        }
    }

    void Start()
    {
        
        player = FindObjectOfType<Player>();
        icon = GetComponent<SpriteRenderer>().sprite;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            pickUp = true;
        }
    }

    public void CollectableAdd()
    {
        player.inventory.Add(this);
        player.slotChanged = true;
        popPickUp.PlayOneShot(clip);
        pickUp = false;
        StartCoroutine(CollectableDestroy());
    }

    IEnumerator CollectableDestroy()
    {
        yield return new WaitWhile(() => popPickUp.isPlaying);
        Destroy(this.gameObject);
    }
    public enum ItemType
    {
        NONE, 
        ITEM,
        FOOD,
        DRINK,
        HOE,
        WATERINGCAN,
        SEED,
        CHILLISEED,
        TOMATOSEED,
        CUCUMBERSEED,
        ONIONSEED
    }
}

