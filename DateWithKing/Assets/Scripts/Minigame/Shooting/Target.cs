using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Target : MonoBehaviour
{
    float speed;
    readonly Vector2 LEFT_LIMIT = new Vector2(-4.5f, 0);
    readonly Vector2 RIGHT_LIMIT = new Vector2(4.5f, 0);
    float t;
    bool movingForward = true;
    void Start()
    {
        ShootingGameManager.Instance.StartGame += () => {gameObject.SetActive(true);};
        ShootingGameManager.Instance.EndGame += () => {this.enabled = false;};
        t = Random.Range(0,1);
        speed = Random.Range(0.5f,2f);
        gameObject.SetActive(false);
    }

    void Update()
    {
        if (movingForward)
        {
            t += Time.deltaTime * speed;
            if (t >= 1f)
            {
                t = 1f;
                movingForward = false;
            }
        }
        else
        {
            t -= Time.deltaTime * speed;
            if (t <= 0f)
            {
                t = 0f;
                movingForward = true;
            }
        }
        
       transform.localPosition = Vector2.Lerp(LEFT_LIMIT, RIGHT_LIMIT, t);
    }
    
    private void OnMouseDown() {
        gameObject.SetActive(false);
        ShootingGameManager.Instance.GetScore(gameObject.tag);
    }
}
