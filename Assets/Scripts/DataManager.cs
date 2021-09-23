using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class DataManager : MonoBehaviour
{
    public static DataManager instance;

    public string leaderName;
    public string playerName;
    public int score;

    void Awake()
    {
        if(instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
        LoadHighScore();
    }

    [System.Serializable]
    class SavedData
    {
        public string leaderPlayerName;
        public int highScore;
    }

    public void SaveHighScore(int score, string playerName)
    {
        SavedData data = new SavedData();
        if (score > data.highScore)
        {
            data.leaderPlayerName = playerName;
            data.highScore = score;
        }
        string json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + "/playerdata.json", json);
    }

    public void LoadHighScore()
    {
        string path = Application.persistentDataPath + "/playerdata.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SavedData data = JsonUtility.FromJson<SavedData>(json);

            leaderName = data.leaderPlayerName;
            score = data.highScore;
        }
        else
        {
            leaderName = null;
            score = 0;
        }
    }

    public string HighScoreToText()
    {
        return string.Format("Leader: {0} with {1} points", leaderName, score);
    }
}
