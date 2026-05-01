using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ClockScript : MonoBehaviour
{
    public bool isRealTime = true;
    public List<ClockButtonScript> buttons;
    public GameObject realTimeText;
    public GameObject virtualTimeText;
    public TextMeshProUGUI clockText;
    public float realTime;
    public float virtualTime = 12;
    GameObject sun;

    private void Awake()
    {
        sun = RenderSettings.sun.gameObject;
        foreach (ClockButtonScript button in buttons) 
            button.OnTriggerActivated += ProcessButton;
    }

    private void Update()
    {
        Vector3 newRotation;
        float currentTime = isRealTime ? realTime : virtualTime;
        if (isRealTime)
            realTime = DateTime.Now.Hour + (DateTime.Now.Minute + DateTime.Now.Second * 0.01667f) * 0.01667f;
        newRotation = new Vector3((currentTime / 24 * 360) - 90f, -20f, 0);
        sun.transform.localRotation = Quaternion.Euler(newRotation);
        DrawClockTime(currentTime);
    }

    private void DrawClockTime(float currentTime)
    {
        clockText.text = TimeSpan.FromHours(currentTime).ToString(@"hh\:mm\:ss");
    }

    void ChangeTimeType()
    {
        realTimeText.SetActive(isRealTime);
        virtualTimeText.SetActive(!isRealTime);
        buttons[1].gameObject.SetActive(!isRealTime);
        buttons[2].gameObject.SetActive(!isRealTime);
    }

    public void ProcessButton(object sender, ClockButtonScript button)
    {
        if (button.buttonType == 0)
        {
            isRealTime = !isRealTime;
            ChangeTimeType();
        }
        else
        {
            if (button.buttonType == 1)
                virtualTime += 1;
            else
                virtualTime -= 1;
            virtualTime %= 24;
            if (virtualTime < 0)
                virtualTime = 23;
        }
    }
}