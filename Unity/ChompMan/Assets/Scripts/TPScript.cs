using System.Collections;
using UnityEngine;

public class TPScript : MonoBehaviour
{
    public Transform tpEnd;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<CharacterController>() != null) //Move between portals
        {
            tpEnd.GetComponentInParent<BoxCollider>().enabled = false;
            other.transform.SetPositionAndRotation(tpEnd.position, Quaternion.identity);
            other.transform.LookAt(transform.position);
            StartCoroutine(ReactivateTP());
        }
    }

    IEnumerator ReactivateTP()
    {
        yield return new WaitForSeconds(0.2f);
        tpEnd.GetComponentInParent<BoxCollider>().enabled = true;
    }
}
