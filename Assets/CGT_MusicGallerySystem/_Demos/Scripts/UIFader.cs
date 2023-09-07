using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class UIFader : MonoBehaviour
{
    [SerializeField] protected Image toFade;
    [SerializeField] protected float duration = 1f;
    [SerializeField] protected float defaultTargetAlpha = 1;
    [SerializeField] protected bool fadeOnAwake;
    [SerializeField] protected UnityEvent OnFadeStart = new UnityEvent();
    [SerializeField] protected UnityEvent OnFadeEnd = new UnityEvent();

    protected virtual void Awake()
    {
        if (fadeOnAwake)
        {
            Fade(defaultTargetAlpha);
        }
    }
    public virtual void Fade(float targetAlpha)
    {
        if (fadingInProgress)
            return;

        StartCoroutine(FadeProcess(targetAlpha));
    }

    protected bool fadingInProgress;

    protected virtual IEnumerator FadeProcess(float targetAlpha)
    {
        fadingInProgress = true;
        OnFadeStart.Invoke();

        float timePassed = 0;
        float origAlpha = toFade.color.a;

        while (timePassed < duration)
        {
            timePassed += Time.deltaTime;
            Color fadedFurther = toFade.color;
            float progressTowardsTargetAlpha = timePassed / duration;
            fadedFurther.a = Mathf.Lerp(origAlpha, targetAlpha, progressTowardsTargetAlpha);
            toFade.color = fadedFurther;
            yield return null;
        }

        fadingInProgress = false;
        OnFadeEnd.Invoke();
    }
}
