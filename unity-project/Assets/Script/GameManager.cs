using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System;

public class GameManager : MonoBehaviour
{
    void Start()
    {
        loadScore();
    }
    private static bool _pause = false;
    public static float move_speed = 2f;
    public static int start_length = 7;
    public static int žebljiček_count = 2;
    public static int kače_count = 2;

    public static int points = 0;
    private static int max_lifes = 5;
    public static int lives = 5;

    protected GameManager() { }

    public static bool pause
    {
        get { return _pause; }
        set
        {
            _pause = value;
            if (_pause)
                Time.timeScale = 0;
            else
                Time.timeScale = 1;
        }
    }

    public static void resetGame()
    {
        points = 0;
        lives = max_lifes;
        pause = false;
    }

    [Serializable]
    public class ScoreEntry
    {
        public string name;
        public int score;
    }

    private static List<ScoreEntry> _highScores = new List<ScoreEntry>();

    public static List<ScoreEntry> highScores
    {
        get { return _highScores; }
    }
    public static void highScoresAdd(ScoreEntry entry){
        _highScores.Add(entry);
        _highScores.Sort(delegate(ScoreEntry x, ScoreEntry y)
        {
            return y.score.CompareTo(x.score);
        });

        while (_highScores.Count > 5)
            _highScores.RemoveAt(_highScores.Count - 1);
        SaveScores();
    }
    public static void highScoresClear()
    {
        _highScores.Clear();
        SaveScores();
    }

    private static void SaveScores()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        MemoryStream stream = new MemoryStream();

        formatter.Serialize(stream, highScores);
        PlayerPrefs.SetString("HighScores",
            Convert.ToBase64String(
                stream.GetBuffer()
            )
        );
    }

    private static void loadScore()
    {
        String data = PlayerPrefs.GetString("HighScores");
        if (!String.IsNullOrEmpty(data))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            MemoryStream stream = new MemoryStream(Convert.FromBase64String(data));
            _highScores = formatter.Deserialize(stream) as List<ScoreEntry>;
        }
    }
}
