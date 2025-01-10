using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class Roulette : MonoBehaviour
{

    [SerializeField]
    private int spinDuration; // 회전 시간
    [SerializeField]
    private Transform spinningRoulette; // 실제 회전하는 회전판 Transfrom
    [SerializeField]
    private AnimationCurve spinningCurve; // 회전 속도 제어를 위한 그래프
    [SerializeField]
    private RoulettePieceData[] roulettePieceData; // 룰렛에 표시되는 정보 배열

    private float pieceAngle; // 정보 하나가 배치되는 각도

    private int accumulatedWeight; // 가중치 계산을 위한 변수
    private bool isSpinning = false; // 현재 회전중인지
    private int selectedIndex = 0; // 룰렛에서 선택된 아이템

    private float halfPieceAngle;               // 정보 하나가 배치되는 각도의 절반 크기
    private float halfPieceAngleWithPaddings;	// 선의 굵기를 고려한 Padding이 포함된 절반 크기

    // 룰렛의 기준점에 따라 바꿀 것
    private float offset = 22.5f;


    private void Awake()
    {
        pieceAngle = 360 / roulettePieceData.Length;
        halfPieceAngle = pieceAngle * 0.5f;
        halfPieceAngleWithPaddings = halfPieceAngle - (halfPieceAngle * 0.25f);

        
        CalculateWeightsAndIndices();

        
    }

    private void CalculateWeightsAndIndices()
    {
        for (int i = 0; i < roulettePieceData.Length; ++i)
        {
            roulettePieceData[i].index = i;

            // 예외처리. 혹시라도 chance값이 0 이하이면 1로 설정
            if (roulettePieceData[i].chance <= 0)
            {
                roulettePieceData[i].chance = 1;
            }

            accumulatedWeight += roulettePieceData[i].chance;
            roulettePieceData[i].weight = accumulatedWeight;

            
        }
    }

    private int GetRandomIndex()
    {
        int weight = Random.Range(0, accumulatedWeight);

        for (int i = 0; i < roulettePieceData.Length; ++i)
        {
            if (roulettePieceData[i].weight > weight)
            {
                return i;
            }
        }

        return 0;
    }

    public void Spin(UnityAction<RoulettePieceData> action = null)
    {
        if (isSpinning == true) return;

        // 룰렛의 결과 값 선택
        selectedIndex = GetRandomIndex();
        // 선택된 결과의 중심 각도
        float angle = pieceAngle * selectedIndex + offset;
        // 정확히 중심이 아닌 결과 값 범위 안의 임의의 각도 선택
        float leftOffset = (angle - halfPieceAngleWithPaddings) % 360;
        float rightOffset = (angle + halfPieceAngleWithPaddings) % 360;
        float randomAngle = Random.Range(leftOffset, rightOffset);

        // 목표 각도(targetAngle) = 결과 각도 + 360 * 회전 시간 * 회전 속도
        int rotateSpeed = 2;
        float targetAngle = (randomAngle + 360 * spinDuration * rotateSpeed);

        Debug.Log($"SelectedIndex:{selectedIndex}, Angle:{angle}");
        Debug.Log($"left/right/random:{leftOffset}/{rightOffset}/{randomAngle}");
        Debug.Log($"targetAngle:{targetAngle}");

        isSpinning = true;
        StartCoroutine(OnSpin(targetAngle, action));
    }

    private IEnumerator OnSpin(float end, UnityAction<RoulettePieceData> action)
    {
        float current = 0;
        float percent = 0;

        while (percent < 1)
        {
            current += Time.deltaTime;
            percent = current / spinDuration;

            float z = Mathf.Lerp(0, end, spinningCurve.Evaluate(percent));
            spinningRoulette.rotation = Quaternion.Euler(0, 0, z);

            yield return null;
        }

        isSpinning = false;

        if (action != null) action.Invoke(roulettePieceData[selectedIndex]);
    }


    
}
