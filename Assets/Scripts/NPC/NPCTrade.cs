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
    public QuestLog questLog;
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
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            trading = !trading;
            if (Global.TRADE_QUEST == false && questLog.questLog.FindIndex(j => j.title == "Buy an Item") != -1)
            {
                Global.TRADE_QUEST = true;
                int index = questLog.questLog.FindIndex(j => j.title == "Buy an Item");
                questLog.questLog[index].Complete();
            }
            InventoryUI.inventoryPanel.SetActive(true);
            NPCInventoryUI.inventoryPanel.SetActive(true);
            NPCInventoryUI.npcTrade = this;
        }
    }
}
