using UnityEngine;
using UnityEngine.UI; // UI 게이지를 위해 필요

public class PowerGaugeController : MonoBehaviour
{
    public Slider gaugeSlider; // UI 슬라이더 (게이지)
    public float maxPower = 15f; // 최대 힘
    public float powerUpSpeed = 10f;
    private float power = 0f;
    private bool isCharging = false;
    private Ball ball;

    void Start(){
        BasketballManager.Instance.StartGame += ()=>{gameObject.SetActive(true);};
        BasketballManager.Instance.EndGame += ()=>{gameObject.SetActive(false);};
        gameObject.SetActive(false);
    }

    void Update()
    {
        if (isCharging)
        {
            power += Time.deltaTime * powerUpSpeed; // 마우스 누르고 있으면 게이지 증가
            power = Mathf.Clamp(power, 0, maxPower); // 최대값 제한
            gaugeSlider.value = power / maxPower; // UI 업데이트
        }
    }

    void OnMouseDown() // 클릭하면 게이지 충전 시작
    {
        ball = BasketballManager.Instance.GetBall();
        if (ball == null) return;
        ball.SetBall();
        isCharging = true;
    }
    void OnMouseUp()
    {
        if(!isCharging) return;
        isCharging = false;
        LaunchBall(power); // 공 발사
        power = 0f; // 초기화
        gaugeSlider.value = 0f; 
    }

    void LaunchBall(float power)
    {
        ball.LaunchBall(power);
    }
}
