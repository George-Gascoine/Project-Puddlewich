using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Inventory_UI : MonoBehaviour
{
    public GameObject inventoryPanel;
    public GameObject removePanel;
    public Item.ItemData movingItem;
    public int movingStack;
    public int movingID;
    public Player player;
    public List<Slot> slots = new();
    [SerializeField] private Canvas canvas;

    private Slot draggedSlot;
    private Image draggedIcon;
    // Start is called before the first frame update

    private void Awake()
    {
        canvas = FindObjectOfType<Canvas>();
        player = FindObjectOfType<Player>();   
    }
    void Start()
    {
        player = FindObjectOfType<Player>();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            ToggleInventory();
        }

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
        if(draggedIcon != null)
        {
            Vector2 position;

            RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.transform as RectTransform, Input.mousePosition, null, out position);

            draggedIcon.gameObject.transform.position = canvas.transform.TransformPoint(position);
        }
    }
    public void ToggleInventory()
    {
        if (!inventoryPanel.activeSelf)
        {
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
        if (draggedIcon == null)
        {
            movingID = slot.slotID;
            draggedIcon = Instantiate(slot.itemIcon);
            movingItem = slot.slotItem;
            movingStack = player.inventory.slots[slot.slotID].count;
            draggedIcon.raycastTarget = false;
            draggedIcon.rectTransform.sizeDelta = new Vector2(50f, 50f);
            draggedIcon.transform.SetParent(canvas.transform);
            player.inventory.RemoveAll(slot.slotID);
        }
        else if(draggedIcon != null && player.inventory.slots[slot.slotID].item == null)
        {
            movingID = slot.slotID;
            player.inventory.slots[slot.slotID].AddItem(movingItem);
            player.inventory.slots[slot.slotID].count = movingStack;
            Destroy(draggedIcon);
            movingItem = null;
            movingStack = 0;
        }
        else if (draggedIcon != null && player.inventory.slots[slot.slotID].item != null)
        {
            Item.ItemData temp = slot.slotItem;
            int tempStack = player.inventory.slots[slot.slotID].count;
            movingID = slot.slotID;
            Destroy(draggedIcon);
            draggedIcon = Instantiate(slot.itemIcon);
            draggedIcon.raycastTarget = false;
            draggedIcon.rectTransform.sizeDelta = new Vector2(50f, 50f);
            draggedIcon.transform.SetParent(canvas.transform);
            player.inventory.slots[slot.slotID].AddItem(movingItem);
            player.inventory.slots[slot.slotID].count = movingStack;
            movingItem = temp;
            movingStack = tempStack;
        }
    }

    public void DropItem()
    {
        if (draggedIcon != null)
        {
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
}
