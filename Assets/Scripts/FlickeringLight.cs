using System.Collections;
using UnityEngine;

public class FlickeringLight : MonoBehaviour
{
    private bool isFlickering = false;
    private float timeDelay;

    void Update()
    {
        if (!isFlickering)        
            StartCoroutine(FlickerLight());
    }

    IEnumerator FlickerLight()
    {
        isFlickering = true;
        gameObject.GetComponent<Light>().enabled = false;
        timeDelay = Random.Range(0.3f, 1.0f);

        yield return new WaitForSeconds(timeDelay);

        gameObject.GetComponent<Light>().enabled = true;
        timeDelay = Random.Range(0.3f, 1.0f);
        
        yield return new WaitForSeconds(timeDelay);
        isFlickering = false;
    }
}
