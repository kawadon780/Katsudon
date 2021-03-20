using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreDisplay : MonoBehaviour
{

    [SerializeField]
    Text ScoreText;

    public int NowScore { get; private set; }

    public void Addition(int score)
    {
        NowScore += score;
        ScoreText.text = NowScore.ToString();
    }

}
