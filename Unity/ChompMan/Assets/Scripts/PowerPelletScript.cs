using UnityEngine;

public class PowerPelletScript : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<CharacterController>() != null) //Kill all enemies
        {
            int internalCount = 0;
            ParticleSystem.MainModule particleSystem;
            foreach (EnemyController enemy in FindObjectsByType<EnemyController>(FindObjectsSortMode.None))
            {
                Destroy(enemy.gameObject);
                particleSystem = Instantiate(GameManager.instance.destructionParticleSystem, enemy.transform.position, Quaternion.identity).GetComponent<ParticleSystem>().main;
                particleSystem.startColor = new Color(0, 1, 1);
                internalCount++;
            }
            GameManager.enemiesLeft = 0;
            GameManager.enemiesKilled += internalCount;
            UIManager.Instance.UpdateEnemiesText();
            SoundManager.instance.SetEffects(3);
            particleSystem = Instantiate(GameManager.instance.destructionParticleSystem, transform.position, Quaternion.identity).GetComponent<ParticleSystem>().main;
            particleSystem.startColor = new Color(1, 0.92f, 0.016f);
            Destroy(gameObject);
        }
    }
}