using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakingScreen : MonoBehaviour
{
    [SerializeField] private RectTransform rightHand; // 첫 번째 오브젝트
    [SerializeField] private RectTransform leftHand; // 두 번째 오브젝트

    [SerializeField] private GameObject rightFoldHand;
    [SerializeField] private GameObject leftFoldHand;

    [SerializeField] private Vector2 startPos1; // 첫 번째 오브젝트 시작 위치
    [SerializeField] private Vector2 endPos1; // 첫 번째 오브젝트 목표 위치
    [SerializeField] private float speed = 200f; // 이동 속도 (초당 픽셀)

    private Vector2 direction; // 이동 방향
    private bool isMoving = false;

    [SerializeField] private Transform targetObject; // 흔들릴 오브젝트
    [SerializeField] private float duration = 0.5f; // 흔들리는 시간
    [SerializeField] private float magnitude = 0.1f; // 흔들림 강도
    [SerializeField] private float shakingSpeed = 20f; // 흔들림 속도

    void Start()
    {
        if (rightHand != null && leftHand != null)
        {
            // 첫 번째 오브젝트 시작 위치 설정
            rightHand.anchoredPosition = startPos1;
            // 두 번째 오브젝트는 X축 대칭 위치에서 시작
            leftHand.anchoredPosition = GetMirroredPosition(startPos1);

            // 이동 방향 설정
            direction = (endPos1 - startPos1).normalized;
            isMoving = true;
        }
    }

    void Update()
    {
        if (isMoving && rightHand != null && leftHand != null)
        {
            float step = speed * Time.deltaTime;

            // 첫 번째 오브젝트 이동
            rightHand.anchoredPosition += direction * step;
            // 두 번째 오브젝트는 X축 대칭 방향으로 이동
            leftHand.anchoredPosition += GetMirroredDirection(direction) * step;

            // 목표 위치 도달 체크
            if (Vector2.Distance(rightHand.anchoredPosition, endPos1) <= step)
            {
                rightHand.anchoredPosition = endPos1;
                leftHand.anchoredPosition = GetMirroredPosition(endPos1);
                isMoving = false;
                StartCoroutine("ChangeHand");
                StartCoroutine("StartShake");
            }
            
        }
    }

    public IEnumerator ChangeHand()
    {
        yield return new WaitForSeconds(1f);
        rightHand.gameObject.SetActive(false);
        leftHand.gameObject.SetActive(false);
        rightFoldHand.SetActive(true);
        leftFoldHand.SetActive(true);
    }
    
    public IEnumerator StartShake()
    {
        yield return new WaitForSeconds(3f);
        StartCoroutine("Shake");
    }

    // X축 기준 대칭 위치 반환
    private Vector2 GetMirroredPosition(Vector2 pos)
    {
        return new Vector2(-pos.x, pos.y);
    }

    // X축 기준 대칭 이동 방향 반환
    private Vector2 GetMirroredDirection(Vector2 dir)
    {
        return new Vector2(-dir.x, dir.y);
    }

    private IEnumerator Shake()
    {
        Vector3 originalPosition = targetObject.localPosition;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            float x = Mathf.Sin(Time.time * shakingSpeed) * magnitude * 0.6f + Random.Range(-0.7f, 0.7f) * magnitude;
            float y = Random.Range(-0.4f, 0.4f) * magnitude;
            Vector3 newPosition = originalPosition + new Vector3(x, y, 0);
            targetObject.localPosition = Vector3.Lerp(targetObject.localPosition, newPosition, Time.deltaTime * shakingSpeed);

            elapsed += Time.deltaTime;
            yield return null;
        }

        targetObject.localPosition = originalPosition;
    }
}
