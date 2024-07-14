using UnityEngine;
using DG.Tweening;

public class MonoTweenHelper : MonoBehaviour
{
    [SerializeField] private float duration = 1f;
    private Transform _target;

    public void SelectTarget(Transform target) 
    {
        _target = target;
    }

    public void SelectDuration(float duration) => this.duration = duration;

    public void RotateTo(Transform rotatePoint) 
    {
        if (TargetAvailable() == false) 
        {
            Debug.LogError("Can't rotate to by null target. " + rotatePoint);
            return;
        }

        _target.DORotate(rotatePoint.eulerAngles, duration)
            .SetEase(Ease.Linear);
    }

    public void MoveTo(Transform positionPoint) 
    {
        if (TargetAvailable() == false) 
        {
            Debug.LogError("Can't move to by null target. " + positionPoint);
            return;
        }

        _target.DOMove(positionPoint.position, duration)
               .SetEase(Ease.Linear);
    }

    public void ScaleTo(Transform scalePoint) 
    {
        if (TargetAvailable() == false) 
        {
            Debug.LogError("Can't scale to by null target. " + scalePoint);
            return;
        }

        _target.DOScale(scalePoint.localScale, duration)
               .SetEase(Ease.Linear);
    }

    private bool TargetAvailable() => _target != null;
}