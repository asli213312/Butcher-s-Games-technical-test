using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public static class AnimTweener
{
    public static void RotateTo(this Transform target, Transform rotatePoint, float duration, Action onComplete = null) 
    {
        target.DORotate(rotatePoint.eulerAngles, duration)
            .SetEase(Ease.Linear)
            .OnComplete(() => onComplete?.Invoke());
    }

    public static void MoveTo(this Transform target, RectTransform positionPoint, float duration, Action onComplete = null) 
    {
        target.DOMove(positionPoint.anchoredPosition, duration)
               .SetEase(Ease.Linear)
               .OnComplete(() => onComplete?.Invoke());
    }

    public static void MoveTo(this Transform target, Transform positionPoint, float duration, Action onComplete = null) 
    {
        target.DOMove(positionPoint.position, duration)
               .SetEase(Ease.Linear)
               .OnComplete(() => onComplete?.Invoke());
    }

    public static void ScaleTo(this Transform target, Transform scalePoint, float duration, Action onComplete = null) 
    {
        target.DOScale(scalePoint.localScale, duration)
               .SetEase(Ease.Linear)
               .OnComplete(() => onComplete?.Invoke());
    }

	public static void EaseOutBounce(this Transform target, Vector3 endPos, float duration, Action onComplete = null) 
    {
        target.DOMove(endPos, duration)
              .SetEase(Ease.OutBounce)
              .OnComplete(() => onComplete?.Invoke());
    }

    public static void Fade(this TextMeshProUGUI tmpText, float targetAlpha, float duration, Action onComplete = null)
    {
        tmpText.DOFade(targetAlpha, duration)
               .OnComplete(() => onComplete?.Invoke());
    }

    public static void Fade(this Image image, float targetAlpha, float duration, Action onComplete = null)
    {
        image.DOFade(targetAlpha, duration)
             .OnComplete(() => onComplete?.Invoke());
    }

    public static void Fade(this Graphic graphic, float targetAlpha, float duration, Action onComplete = null)
    {
        Color color = graphic.color;
        graphic.DOFade(targetAlpha, duration)
               .OnComplete(() => onComplete?.Invoke());
    }

    public static void Fade(this CanvasGroup canvasGroup, float targetAlpha, Action onComplete = null)
    {
        canvasGroup.DOFade(targetAlpha, 0)
                   .OnComplete(() => onComplete?.Invoke());
    }

    public static void FadeIn(this CanvasGroup canvasGroup, Action onComplete = null)
    {
        canvasGroup.DOFade(1f, 0)
                   .OnComplete(() => onComplete?.Invoke());
    }

    public static void FadeOut(this CanvasGroup canvasGroup, Action onComplete = null)
    {
        canvasGroup.DOFade(0f, 0)
                   .OnComplete(() => onComplete?.Invoke());
    }
}
