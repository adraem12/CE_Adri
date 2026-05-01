using System;
using UnityEngine;

public class ClockButtonScript : MonoBehaviour
{
    public event EventHandler<ClockButtonScript> OnTriggerActivated;
    public int buttonType;

    private void OnTriggerEnter(Collider other)
    {
        OnTriggerActivated?.Invoke(this, this);
    }
}