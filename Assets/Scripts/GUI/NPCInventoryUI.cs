using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCInventoryUI : MonoBehaviour
{
    public NPCTrade npcTrade;
    public Player player;
    public GameObject inventoryPanel;
    public List<Slot> slots = new();

    [SerializeField] private Canvas canvas;
    // Start is called before the first frame update
    private void Awake()
    {
        player = FindObjectOfType<Player>();
        canvas = FindObjectOfType<Canvas>();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (slots.Count == npcTrade.inventory.slots.Count)
        {
            for (int i = 0; i < slots.Count; i++)
            {
                slots[i].sellableSlot = true;
                if (npcTrade.inventory.slots[i].type != Collectable.ItemType.NONE)
                {
                    slots[i].SetItem(npcTrade.inventory.slots[i]);
                }
                else
                {
                    slots[i].SetEmpty();
                }
            }
        }
    }

    public void BuyItem(Slot slot)
    {
        player.inventory.Add(slot.slotItem);
        player.pennies += GameManager.instance.itemManager.GetPriceByType(slot.slotItem.type);
        npcTrade.inventory.Remove(slot.slotID);
        Refresh();
    }

    public void Refresh()
    {
        if (slots.Count == npcTrade.inventory.slots.Count)
        {
            for (int i = 0; i < slots.Count; i++)
            {
                if (npcTrade.inventory.slots[i].type != Collectable.ItemType.NONE)
                {
                    slots[i].SetItem(npcTrade.inventory.slots[i]);
                }
                else
                {
                    slots[i].SetEmpty();
                }
            }
        }
    }
}
