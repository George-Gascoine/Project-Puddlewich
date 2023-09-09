using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using static Item;
using UnityEngine.EventSystems;
using System;
using static SaveGame;
//Slot Class
public class Slot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public bool isInputEnabled = false;

    public void OnPointerEnter(PointerEventData eventData)
    {
        isInputEnabled = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isInputEnabled = false;
    }

    void Update()
    {
        if (isInputEnabled)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Debug.Log(slotID);
            }
        }
    }

    public Item.ItemData slotItem;
    public int slotID;
    public int quantity;
    public Image itemIcon;
    public TextMeshProUGUI quantityText;
    public bool sellableSlot = false;
    public bool mouseOver = false;
    public GameObject tooltip;
    public bool toolbarSlot;
    [SerializeField] private GameObject highlight;

    public void SetItem(Inventory.Slot slot)
    {
        Item.ItemData displayItem = new Item.ItemData
        {
            name = slot.item.name,
            id = slot.item.id,
            type = slot.item.type,
            effect = slot.item.effect,
            description = slot.item.description,
            sprite = slot.item.sprite,
            cost = slot.item.cost,
            quality = slot.item.quality
        };
        slotItem = displayItem;
        quantity = slot.count;
        quantityText.text = quantity.ToString();
        itemIcon.sprite = Resources.Load<Sprite>("Sprites/Items/" + slotItem.sprite);
    }
    public void SetEmpty()
    {

    }

    public void SetHighlight(bool isOn)
    {
        highlight.SetActive(isOn);
    }

    public void ActivateTooltip()
    {
        if (slotItem != null)
        {
            tooltip.gameObject.SetActive(true);
            tooltip.GetComponent<Tooltip>().slot = this;
            tooltip.GetComponent<Tooltip>().SetTooltip();
        }

    }

    public void DeActivateTooltip()
    {
        tooltip.gameObject.SetActive(false);
    }
}
