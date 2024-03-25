using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Internal.Coroutine
{
    internal class EasyCoroutineInternal
    {
        internal IEnumerator CascadeCoroutine(MonoBehaviour monoBehaviour, List<IEnumerator> coroutines)
        {
            for (int i = 0; i < coroutines.Count; i++)
            {
                yield return monoBehaviour.StartCoroutine(coroutines[i]);
            }
        }
        internal IEnumerator FrameSkip(int frameSkipCount)
        {
            WaitForEndOfFrame wait = new WaitForEndOfFrame(); //call speed optimalization

            for (int i = 0; i < frameSkipCount; i++)
                yield return wait;
        }
        internal IEnumerator ScaledAction(float duration, System.Action<float> action)
        {
            yield return BaseAction(duration, action, () => Time.time);
        }
        internal IEnumerator UnscaledAction(float duration, System.Action<float> action)
        {
            yield return BaseAction(duration, action, () => Time.unscaledTime);
        }
        private IEnumerator BaseAction(float duration, System.Action<float> callback, System.Func<float> timeProvider)
        {
            float corStartTime= timeProvider();
            float t = 0;

            if (Mathf.Approximately(duration,0) || duration < 0)
            {
                callback?.Invoke(1);
                yield break;
            }
            else
            {
                while (!Mathf.Approximately(t, 1))
                {
                    t = Mathf.Min(1, (timeProvider() - corStartTime) / duration);
                    callback?.Invoke(t);
                    yield return 0;
                }
            }
        }

        internal IEnumerator FireImmediately(System.Action instantCallback)
        {
            instantCallback?.Invoke();
            yield break;
        }
        internal IEnumerator WaitScaledTime(float scaledTimeWaitSeconds, System.Action doAfterWaiting)
        {
            yield return new WaitForSeconds(scaledTimeWaitSeconds);
            doAfterWaiting?.Invoke();
        }
        internal IEnumerator WaitUnscaledTime(float realTimeWaitSeconds, System.Action doAfterWaiting)
        {
            yield return new WaitForSecondsRealtime(realTimeWaitSeconds);
            doAfterWaiting?.Invoke();
        }
    }
}
