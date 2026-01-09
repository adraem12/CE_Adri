using UnityEngine;

public class CoinScript : MonoBehaviour
{
    //Variables
    public float maxDistance = 80;
    public float minDistance = 10;
    Transform shipTransform;

    private void Awake()
    {
        shipTransform = GameManager.Instance.ship.transform;        
    }

    void Start()
    {
        transform.eulerAngles = new Vector3(0, Random.Range(0f, 360f), 0); //Randomiza la rotación inicial
    }

    void Update()
    {
        //Rotación respecto a la distancia con la nave
        float inverseLerp = Mathf.InverseLerp(maxDistance, minDistance, Vector3.Distance(transform.position, shipTransform.position)) * 10;
        transform.Rotate(0, 100f * Time.deltaTime * (inverseLerp + 1), 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Ship>())
        {
            GameManager.Instance.AddCoin();
            Destroy(gameObject);
        }
    }
}