using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DissolveController : MonoBehaviour
{
    // Define vars for dissolve
    public MeshRenderer meshRenderer;
    public float dissolveRate = 0.001f;

IEnumerator DissolveProc(float startValue, float targetValue, float startDelay)
    {
        if (meshRenderer.materials?.Length > 0)
        {
            var material = meshRenderer.materials[0];
            material.SetFloat("_dissolveAmount", startValue);
            
            yield return new WaitForSeconds(startDelay);

            float counter = 0;

            while (material.GetFloat("_dissolveAmount") < targetValue-0.1f) //exposed _dissolveAmount property from material's shader (material must use the dissolve shader)
            {
                //Increment dissolveAmount material property
                
                counter = Mathf.Lerp(material.GetFloat("_dissolveAmount"), targetValue, dissolveRate);

                for (int i = 0; i < meshRenderer.materials.Length; i++)
                {
                   material.SetFloat("_dissolveAmount", counter);
                }
                yield return new WaitForEndOfFrame(); 
            }

            gameObject.SetActive(false);
        }
    }

public void DissolveOut(float startDelay)
{
    StopAllCoroutines();
    StartCoroutine(DissolveProc(0, 1, startDelay));
}

public void DissolveIn(float startDelay)
{
    StopAllCoroutines();
    StartCoroutine(DissolveProc(1, 0, startDelay));
}

}
