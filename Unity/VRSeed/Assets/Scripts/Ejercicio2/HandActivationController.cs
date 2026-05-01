using System.Collections;
using UnityEngine;

public class HandActivationController : MonoBehaviour
{
    public void CallActivateHand(GameObject hand)
    {
        StartCoroutine(ActivateHand(hand));
    }

    IEnumerator ActivateHand(GameObject hand)
    {
        yield return new WaitForSeconds(0.2f);
        hand.SetActive(true);
    }
}