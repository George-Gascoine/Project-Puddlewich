using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Collectable;
using static Inventory;

[System.Serializable]
public class Item: MonoBehaviour
{
    [System.Serializable]
    public class ItemData
    {
        public string name;
        public int id;
        public string type;
        public string effect;
        public string description;
        public string sprite;
        public int cost;
        public int quality;
    }

    public ItemData data;

    [System.Serializable]
    public class Recipe
    {
        public string name;
        public List<string> steps = new();
        public List<bool> completedSteps = new();

        void Start()
        {
            // Initialize the completedSteps list with the same number of elements as the steps list
            completedSteps = new List<bool>(new bool[steps.Count]);
        }

        public void CompleteStep(int stepIndex)
        {
            // Mark the specified step as completed
            completedSteps[stepIndex] = true;
        }

        public bool IsRecipeCompleted()
        {
            // Check if all steps have been completed
            foreach (bool stepCompleted in completedSteps)
            {
                if (!stepCompleted)
                {
                    return false;
                }
            }
            return true;
        }
    }
    public Player player;
    public bool onGround { get; set; } = true;
    public int itemCost;
    public Tile buyingTile;
    SpriteRenderer spriteRenderer;
    public ShopManager shopManager;
    public FarmManager farmManager;
    public bool pickUp;
    public Crop crop;
    public AudioSource popPickUp;
    public AudioClip clip;
    public int stack;
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
    public void Start()
    {
        itemCost = data.cost;
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = Resources.Load<Sprite>("Sprites/Items/" + data.sprite);
    }
    void Awake()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && onGround)
        {
            pickUp = true;
        }
    }

    public void CollectableAdd()
    {
        player.inventory.Add(this.data);
        player.slotChanged = true;
        popPickUp.PlayOneShot(clip);
        spriteRenderer.sprite = null;
        pickUp = false;
        StartCoroutine(CollectableDestroy());
    }

    IEnumerator CollectableDestroy()
    {
        yield return new WaitWhile(() => popPickUp.isPlaying);
        Destroy(this.gameObject);
    }
}
