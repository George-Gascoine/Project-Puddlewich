using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class NPCTrade : MonoBehaviour
{
    public Inventory inventory;
    public bool trading;
    public NPCInventoryUI NPCInventoryUI;
    public Inventory_UI InventoryUI;
    public Player player;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();
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
            if(player.questManager.activeQuests.FindIndex(j => j.title == "Item Gatherer") != -1)
            {
                player.questManager.activeQuests[player.questManager.activeQuests.FindIndex(j => j.title == "Item Gatherer")].complete = true;
                player.questManager.activeQuests[player.questManager.activeQuests.FindIndex(j => j.title == "Item Gatherer")].Complete();
                Debug.Log("Complete");
            }
            //InventoryUI.inventoryPanel.SetActive(true);
            //NPCInventoryUI.inventoryPanel.SetActive(true);
            //NPCInventoryUI.npcTrade = this;
    }
}
