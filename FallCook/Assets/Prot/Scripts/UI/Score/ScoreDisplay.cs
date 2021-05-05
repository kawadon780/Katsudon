using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreDisplay : MonoBehaviour
{

    [SerializeField]
    Text ScoreText;

    public void SetScore(int score)
    {
        ScoreText.text = score.ToString();
    }

}
