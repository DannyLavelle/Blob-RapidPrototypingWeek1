using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropWall : MonoBehaviour
{

    // Start is called before the first frame update
    private void OnCollisionEnter2D(Collision2D collision)
    {
        StartCoroutine(ExecuteAfterDelay(3f));
    }
    private IEnumerator ExecuteAfterDelay(float delayInSeconds)
    {
        yield return new WaitForSeconds(delayInSeconds);

        gameObject.SetActive(false);
    }
}
