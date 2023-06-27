using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NPCTalk : MonoBehaviour
{
    [SerializeField] public GameObject textPanel;
    public GameObject NPC;
    public TextMeshProUGUI NPCText;
    public string[] dialogue;
    private int index;

    public float wordSpeed;
    public bool playerClose;
    private void Start()
    {
        index = 0;
    }
    void OnMouseDown()
    {
        if (playerClose)
        {
            textPanel.SetActive(true);
            StartCoroutine(Typing());
        }
    }

    private void Update()
    {
        if (textPanel.activeInHierarchy && playerClose)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if(NPCText.text == dialogue[index])
                {
                    NextLine();
                }
            }
        }
    }

    IEnumerator Typing()
    {
        foreach (char letter in dialogue[index].ToCharArray())
        {
            NPCText.text += letter;
            yield return new WaitForSeconds(wordSpeed);
        }
    }
    
    public void NextLine()
    {
        if (index < dialogue.Length - 1)
        {
            index++;
            NPCText.text = "";
            StartCoroutine(Typing());
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
        textPanel.SetActive(false);
    }
}
