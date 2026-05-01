using System;
using UnityEngine;

public class BallScript : MonoBehaviour
{
    public EventHandler<BallScriptEventArgs> OnHit;
    public EventHandler OnPick;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision != null && collision.gameObject.CompareTag("Room"))
            CallOnHit(false);
        else if (collision != null && collision.gameObject.CompareTag("Box"))
        {
            CallOnHit(false);
            collision.gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other != null && other.gameObject.CompareTag("Room"))
            CallOnHit(true);
    }

    public void CallOnHit(bool score)
    {
        OnHit?.Invoke(this, new BallScriptEventArgs(this, score));
        Destroy(gameObject);
    }

    public void OnPickBall()
    {
        OnPick?.Invoke(this, EventArgs.Empty);
    }
}

public class BallScriptEventArgs: EventArgs
{
    public BallScript NewBallScript;
    public bool ScoredBall;

    public BallScriptEventArgs(BallScript newBallScript, bool scoredBall)
    {
        NewBallScript = newBallScript;
        ScoredBall = scoredBall;
    }
}