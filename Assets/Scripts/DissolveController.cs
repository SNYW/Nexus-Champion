using System;
using System.Collections;
using UnityEngine;

public class DissolveController : MonoBehaviour
{
    // Define vars for dissolve
    public SkinnedMeshRenderer meshRenderer;
    public float dissolveRate = 0.001f;
    public float tolerance;
    private static readonly int DissolveAmount = Shader.PropertyToID("_dissolveAmount");

    IEnumerator DissolveProc(float startValue, float targetValue, float startDelay, Action callback = null, bool activeOnComplete = true)
    {
        if (meshRenderer.materials?.Length > 0)
        {
            var material = meshRenderer.materials[0];
            material.SetFloat(DissolveAmount, startValue);
            
            yield return new WaitForSeconds(startDelay);

            while (Math.Abs(material.GetFloat(DissolveAmount) - targetValue) > tolerance) //exposed _dissolveAmount property from material's shader (material must use the dissolve shader)
            {
                float counter = Mathf.Lerp(material.GetFloat(DissolveAmount), targetValue, dissolveRate);

                for (int i = 0; i < meshRenderer.materials.Length; i++)
                {
                    material.SetFloat(DissolveAmount, counter);
                }
                yield return new WaitForEndOfFrame(); 
            }

            callback?.Invoke();
            gameObject.SetActive(activeOnComplete);
        }
    }
    
    public void DissolveOut(float startDelay, Action callback = null, bool activeOnComplete = true)
    {
        Dissolve(startDelay, -1, 1, callback, activeOnComplete);
    }

    public void DissolveIn(float startDelay, Action callback = null, bool activeOnComplete = true)
    {
        Dissolve(startDelay, 1, -1, callback, activeOnComplete);
    }

    private void Dissolve(float startDelay, float startValue, float endValue, Action callback = null, bool activeOnComplete = true)
    {
        StopAllCoroutines();
        StartCoroutine(DissolveProc(startValue, endValue, startDelay, callback, activeOnComplete));
    }

}
