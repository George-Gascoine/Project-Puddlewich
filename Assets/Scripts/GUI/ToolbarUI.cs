using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolbarUI : MonoBehaviour
{
    [SerializeField] public List<Slot> toolbarSlots = new();
    public Player player;
    public Slot selectedSlot;

    void Start()
    {
        SelectSlot(0);    
    }
    // Update is called once per frame
    void Update()
    {
        CheckKeys();
        for (int i = 0; i < toolbarSlots.Count; i++)
        {
            if (player.inventory.slots[i].type != Collectable.ItemType.NONE)
            {
                toolbarSlots[i].SetItem(player.inventory.slots[i]);
            }
            else
            {
                toolbarSlots[i].SetEmpty();
            }
        }
    }

    public void SelectSlot(int index)   
    {
        if(selectedSlot != null)
        {
            selectedSlot.SetHighlight(false);
        }
        selectedSlot = toolbarSlots[index];
        selectedSlot.SetHighlight(true);
    }

    private void CheckKeys()
    {
        if (Input.inputString != "")
        {
            int number;
            bool is_a_number = Int32.TryParse(Input.inputString, out number);
            if (is_a_number && number >= 1 && number < 10)
            {
                SelectSlot(number-1);
                player.equippedItem = GameManager.instance.itemManager.GetItemByType(player.inventory.slots[number - 1].type);
            }
        }
    }
}