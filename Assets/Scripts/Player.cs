using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Linq;
using JetBrains.Annotations;
using static Crop;

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
        inventory.CreateInventory(18);
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
        DropItem(GameManager.instance.itemManager.itemList.item.Single(s => s.id == 1));
        DropItem(GameManager.instance.itemManager.itemList.item.Single(s => s.id == 2));
        DropItem(GameManager.instance.itemManager.itemList.item.Single(s => s.id == 4));
        DropItem(GameManager.instance.itemManager.itemList.item.Single(s => s.id == 6));
    }

    // Update is called once per frame
    void Update()
    {
        float inputX = Input.GetAxis("Horizontal");
        float inputY = Input.GetAxis("Vertical");

        Vector2 movement = new Vector2(inputX, inputY);
        movement.Normalize();
        Vector2 isoMovement = new Vector2();
        //Up
        if (Input.GetKey(KeyCode.W))
        {
            isoMovement = new Vector2(0, 1);
            if (Input.GetKey(KeyCode.A))
            {
                isoMovement = new Vector2(-1, 0.5f);
            }
            if (Input.GetKey(KeyCode.D))
            {
                isoMovement = new Vector2(1, 0.5f);
            }
        }
        //Down
        if (Input.GetKey(KeyCode.S))
        {
            isoMovement = new Vector2(0, -1);
            if (Input.GetKey(KeyCode.A))
            {
                isoMovement = new Vector2(-1, -0.5f);
            }
            if (Input.GetKey(KeyCode.D))
            {
                isoMovement = new Vector2(1, -0.5f);
            }
        }
        //Left
        if (Input.GetKey(KeyCode.A))
        {
            isoMovement = new Vector2(-1, 0);
            if (Input.GetKey(KeyCode.S))
            {
                isoMovement = new Vector2(-1, -0.5f);
            }
            if (Input.GetKey(KeyCode.W))
            {
                isoMovement = new Vector2(-1, 0.5f);
            }
        }
        //Right
        if (Input.GetKey(KeyCode.D))
        {
            isoMovement = new Vector2(1, 0);
            if (Input.GetKey(KeyCode.S))
            {
                isoMovement = new Vector2(1, -0.5f);
            }
            if (Input.GetKey(KeyCode.W))
            {
                isoMovement = new Vector2(1, 0.5f);
            }
        }
        transform.Translate(isoMovement * Time.deltaTime * speed);
        if (slotChanged)
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
        Vector3 spawnLocation = new Vector3(transform.position.x,transform.position.y - 1, transform.position.z);

        GameObject droppedItem = Instantiate(GameManager.instance.baseItem,spawnLocation,Quaternion.identity);
        droppedItem.GetComponent<Item>().data = item;
    }
}
