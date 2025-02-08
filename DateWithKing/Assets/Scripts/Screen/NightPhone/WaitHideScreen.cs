using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitHideScreen : MonoBehaviour
{
    public void WaitScreen()
    {
        if (gameObject.activeSelf)
            StartCoroutine("WaitHide");
    }

    public IEnumerator WaitHide()
    {
        yield return new WaitForSeconds(1.0f);
        gameObject.SetActive(false);
    }
}
