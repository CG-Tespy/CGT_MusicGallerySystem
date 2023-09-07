using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class AudioFader : MonoBehaviour
{
    [SerializeField] protected AudioSource toFade;
    [SerializeField] protected float duration = 1f;
    [SerializeField] protected float defaultTargetVol = 1;
    [SerializeField] protected bool fadeOnAwake;
    [SerializeField] protected UnityEvent OnFadeStart = new UnityEvent();
    [SerializeField] protected UnityEvent OnFadeEnd = new UnityEvent();

    protected virtual void Awake()
    {
        if (fadeOnAwake)
        {
            Fade(defaultTargetVol);
        }
    }
    public virtual void Fade(float targetVol)
    {
        if (fadingInProgress)
            return;

        StartCoroutine(FadeProcess(targetVol));
    }

    protected bool fadingInProgress;

    protected virtual IEnumerator FadeProcess(float targetVol)
    {
        fadingInProgress = true;
        OnFadeStart.Invoke();

        float timePassed = 0;
        float origVol = toFade.volume;

        while (timePassed < duration)
        {
            timePassed += Time.deltaTime;
            float progressTowardsTargetVol = timePassed / duration;
            float newVol = Mathf.Lerp(origVol, targetVol, progressTowardsTargetVol);

            toFade.volume = newVol;
            yield return null;
        }

        fadingInProgress = false;
        OnFadeEnd.Invoke();
    }
}
