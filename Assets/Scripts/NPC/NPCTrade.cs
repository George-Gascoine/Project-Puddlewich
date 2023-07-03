using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCTrade : MonoBehaviour
{
    public Inventory inventory;
    public bool trading;
    public NPCInventoryUI NPCInventoryUI;
    public Inventory_UI InventoryUI;
    // Start is called before the first frame update
    void Start()
    {
        trading = false;
        inventory = new Inventory(18);
        inventory.Add(GameManager.instance.itemManager.GetItemByType(Collectable.ItemType.ITEM));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnMouseDown()
    {
        trading = !trading;
        InventoryUI.inventoryPanel.SetActive(true);
        NPCInventoryUI.inventoryPanel.SetActive(true);
        NPCInventoryUI.npcTrade = this;
    }
}
