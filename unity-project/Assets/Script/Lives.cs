using UnityEngine;
using System.Collections;

public class Lives : MonoBehaviour
{
    bool lockIncrease = false;
    int previousPoints;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.points % 50 == 0 && lockIncrease == false && GameManager.points != 0)
        {
            previousPoints = GameManager.points;
            lockIncrease = true;
            GameManager.lives++;
        }
        if (lockIncrease && previousPoints != GameManager.points)
        {
            lockIncrease = false;
        }
    }
}
