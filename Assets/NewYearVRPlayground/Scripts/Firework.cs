using UnityEngine;

public class Firework : MonoBehaviour
{
    [SerializeField] private GameObject rocketPrefab;
    [SerializeField] private Transform rocketSpawnPoint;

    public void SpawnRocket()
    {
        Instantiate(rocketPrefab, rocketSpawnPoint.position, rocketSpawnPoint.rotation);
        Destroy(gameObject);
    }

}
