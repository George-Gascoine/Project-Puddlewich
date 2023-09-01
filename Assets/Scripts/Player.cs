using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Linq;
using JetBrains.Annotations;

public class Player : MonoBehaviour
{
    public string playerName;
    public float speed = 3.0f;
    public float pennies = 10f;
    public Rigidbody2D rb;
    public Inventory inventory;
    public Item.ItemData equippedItem;
    public GameManager manager;
    public QuestManager questManager;
    public int selectedSlot;
    public bool slotChanged = false;
    public Sprite[] bodyParts;
    public SpriteRenderer hair, body, top, bottom;
    //public List<GameObject> myListObjects = new();

    public void Awake()
    {
        inventory = new Inventory(18);
        rb = GetComponent<Rigidbody2D>();
        manager = FindObjectOfType<GameManager>();
        questManager = FindObjectOfType<QuestManager>();
    }
    // Start is called before the first frame update
    void Start()
    {
        //inventory.Add(GameManager.instance.itemManager.GetItemByType(Collectable.ItemType.ITEM));
        if (inventory.slots[0].item != null)
        {
            equippedItem = inventory.slots[0].item;
        }
    }

    // Update is called once per frame
    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3 tempVect = new Vector3(h, v, -1.0f);
        tempVect = tempVect.normalized * speed * Time.deltaTime;
        rb.MovePosition(rb.transform.position + tempVect);
        if(slotChanged)
        {
            equippedItem = inventory.slots[selectedSlot].item;
            slotChanged = false;
        }
    }
    public void SetPlayer()
    {
        playerName = GameManager.instance.playerName;
        bodyParts = GameManager.instance.bodyParts;
        hair.sprite = bodyParts[0];
        body.sprite = bodyParts[1];
        top.sprite = bodyParts[2];
        bottom.sprite = bodyParts[3];
    }
    public void DropItem(Item.ItemData item)
    {
        Vector3 spawnLocation = transform.position;

        //Item droppedItem = Instantiate(item,spawnLocation,Quaternion.identity);
    }
}
