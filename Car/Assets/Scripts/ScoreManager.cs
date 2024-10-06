using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;

    private int score;
    void Start()
    {
        text.text = "0";
    }

    public void ChangeScore()
    {
        score++;
        text.text = $"{score}";
    }
}
