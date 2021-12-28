using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChoosingMode : MonoBehaviour
{
    private FadePanel _fadePanel;
    private void Awake()
    {
        _fadePanel = FindObjectOfType<FadePanel>();
    }

    /// <summary>
    /// записывается в кнопку, указать название мода то же что и у скриптбл обжекта
    /// запускает выбранный мод, также переходит к следующей сцене
    /// </summary>
    /// <param name="modeName"></param>
    public void OnModeChooseClick(string modeName)
    {
        
        if (Resources.Load<GameMode>("ScriptableObjects/GameMods/" + modeName) != null)
        {
            PlayerPrefs.SetString("Gamemode", modeName);
            _fadePanel.FadeOut(1);
        }            
        else
        {
            Debug.LogError("Неверно указано название мода!");
        }
           
    }
}
