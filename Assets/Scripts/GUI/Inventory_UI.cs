using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Inventory_UI : MonoBehaviour
{
    public GameObject inventoryPanel;
    public GameObject removePanel;
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
            removePanel.SetActive(false);
            inventoryPanel.SetActive(false);
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

    public void SlotBeginDrag(Slot slot)
    {
        draggedSlot = slot;
        draggedIcon = Instantiate(slot.itemIcon);
        draggedIcon.raycastTarget = false;
        draggedIcon.rectTransform.sizeDelta = new Vector2(50f, 50f);
        draggedIcon.transform.SetParent(canvas.transform);
        

        MoveToMousePosition(draggedIcon.gameObject);
        Debug.Log("Begin");
    }

    public void SlotDrag()
    {
        MoveToMousePosition(draggedIcon.gameObject);
        Debug.Log("Dragging");
    }

    public void SlotEndDrag()
    {
        Destroy(draggedIcon.gameObject);
        draggedIcon = null;
        Debug.Log("End");
    }
    public void SlotDrop(Slot slot)
    {
        Item.ItemData itemToDrop = player.inventory.slots[draggedSlot.slotID].item;
        player.inventory.Remove(draggedSlot.slotID);
        player.DropItem(itemToDrop);
        Refresh();
        Debug.Log("Drop");
        draggedSlot = null;
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
