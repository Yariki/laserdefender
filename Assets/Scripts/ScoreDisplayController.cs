using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreDisplayController : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        Text myScore = GetComponent<Text>();
        myScore.text = ScoreKeeperController.score.ToString();
        ScoreKeeperController.Reset();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
