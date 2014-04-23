using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {

    private static bool _pause = false;
    public static float move_speed = 2f;
    public static int start_length = 7;
	public static int žebljiček_count = 1;
	public static int kače_count = 2;

    public static int points = 0;
    private static int max_lifes = 5;
    public static int lifes = 5;

    protected GameManager() { }

    public static bool pause
    {
        get { return _pause;}
        set { _pause = value;
        if (_pause)
            Time.timeScale = 0;
        else
            Time.timeScale = 1;
        }
    }

    public static void resetGame(){
        points = 0;
        lifes = max_lifes;
    }

    public class ScoreEntry
    {
        public string name;
        public int score;
    }

    public static List<ScoreEntry> highScores = new List<ScoreEntry>();
}
