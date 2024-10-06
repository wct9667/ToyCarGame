using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private UnityEngine.UI.Image[] _images;

    private int score= 0;
    void Start()
    {
        foreach (UnityEngine.UI.Image image in _images)
        {
            image.enabled = false;
        }

        _images[0].enabled = true;
    }

    public void ChangeScore()
    {
        score++;
        switch (score)
        {
            case 0:
                DisableImages();
                _images[0].enabled = true;
                break;
            case 1:
                DisableImages();
                _images[1].enabled = true;
                break;
            case 2:
                DisableImages();
                _images[2].enabled = true;
                break;
            case 3:
                DisableImages();
                _images[3].enabled = true;
                break;
            case 4:
                DisableImages();
                _images[4].enabled = true;
                break;
            case 5:
                DisableImages();
                _images[5].enabled = true;
                break;
            case 6:
                DisableImages();
                _images[6].enabled = true;
                break;
            case 7:
                DisableImages();
                _images[7].enabled = true;
                break;
            case 8:
                DisableImages();
                _images[8].enabled = true;
                break;
            case 9:
                DisableImages();
                _images[9].enabled = true;
                break;
        }
    }

    private void DisableImages()
    {
        foreach (UnityEngine.UI.Image image in _images)
        {
            image.enabled = false;
        }
    }
}
