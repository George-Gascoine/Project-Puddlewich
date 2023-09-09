using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolbarUI : MonoBehaviour
{
    [SerializeField] public List<Slot> toolbarSlots = new();
    public Player player;
    public Slot selectedSlot;

    public void Awake()
    {
        player = FindObjectOfType<Player>();
    }
    void Start()
    {
        player = FindObjectOfType<Player>();
        UpdateToolbar();
        foreach (Slot slot in toolbarSlots)
        {
            slot.toolbarSlot = true;
        }
        SelectSlot(0);    
    }
    // Update is called once per frame
    void Update()
    {
        CheckKeys();
        UpdateToolbar();
    }

    public void SelectSlot(int index)   
    {
        if(selectedSlot != null)
        {
            selectedSlot.SetHighlight(false);
        }
        player.selectedSlot = index;
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
                SelectSlot(number - 1);
                player.equippedItem = player.inventory.slots[number - 1].item;
            }
        }
    }
    public void UpdateToolbar()
    {
        for (int i = 0; i < toolbarSlots.Count; i++)
        {
            if (player.inventory.slots[i].item != null)
            {
                toolbarSlots[i].SetItem(player.inventory.slots[i]);
                toolbarSlots[i].quantityText.text = player.inventory.slots[i].count.ToString();
            }
            else
            {

            }
        }
    }
}
