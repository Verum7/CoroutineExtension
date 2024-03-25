using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Internal.Coroutine.Sample
{
    public class EasyCoroutinesSample : MonoBehaviour
    {
        [SerializeField]
        Transform gcube;

        /* 
        * TO STOP COROUTINES (Similar to Stopping regular Coroutines):
        * You may use coroutine objects like this:
        * 
        * Example:
        * Coroutine coroutine = this.Cor_WaitUnscaledTime(1.5f, () => Debug.Log("Executed after waiting for 1.5 seconds"));
        * StopCoroutine(coroutine);
        * 
        * Using StopAllCoroutines(); will also affect coroutines created by this extension in a similar way.
        * 
        */

        #region Wait for realtime seconds - Demonstrating waiting for a specified amount of unscaled time
        // Using Easy Coroutines extension
        public void ExtWaitForSeconds()
        {
            //use this.Cor_WaitScaledTime for scaled time action
            this.Cor_WaitUnscaledTime(1.5f, () => Debug.Log("Ext After waiting for 1.5 seconds"));
        }
        // Equivalent code without the extension
        public void NoExtoWaitForSeconds()
        {
            StartCoroutine(NoExtWaitUnscaledTime());
        }
        IEnumerator NoExtWaitUnscaledTime()
        {
            yield return new WaitForSecondsRealtime(1.5f);
            Debug.Log("NonExt After waiting for 1.5 seconds");
        }
        #endregion


        #region Coroutine realtime action1
        // Using Easy Coroutines extension
        public void ExtUnscaledAction()
        {
            Vector3 startPosition = Vector3.up * -1;
            Vector3 endPosition = Vector3.up * 3;
            float duration = 4;

            gcube.transform.position = startPosition;


            // When you use the Easy Coroutines extension's this.Cor_UnscaledAction or this.Cor_ScaledAction,
            // you are guaranteed that the 't' value is clamped between 0 and 1.
            //use this.Cor_ScaledAction for scaled time action
            this.Cor_UnscaledAction(duration, t =>
            {
                gcube.transform.position = Vector3.Lerp(startPosition, endPosition, t);
                Debug.Log("ExtUnscaledAction t value: " + t);
            });
        }
        // Equivalent code without the extension
        public void NoExtUnscaledAction()
        {
            StartCoroutine(NoExtUnscaledActionCor());
        }
        IEnumerator NoExtUnscaledActionCor()
        {
            Vector3 startPosition = Vector3.up * -1;
            Vector3 endPosition = Vector3.up * 3;
            float duration = 4;
            gcube.transform.position = startPosition;

            float startTime = Time.realtimeSinceStartup;
            float t = 0;

            while (t < 1)
            {
                t = (Time.realtimeSinceStartup - startTime) / duration;
                gcube.transform.position = Vector3.Lerp(startPosition, endPosition, t);
                Debug.Log("NoExtUnscaledActionCor t value: " + t);
                yield return 0;
            }
        }
        #endregion

        #region Coroutine realtime action2
        // Using Easy Coroutines extension
        public void ExtUnscaledAction2()
        {
            Vector3 startPosition = Vector3.up * -1;
            Vector3 firstMovePosition = Vector3.up * 3;
            Vector3 secondMovePosition = new Vector3(3, -3, 0);
            Vector3 thirdMovePosition = Vector3.up * -1;
            Vector3 endTeleportPosition = new Vector3(3, 3, 0);

            float durationEachStage = 2;

            gcube.transform.position = startPosition;

            // When you use the Easy Coroutines extension's this.Cor_UnscaledAction or this.Cor_ScaledAction,
            // you are guaranteed that the 't' value is clamped between 0 and 1.
            //use this.Cor_ScaledAction for scaled time action
            this.Cor_UnscaledAction(
                durationEachStage, t => gcube.transform.position = Vector3.Lerp(startPosition, firstMovePosition, t),
                durationEachStage, t => gcube.transform.position = Vector3.Lerp(firstMovePosition, secondMovePosition, t),
                durationEachStage, t => gcube.transform.position = Vector3.Lerp(secondMovePosition, thirdMovePosition, t),
                // Setting "duration" to 0 causes the action to be executed immediately
                0, _ => gcube.transform.position = endTeleportPosition
           );
        }
        // Equivalent code without the extension
        public void NoExtUnscaledAction2()
        {
            StartCoroutine(NoExtUnscaledActionCor2());
        }
        IEnumerator NoExtUnscaledActionCor2()
        {
            Vector3 startPosition = Vector3.up * -1;
            Vector3 firstMovePosition = Vector3.up * 3;
            Vector3 secondMovePosition = new Vector3(3, -3, 0);
            Vector3 thirdMovePosition = Vector3.up * -1;
            Vector3 endTeleportPosition = new Vector3(3, 3, 0); ;

            float durationEachStage = 2;

            gcube.transform.position = startPosition;

            float startTime = Time.realtimeSinceStartup;
            float t = 0;

            while (t < 1)
            {
                t = (Time.realtimeSinceStartup - startTime) / durationEachStage;
                gcube.transform.position = Vector3.Lerp(startPosition, firstMovePosition, t);
                yield return 0;
            }

            startTime = Time.realtimeSinceStartup;
            t = 0;
            while (t < 1)
            {
                t = (Time.realtimeSinceStartup - startTime) / durationEachStage;
                gcube.transform.position = Vector3.Lerp(firstMovePosition, secondMovePosition, t);
                yield return 0;
            }

            startTime = Time.realtimeSinceStartup;
            t = 0;
            while (t < 1)
            {
                t = (Time.realtimeSinceStartup - startTime) / durationEachStage;
                gcube.transform.position = Vector3.Lerp(secondMovePosition, thirdMovePosition, t);
                yield return 0;
            }

            gcube.transform.position = endTeleportPosition;
        }
        #endregion

        public void ExtCoroutineAction3()
        {
            Vector3 startPosition = Vector3.up * -1;
            Vector3 firstMovePosition = Vector3.up * 3;
            Vector3 secondStartPosition = new Vector3(-4, -1, 0);
            Vector3 secondMovePosition = new Vector3(-4, 3, 0);


            gcube.transform.position = startPosition;
            this.Cor_UnscaledAction(
                3, t => gcube.transform.position = Vector3.Lerp(startPosition, firstMovePosition, t), //first move gcube with duration 4 seconds
                                                                                                      //0 in duration means the action will be called immediatelly and only once
                0, _ => gcube.transform.position = secondStartPosition, //teleport gcube to second start position,
                2, _ => { }, //just wait 2 seconds,
                1, t => gcube.transform.position = Vector3.Lerp(secondStartPosition, secondMovePosition, t) //move gcube with duration 2 seconds
           );
        }

    }
}