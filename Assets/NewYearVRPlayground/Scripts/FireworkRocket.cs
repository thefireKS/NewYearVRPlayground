using System.Collections.Generic;
using UnityEngine;

public class FireworkRocket : MonoBehaviour
{
    [Header("Движение ракеты")]
    [SerializeField] private float speed = 5f; // Скорость движения вверх
    [SerializeField] private float deviationAngle = 5f; // Максимальный угол отклонения от оси Y
    [SerializeField] private float lifetime = 3f; // Время жизни ракеты

    [Header("Взрыв")]
    [SerializeField] private List<GameObject> explosionEffect; // Префаб эффекта взрыва
    [SerializeField] private AudioClip explosionSound; // Звук взрыва

    private Rigidbody _rb;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        if (_rb == null)
        {
            Debug.LogError("Для работы ракеты необходим компонент Rigidbody.");
        }

        // Уничтожаем ракету через указанное время
        Destroy(gameObject, lifetime);
    }

    private void Update()
    {
        // Рассчитываем случайное отклонение
        float randomX = Random.Range(-deviationAngle, deviationAngle);
        float randomY = Random.Range(-deviationAngle, deviationAngle);
        transform.Rotate(randomX*Time.deltaTime, randomY*Time.deltaTime, 0);

        // Движение ракеты
        _rb.linearVelocity = transform.forward * speed;
    }

    private void OnDestroy()
    {
        // Взрыв при уничтожении
        if (explosionEffect != null)
        {
            var explosion = explosionEffect[Random.Range(0, explosionEffect.Count)];
            Instantiate(explosion, transform.position, transform.rotation);
        }

        if (explosionSound != null)
        {
            AudioSource.PlayClipAtPoint(explosionSound, transform.position, 1f);
        }
    }
}