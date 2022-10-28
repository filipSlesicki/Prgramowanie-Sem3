using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameTimer : MonoBehaviour
{
    [SerializeField] float startTime = 100;
    [SerializeField]  TMP_Text text;

    float currentTimeLeft;
    void Start()
    {
        currentTimeLeft = startTime;
        text.text = startTime.ToString("0.00");
    }

    // Update is called once per frame
    void Update()
    {
        currentTimeLeft -= Time.deltaTime;
        text.text = currentTimeLeft.ToString("0.00");
    }
}
