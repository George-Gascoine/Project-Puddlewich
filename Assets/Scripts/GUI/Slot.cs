using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using static Item;
//Slot Class
public class Slot : MonoBehaviour
{
    public Item.ItemData slotItem;
    public int slotID;
    public Image itemIcon;
    public TextMeshProUGUI quantityText;
    public bool sellableSlot = false;
    public bool mouseOver = false;
    [SerializeField] private GameObject highlight;

    public void SetItem(Inventory.Slot slot)
    {
        if(slot != null)
        {
            slotItem = slot.item;
            itemIcon.sprite = Resources.Load<Sprite>("Sprites/Items/" + slot.item.sprite);
            //itemIcon.sprite = slot.icon;
            itemIcon.color = new Color(1, 1, 1, 1);
            quantityText.text = slot.count.ToString();
        }
    }
    public void SetEmpty()
    {
        itemIcon.sprite = null;
        itemIcon.color = new Color(1, 1, 1, 0);
        quantityText.text = "";
    }

    public void SetHighlight(bool isOn)
    {
        highlight.SetActive(isOn);
    }

    public void OnMouseOver()
    {
        mouseOver = true;
        Debug.Log(mouseOver);
    }
}
