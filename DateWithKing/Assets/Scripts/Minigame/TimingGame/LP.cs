using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LP : MonoBehaviour
{
    [SerializeField] private int spinDuration; // 회전 시간
    [SerializeField] private Transform centerPoint; // 중심으로 할 LP판의 중심

    [Header("Game Manager")]
    [SerializeField] private PlayingLP playingLP;

    private BoxCollider2D boxCollider2D;
    public float rotationSpeed = 36f; // 초당 회전 각도 (10초 동안 360도 회전)
    private bool isSpinning = false; // 회전 여부 확인

    // Start is called before the first frame update
    void Start()
    {
        boxCollider2D = GetComponent<BoxCollider2D>();
        boxCollider2D.isTrigger = true; // 충돌 감지용
    }

    /*void Awake()
    {
        boxCollider2D = GetComponent<BoxCollider2D>();
    }*/

    // Update is called once per frame
    void Update()
    {
        if (playingLP.isPlaying) // 게임이 시작되었을 때만 회전
        {
            transform.RotateAround(centerPoint.position, Vector3.back, rotationSpeed * Time.deltaTime);
        }
    }

    /*
    // 회전 시작하는 메서드 (게임 시작 시 호출)
    public void StartSpinning()
    {
        isSpinning = true;
    }

    // 회전 멈추는 메서드 (게임 오버 시 호출) 
    public void StopSpinning()
    {
        isSpinning = false;
    }

    public void StopGame()
    {
        isSpinning = false;
        StopAllCoroutines();
    }

    public void Activate(int level)
    {
        StartCoroutine();
    }
    */
}
