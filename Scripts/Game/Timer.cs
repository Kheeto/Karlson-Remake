using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    [SerializeField] TMP_Text text;
    [SerializeField] float time;
    public bool active = true;

    void Update()
    {
        if (active)
        {
            time += Time.deltaTime;
            float textTime = Mathf.Round(time * 100) / 100;
            text.text = textTime.ToString().Replace(".", ":");
        }
    }
}
