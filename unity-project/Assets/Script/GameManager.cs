using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    public static float move_speed = 2f;
    public static int start_length = 7;
	public static int žebljiček_count = 1;
	public static int kače_count = 2;
    public static GameManager _instance;

    protected GameManager() { }

	void Awake () {
        _instance = this;
	}
}
