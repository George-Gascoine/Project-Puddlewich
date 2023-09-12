using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static SaveGame;

public class StartMenu : MonoBehaviour
{
    public GameObject mainCanvas;
    public GameObject loadCanvas;
    public List<Button> loadedGames;
    public Button loadButton;

    // Called when we click the "Play" button.
    public void OnPlayButton()
    {
        SceneManager.LoadScene(1);
    }

    public void PopulateLoads()
    {
        mainCanvas.SetActive(false);
        loadCanvas.SetActive(true);
        SavePlayer playerLoad = new SavePlayer();
        DirectoryInfo dir = new DirectoryInfo("C:\\GitHub\\Potioneer\\Puddlewich\\Project Potioneer\\Project-Puddlewich\\Saves");
        FileInfo[] info = dir.GetFiles("*_*");
        
        int i = 0;
        foreach (FileInfo f in info)
        {
            i++;
            Debug.Log(f);
            Button newButton = Instantiate(loadButton);
            newButton.transform.SetParent(loadCanvas.transform, false);
            var ty = newButton.GetComponentInChildren<TextMeshProUGUI>();
            var pos = newButton.GetComponent<RectTransform>();
            pos.anchoredPosition = new Vector2(0, 200 - i * 80);
            string saveText = File.ReadAllText(f.FullName);
            SaveClass loadClass = JsonUtility.FromJson<SaveClass>(saveText);
            SavePlayer loadPlayer = loadClass.savePlayer;
            int loadTime = loadClass.saveTime;
            ty.text = loadPlayer.playerName + " " + loadPlayer.saveTime;
            loadedGames.Add(newButton);
            newButton.onClick.AddListener(() => { Load(loadClass); });
        }
    }

    public void Load(SaveClass loadClass)
    {
        foreach (Button butt in loadedGames)
        {
            Destroy(butt.gameObject);
        }
        SavePlayer loadPlayer = loadClass.savePlayer;
        GameManager.instance.load = true;
        GameManager.instance.GameStart();
        GameManager.instance.playerName = loadPlayer.playerName;
        GameManager.instance.player.pennies = loadPlayer.pennies;
        GameManager.instance.player.inventory = loadPlayer.inventory;
        GameManager.instance.player.transform.position = loadPlayer.position;
        GameManager.instance.bodyParts = loadPlayer.bodyParts;
    }
    // Called when we click the "Quit" button.
    public void OnQuitButton()
    {
        Application.Quit();
    }
}
