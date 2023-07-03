using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
//Slot Class
public class Slot : MonoBehaviour
{
    public Collectable slotItem;
    public int slotID;
    public Image itemIcon;
    public TextMeshProUGUI quantityText;
    public bool sellableSlot = false;
    [SerializeField] private GameObject highlight;

    public void SetItem(Inventory.Slot slot)
    {
        if(slot != null)
        {
            slotItem = GameManager.instance.itemManager.GetItemByType(slot.type);
            itemIcon.sprite = GameManager.instance.itemManager.GetSpriteByType(slot.type);
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
}
