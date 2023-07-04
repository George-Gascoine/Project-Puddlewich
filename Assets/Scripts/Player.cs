using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Linq;

public class Player : MonoBehaviour
{
    public float speed = 3.0f;
    public float pennies = 10f;
    public Rigidbody2D rb;
    public Inventory inventory;
    public Collectable equippedItem;
    public GameManager manager;
    //public List<GameObject> myListObjects = new();

    public void Awake()
    {
        inventory = new Inventory(18);
    }
    // Start is called before the first frame update
    void Start()
    {
        //inventory.Add(GameManager.instance.itemManager.GetItemByType(Collectable.ItemType.ITEM));
    }

    // Update is called once per frame
    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3 tempVect = new Vector3(h, v, -1.0f);
        tempVect = tempVect.normalized * speed * Time.deltaTime;
        rb.MovePosition(rb.transform.position + tempVect);
        if (inventory.slots[0].type != Collectable.ItemType.NONE)
        {
            equippedItem = GameManager.instance.itemManager.GetItemByType(inventory.slots[0].type);
        }
    }

    public void DropItem(Collectable item)
    {
        Vector3 spawnLocation = transform.position;

        Collectable droppedItem = Instantiate(item,spawnLocation,Quaternion.identity);
    }
}
