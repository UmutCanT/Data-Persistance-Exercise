using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;
#endif

[DefaultExecutionOrder(1000)]
public class MenuManager : MonoBehaviour
{
    [SerializeField] Text highScoreText;
    [SerializeField] Text placeHolderText;
    [SerializeField] Text playerNameText;

    string titleHS = "Can you beat {0} by getting more than {1} points?";

    // Start is called before the first frame update
    void Start()
    {
        TitleManager();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartNewGame()
    {
        DataManager.instance.playerName = PlayerNameManager();
        SceneManager.LoadScene(1);
    }

    public void Quit()
    {
        DataManager.instance.SaveHighScore(DataManager.instance.score,  DataManager.instance.playerName);
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }

    void TitleManager()
    {
        if(DataManager.instance.leaderName == null)
        {
            highScoreText.text = "First Contender!!!";
        }
        else
        {
            highScoreText.text = string.Format(titleHS, DataManager.instance.leaderName, DataManager.instance.score);
            placeHolderText.text = DataManager.instance.leaderName;
        }
    }

    string PlayerNameManager()
    {
        if(playerNameText.text == null)
        {
            return placeHolderText.text;
        }
        else
        {
            return playerNameText.text;
        }
    }
}
