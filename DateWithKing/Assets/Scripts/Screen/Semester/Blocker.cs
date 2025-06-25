using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blocker : MonoBehaviour
{
    public void Start()
    {
        gameObject.SetActive(false);
    }
    public void Block()
    {
        gameObject.SetActive(true);
        StartCoroutine(RidBlocker());
    }

    private IEnumerator RidBlocker()
    {
        yield return new WaitForSeconds(1f);
        gameObject.SetActive(false);
    }
}
