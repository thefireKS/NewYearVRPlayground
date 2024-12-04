using UnityEngine;

public class ScaleEffect : MonoBehaviour
{
    [Header("Настройки масштаба")]
    [SerializeField] private float minScale = 0.8f;  // Минимальный масштаб
    [SerializeField] private float maxScale = 1.2f;  // Максимальный масштаб
    [SerializeField] private float scaleInDuration = 0.2f; // Время для достижения минимального масштаба
    [SerializeField] private float scaleOutDuration = 0.2f; // Время для достижения максимального масштаба
    [SerializeField] private float returnToNormalDuration = 0.5f; // Время для возвращения к нормальному масштабу

    private Vector3 originalScale;
    private float scaleTime = 0f;
    private bool isScalingIn = false;
    private bool isScalingOut = false;
    private bool isReturningToNormal = false;

    private void Start()
    {
        // Сохраняем исходный масштаб объекта
        originalScale = transform.localScale;
    }

    private void OnCollisionEnter(Collision collision)
    {
        // При столкновении начинаем эффект уменьшения масштаба
        if (!isScalingIn && !isScalingOut && !isReturningToNormal)
        {
            isScalingIn = true; // Начинаем уменьшение
            scaleTime = scaleInDuration;
        }
    }

    private void Update()
    {
        // Эффект изменения масштаба (уменьшение)
        if (isScalingIn)
        {
            if (scaleTime > 0)
            {
                // Плавное уменьшение масштаба до минимального
                float scale = Mathf.Lerp(1f, minScale, 1 - (scaleTime / scaleInDuration));
                transform.localScale = originalScale * scale;
                scaleTime -= Time.deltaTime;
            }
            else
            {
                // Завершаем уменьшение, начинаем увеличивать объект
                isScalingIn = false;
                isScalingOut = true;
                scaleTime = scaleOutDuration;
            }
        }

        // Эффект изменения масштаба (увеличение)
        if (isScalingOut)
        {
            if (scaleTime > 0)
            {
                // Плавное увеличение масштаба до максимального
                float scale = Mathf.Lerp(minScale, maxScale, 1 - (scaleTime / scaleOutDuration));
                transform.localScale = originalScale * scale;
                scaleTime -= Time.deltaTime;
            }
            else
            {
                // Завершаем увеличение, начинаем возвращение к нормальному масштабу
                isScalingOut = false;
                isReturningToNormal = true;
                scaleTime = returnToNormalDuration;
            }
        }

        // Плавное возвращение к нормальному масштабу
        if (isReturningToNormal)
        {
            if (scaleTime > 0)
            {
                // Плавное возвращение масштаба к оригинальному
                float scale = Mathf.Lerp(maxScale, 1f, 1 - (scaleTime / returnToNormalDuration));
                transform.localScale = originalScale * scale;
                scaleTime -= Time.deltaTime;
            }
            else
            {
                // Завершаем возвращение, восстанавливаем оригинальный масштаб
                transform.localScale = originalScale;
                isReturningToNormal = false;
            }
        }
    }
}
