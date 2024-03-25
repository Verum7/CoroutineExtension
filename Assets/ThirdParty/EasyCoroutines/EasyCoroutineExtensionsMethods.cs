using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Internal.Coroutine;

public static class CoroutineExtensionsMethods
{
    #region Coroutine Extensions
    public static Coroutine Cor_WaitUnscaledTime(this MonoBehaviour monoBehaviour, float waitForSeconds, System.Action afterWaiting)
    {
        return monoBehaviour.StartCoroutine(new EasyCoroutineInternal().WaitUnscaledTime(waitForSeconds, afterWaiting));
    }
    public static Coroutine Cor_WaitScaledTime(this MonoBehaviour monoBehaviour, float waitForSeconds, System.Action afterWaiting)
    {
        return monoBehaviour.StartCoroutine(new EasyCoroutineInternal().WaitScaledTime(waitForSeconds, afterWaiting));
    }

    public static Coroutine Cor_SkipFrames(this MonoBehaviour monoBehaviour, int countFrame, System.Action afterWaiting)
    {
        List<IEnumerator> coroutinesList = new();
        coroutinesList.Add(new EasyCoroutineInternal().FrameSkip(countFrame));
        coroutinesList.Add(new EasyCoroutineInternal().FireImmediately(afterWaiting));

        return monoBehaviour.StartCoroutine(new EasyCoroutineInternal().CascadeCoroutine(monoBehaviour, coroutinesList));
    }

    public static Coroutine Cor_UnscaledAction(this MonoBehaviour monoBehaviour,
            float duration1, System.Action<float> corAction1,
            float duration2 = 0, System.Action<float> corAction2 = null,
            float duration3 = 0, System.Action<float> corAction3 = null,
            float duration4 = 0, System.Action<float> corAction4 = null
            )
    {
        List<IEnumerator> coroutinesList = new();

        coroutinesList.Add(new EasyCoroutineInternal().UnscaledAction(duration1, corAction1));

        if (corAction2 != null)
            coroutinesList.Add(new EasyCoroutineInternal().UnscaledAction(duration2, corAction2));
        if (corAction3 != null)
            coroutinesList.Add(new EasyCoroutineInternal().UnscaledAction(duration3, corAction3));
        if (corAction4 != null)
            coroutinesList.Add(new EasyCoroutineInternal().UnscaledAction(duration4, corAction4));

        return monoBehaviour.StartCoroutine(new EasyCoroutineInternal().CascadeCoroutine(monoBehaviour, coroutinesList));
    }

    public static Coroutine Cor_ScaledAction(this MonoBehaviour monoBehaviour,
            float duration1, System.Action<float> corAction1,
            float duration2 = 0, System.Action<float> corAction2 = null,
            float duration3 = 0, System.Action<float> corAction3 = null,
            float duration4 = 0, System.Action<float> corAction4 = null
            )
    {
        List<IEnumerator> coroutinesList = new();

        coroutinesList.Add(new EasyCoroutineInternal().ScaledAction(duration1, corAction1));

        if (corAction2 != null)
            coroutinesList.Add(new EasyCoroutineInternal().ScaledAction(duration2, corAction2));
        if (corAction3 != null)
            coroutinesList.Add(new EasyCoroutineInternal().ScaledAction(duration3, corAction3));
        if (corAction4 != null)
            coroutinesList.Add(new EasyCoroutineInternal().ScaledAction(duration4, corAction4));

        return monoBehaviour.StartCoroutine(new EasyCoroutineInternal().CascadeCoroutine(monoBehaviour, coroutinesList));
    }

}
#endregion
