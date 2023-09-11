using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Inventory_UI : MonoBehaviour
{
    public GameObject inventoryPanel;
    public GameObject removePanel;
    public GameObject tooltip;
    public Item.ItemData movingItem;
    public int movingStack;
    public int movingID;
    public Player player;
    public List<Slot> slots = new();
    [SerializeField] private Canvas canvas;

    private Slot draggedSlot;
    private Image draggedIcon;
    // Start is called before the first frame update
    private void Start()
    {
        //draggedIcon = gameObject.AddComponent<Image>();
    }
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.E))
        {
            ToggleInventory();
        }
        if (inventoryPanel.activeSelf)
        {
            if (slots.Count == player.inventory.slots.Count && inventoryPanel.activeSelf)
            {
                for (int i = 0; i < slots.Count; i++)
                {
                    if (player.inventory.slots[i].item != null)
                    {
                        slots[i].SetItem(player.inventory.slots[i]);
                    }
                    else
                    {
                        slots[i].SetEmpty();
                    }
                }
            }
            if (draggedIcon != null)
            {
                tooltip.SetActive(false);
                Vector2 position;

                RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.transform as RectTransform, Input.mousePosition, null, out position);

                draggedIcon.gameObject.transform.position = canvas.transform.TransformPoint(position);
            }
        }
    }
    public void ToggleInventory()
    {
        if (!inventoryPanel.activeSelf)
        {
            Global.uiOpen = true;
            removePanel.SetActive(true);
            inventoryPanel.SetActive(true);
            Refresh();
        }
        else if (inventoryPanel.activeSelf)
        {
            if (draggedIcon != null) 
            {
               
            }
            else 
            {
                Global.uiOpen = false;
                tooltip.SetActive(false);
                removePanel.SetActive(false);
                inventoryPanel.SetActive(false);
            }
            
        }
    }
    public void Refresh()
    {
        if (slots.Count == player.inventory.slots.Count)
        {
            for (int i = 0; i < slots.Count; i++)
            {
                if (player.inventory.slots[i].item != null)
                {
                    slots[i].SetItem(player.inventory.slots[i]);
                }
                else
                {
                    slots[i].SetEmpty();
                }
            }
        }
    }

    public void MouseCheck(Slot slot)
    {
        Debug.Log(draggedIcon);
        if (draggedIcon == null)
        {
            movingID = slot.slotID;
            if (player.inventory.slots[slot.slotID].item.id != 0)
            {
                draggedIcon = Instantiate(slot.itemIcon);
                movingItem = slot.slotItem;
                movingStack = player.inventory.slots[slot.slotID].count;
                draggedIcon.raycastTarget = false;
                draggedIcon.rectTransform.sizeDelta = new Vector2(50f, 50f);
                draggedIcon.transform.SetParent(canvas.transform);
                player.inventory.RemoveAll(slot.slotID);
                slot.SetItem(player.inventory.slots[slot.slotID]);
            }
        }
        else if (draggedIcon != null && player.inventory.slots[slot.slotID].item.id == 0)
        {
            Destroy(draggedIcon.gameObject);
            draggedIcon = null;
            movingID = slot.slotID;
            player.inventory.AddToSlot(movingID, movingItem,movingStack);
            movingItem = null;
            movingStack = 0;
            StartCoroutine(TooltipUpdate(slot));
        }
        else if (draggedIcon != null && player.inventory.slots[slot.slotID].item.id != 0)
        {
            Item.ItemData temp = slot.slotItem;
            int tempStack = player.inventory.slots[slot.slotID].count;
            movingID = slot.slotID;
            Destroy(draggedIcon.gameObject);
            draggedIcon = Instantiate(slot.itemIcon);
            draggedIcon.raycastTarget = false;
            draggedIcon.rectTransform.sizeDelta = new Vector2(50f, 50f);
            draggedIcon.transform.SetParent(canvas.transform);
            player.inventory.AddToSlot(movingID, movingItem, movingStack);
            movingItem = temp;
            movingStack = tempStack;
            Refresh();
        }
    }

    public void DropItem()
    {
        if (draggedIcon != null)
        {
            player.inventory.RemoveItem(movingID);
            player.DropItem(movingItem);
            Destroy(draggedIcon);
            movingItem = null;
            movingStack = 0;
        }
    }
    private void MoveToMousePosition(GameObject toMove)
    {
        if(canvas!=null)
        {
            Vector2 position;

            RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.transform as RectTransform,Input.mousePosition,null,out position);

            toMove.transform.position = canvas.transform.TransformPoint(position);
        }
    }
    IEnumerator TooltipUpdate(Slot slot)
    {
        yield return null;
        slot.ActivateTooltip();
        slot.tooltip.transform.position = new Vector3(Input.mousePosition.x + 120, Input.mousePosition.y - 120, Input.mousePosition.z);
    }
}
