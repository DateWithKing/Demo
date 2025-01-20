using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LP : MonoBehaviour
{
    [SerializeField] private int spinDuration; // 회전 시간
    [SerializeField] private Transform centerPoint; // 중심으로 할 LP판의 중심
    [SerializeField] private float rotationSpeed; // 회전 속도

    [Header("Game Manager")]
    [SerializeField] private PlayingLP playingLP;   

    // Start is called before the first frame update
    void Start()
    {
        rotationSpeed = UnityEngine.Random.Range(36f, 720f); // 회전 속도 초기화
    }

    // Update is called once per frame
    void Update()
    {
        if (playingLP.isPlaying) // 게임이 시작되었을 때만 회전
        {
            transform.RotateAround(centerPoint.position, Vector3.back, rotationSpeed * Time.deltaTime);
        }
    }
}