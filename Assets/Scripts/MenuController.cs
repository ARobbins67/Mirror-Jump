using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class MenuController : MonoBehaviour
{
    [SerializeField] GameObject WinGameMenu;
    [SerializeField] GameObject EscapeMenu;
    
    [SerializeField] GameObject WinLevelMenu;
    [SerializeField] TextMeshProUGUI winLevelText;

    // Start is called before the first frame update
    void Start()
    {
        winLevelText = WinLevelMenu.GetComponentInChildren<TextMeshProUGUI>();
        WinGameMenu.SetActive(false);
        WinLevelMenu.SetActive(false);
        EscapeMenu.SetActive(false);
    }

    public void SetEscapeMenu(bool state)
    {
        EscapeMenu.SetActive(state);
    }

    public void SetWinLevel(bool state, string message)
    {
        WinLevelMenu.SetActive(state);
        winLevelText.text = message;
    }

    public void SetWinGame(bool state)
    {
        WinGameMenu.SetActive(state);
    }
}
