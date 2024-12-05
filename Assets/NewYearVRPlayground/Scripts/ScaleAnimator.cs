using UnityEngine;
using DG.Tweening;

public class ScaleAnimator : MonoBehaviour
{
    [Header("Настройки анимации")]
    [SerializeField] private Vector3 targetScale = new(1.5f, 0.5f, 2f); // Конечный размер по осям
    [SerializeField] private int loopCount = 2; // Количество циклов
    [SerializeField] private float animationDuration = 1f; // Длительность одного цикла анимации

    private Vector3 _initialScale; // Исходный размер объекта
    private bool _isPlaying;

    private void Awake()
    {
        // Сохраняем начальный масштаб объекта
        _initialScale = transform.localScale;
    }

    private void OnCollisionEnter()
    {
        if(!_isPlaying) PlayScaleAnimation();
    }

    [ContextMenu("PlayScaleAnimation")]
    private void PlayScaleAnimation()
    {
        _isPlaying = true;
        // Создаём последовательность анимаций
        Sequence sequence = DOTween.Sequence();

        // Увеличение до целевого масштаба
        sequence.Append(transform.DOScale(targetScale, animationDuration).SetEase(Ease.InOutSine));

        // Возвращение к начальному масштабу
        sequence.Append(transform.DOScale(_initialScale, animationDuration).SetEase(Ease.InOutSine));

        // Задаём количество циклов
        sequence.SetLoops(loopCount, LoopType.Restart);

        // Запускаем анимацию
        sequence.Play();
        _isPlaying = true;
        sequence.OnComplete(() =>
            {
                _isPlaying = false;
            }
        );
    }
}