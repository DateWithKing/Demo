using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private Rigidbody2D ball;
    private Vector3 launchPoint = new Vector3(-3.4f, -2.47f, 0); // 발사 위치
    public float launchAngle; // 발사 각도
    [HideInInspector] public bool isReady;

    void Start() {
        ball = GetComponent<Rigidbody2D>();
        ball.bodyType = RigidbodyType2D.Dynamic;
        BasketballManager.Instance.EndGame += ()=>{ball.bodyType = RigidbodyType2D.Static;};
    }

    public void SetBall()
    {
        isReady = false;
        transform.position = launchPoint;
        ball.bodyType = RigidbodyType2D.Static;
    }
    public void LaunchBall(float power)
    {   
        // 힘 계산 (각도에 따라 분해)
        float radian = launchAngle * Mathf.Deg2Rad;
        Vector3 force = new Vector3(Mathf.Cos(radian), Mathf.Sin(radian), 0) * power;

        ball.bodyType = RigidbodyType2D.Dynamic;
        ball.AddForce(force, ForceMode2D.Impulse); // 힘을 줘서 발사
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.gameObject.tag == "ScoreTrigger") {
            BasketballManager.Instance.ScoreUp();
        }
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.tag == "Floor") {
            isReady = true;
        }
    }
}

