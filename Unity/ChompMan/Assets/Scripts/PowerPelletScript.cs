using UnityEngine;

public class PowerPelletScript : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<CharacterController>() != null)
        {
            int internalCount = 0;
            foreach (EnemyController enemy in FindObjectsByType<EnemyController>(FindObjectsSortMode.None))
            {
                Destroy(enemy.gameObject);
                internalCount++;
            }
            Destroy(gameObject);
            GameManager.enemiesLeft = 0;
            GameManager.enemiesKilled += internalCount;
            UIManager.Instance.UpdateEnemiesText();
        }
    }
}