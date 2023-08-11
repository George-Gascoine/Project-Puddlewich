using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NPCTalk : MonoBehaviour
{
    [SerializeField] public GameObject textPanel;
    public GameObject NPC;
    public Player player;
    public TextMeshProUGUI NPCText;
    public float friendshipLevel;
    public string[] selectedDiaArray;
    public string[] dialogue;
    public string[] friendshipDialogue;
    public List<Collectable> likedItems;
    public List<Collectable> dislikedItems;
    public int index;
    public bool greeted = false; 
    public bool typing = false;

    public float wordSpeed;
    public bool playerClose;
    private void Start()
    {
        index = 0;
        player = FindObjectOfType<Player>();
    }
    void OnMouseUp()
    {
        if(player.equippedItem != null && playerClose && !textPanel.activeInHierarchy)
        {
            UpgradeFriendship(player.equippedItem);
            Debug.Log("Close");
        }
        else if (playerClose && !textPanel.activeInHierarchy)
        {
            selectedDiaArray = dialogue;
            greeted = true;
            textPanel.SetActive(true);
            StartCoroutine("Typing");
        }
    }

    private void Update()
    {
        if (textPanel.activeInHierarchy && playerClose)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (NPCText.text == selectedDiaArray[index])
                {
                    Debug.Log("Line");
                    if(selectedDiaArray == friendshipDialogue)
                    {
                        EndText();
                    }
                    else
                    { 
                        NextLine();
                    }
                }
                else if (typing == true)
                {
                    Debug.Log(index);
                    StopCoroutine("Typing");
                    typing = false;
                    NPCText.text = selectedDiaArray[index];
                }
            }
        }
    }

    IEnumerator Typing()
    {
        typing = true;
        foreach (char letter in selectedDiaArray[index].ToCharArray())
        {
            NPCText.text += letter;
            yield return new WaitForSeconds(wordSpeed);
        }
    }
    
    public void NextLine()
    {
        if (index < selectedDiaArray.Length - 1)
        {
            index++;
            NPCText.text = "";
            StartCoroutine("Typing");
        }
        else
        {
            EndText();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerClose = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerClose = false;
            EndText();
        }
    }

    public void EndText()
    {
        NPCText.text = "";
        index = 0;
        textPanel.SetActive(false);
    }

    public void UpgradeFriendship(Collectable gift)
    {
        selectedDiaArray = friendshipDialogue;
        if (likedItems.IndexOf(gift) != -1)
        {
            player.inventory.Remove(player.selectedSlot);
            player.slotChanged = true;
            friendshipLevel += 0.5f;
            index = 0;
            textPanel.SetActive(true);
            StartCoroutine("Typing");
        }
        else if (dislikedItems.IndexOf(gift) != -1)
        {
            player.inventory.Remove(player.selectedSlot);
            player.slotChanged = true;
            friendshipLevel -= 0.5f;
            index = 1;
            textPanel.SetActive(true);
            StartCoroutine("Typing");
        }
        else
        {
            player.inventory.Remove(player.selectedSlot);
            player.slotChanged = true;
            index = 2;
            textPanel.SetActive(true);
            StartCoroutine("Typing");
        }
    }
}
