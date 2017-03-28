using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreKeeperController : MonoBehaviour
{

    public static int score;

    Text text;


    void Start()
    {
        text = GetComponent<Text>();
        Reset();
    }

    public void ScorePoints(int points)
    {
        score += points;
        UpdateScoreText();
    }

    public static void Reset()
    {
        score = 0;
    }

    private  void UpdateScoreText()
    {
        if (text != null)
        {
            text.text = score.ToString();
        }
    }
}
