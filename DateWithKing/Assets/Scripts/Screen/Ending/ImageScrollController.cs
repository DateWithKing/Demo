using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ImageScrollController : MonoBehaviour
{
    [Header("이미지 설정")]
    public RectTransform imageRectTransform; // 스크롤할 이미지의 RectTransform
    public Canvas parentCanvas; // 부모 캔버스 (화면 크기 계산용)
    
    [Header("스크롤 설정")]
    public float normalScrollSpeed = 100f; // 기본 스크롤 속도 (픽셀/초)
    public float fastScrollMultiplier = 2f; // 빠른 스크롤 배수
    
    [Header("자동 계산 설정")]
    public bool autoCalculateScrollRange = true; // 이미지 크기에 따라 자동 계산
    public float manualStartY = 0f; // 수동 설정시 시작 위치
    public float manualEndY = 1000f; // 수동 설정시 끝 위치
    
    private float startY; // 실제 시작 위치
    private float endY; // 실제 끝 위치
    private float currentScrollSpeed;
    private bool isScrolling = true;
    private bool isFastScrolling = false;
    
    void Start()
    {
        InitializeScrollRange();
        SetImageToStartPosition();
        currentScrollSpeed = normalScrollSpeed;
    }
    
    void Update()
    {
        CheckInput();
        
        if (isScrolling && imageRectTransform != null)
        {
            ScrollImage();
            CheckScrollComplete();
        }
    }
    
    void InitializeScrollRange()
    {
        if (autoCalculateScrollRange && imageRectTransform != null)
        {
            // 캔버스 크기 가져오기
            RectTransform canvasRect = parentCanvas.GetComponent<RectTransform>();
            float canvasHeight = canvasRect.rect.height;
            float imageHeight = imageRectTransform.rect.height;
            
            // 이미지가 화면 아래에서 시작해서 이미지 하단이 화면 하단과 맞을 때까지의 범위 계산
            startY = -(canvasHeight / 2 + imageHeight / 2); // 화면 아래 (이미지 완전히 숨김)
            endY = imageHeight / 2 - canvasHeight / 2; // 이미지 하단이 화면 하단과 맞는 위치
            
            Debug.Log($"자동 계산된 스크롤 범위: {startY} ~ {endY}");
        }
        else
        {
            // 수동 설정값 사용
            startY = manualStartY;
            endY = manualEndY;
            Debug.Log($"수동 설정된 스크롤 범위: {startY} ~ {endY}");
        }
    }
    
    void SetImageToStartPosition()
    {
        if (imageRectTransform != null)
        {
            Vector2 startPosition = imageRectTransform.anchoredPosition;
            startPosition.y = startY;
            imageRectTransform.anchoredPosition = startPosition;
        }
    }
    
    void CheckInput()
    {
        // 마우스 좌클릭 또는 스페이스바 입력 확인
        bool inputPressed = Input.GetMouseButton(0) || Input.GetKey(KeyCode.Space);
        
        if (inputPressed && !isFastScrolling)
        {
            isFastScrolling = true;
            currentScrollSpeed = normalScrollSpeed * fastScrollMultiplier;
            Debug.Log("빠른 스크롤 시작");
        }
        else if (!inputPressed && isFastScrolling)
        {
            isFastScrolling = false;
            currentScrollSpeed = normalScrollSpeed;
            Debug.Log("일반 스크롤로 복귀");
        }
    }
    
    void ScrollImage()
    {
        Vector2 currentPosition = imageRectTransform.anchoredPosition;
        currentPosition.y += currentScrollSpeed * Time.deltaTime;
        imageRectTransform.anchoredPosition = currentPosition;
    }
    
    void CheckScrollComplete()
    {
        if (imageRectTransform.anchoredPosition.y >= endY)
        {
            OnScrollComplete();
        }
    }
    
    void OnScrollComplete()
    {
        isScrolling = false;
        Debug.Log("이미지 스크롤 완료!");

        StartCoroutine(WaitForNext());

        // 스크롤 완료 후 실행할 코드 추가
        // 예: 다음 씬으로 이동, 메뉴로 돌아가기 등
        // SceneManager.LoadScene("MainMenu");
    }

    private IEnumerator WaitForNext()
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("Lobby");
    }
    
    // 공개 메서드들 - 외부에서 호출 가능
    public void RestartScroll()
    {
        SetImageToStartPosition();
        isScrolling = true;
        currentScrollSpeed = normalScrollSpeed;
        isFastScrolling = false;
        Debug.Log("이미지 스크롤 재시작");
    }
    
    public void PauseScroll()
    {
        isScrolling = false;
        Debug.Log("이미지 스크롤 일시정지");
    }
    
    public void ResumeScroll()
    {
        isScrolling = true;
        Debug.Log("이미지 스크롤 재개");
    }
    
    public void TogglePause()
    {
        isScrolling = !isScrolling;
        Debug.Log(isScrolling ? "이미지 스크롤 재개" : "이미지 스크롤 일시정지");
    }
    
    public void SetScrollSpeed(float newSpeed)
    {
        normalScrollSpeed = newSpeed;
        if (!isFastScrolling)
        {
            currentScrollSpeed = normalScrollSpeed;
        }
    }
    
    // 스크롤 진행률 반환 (0~1)
    public float GetScrollProgress()
    {
        if (imageRectTransform == null) return 0f;
        
        float currentY = imageRectTransform.anchoredPosition.y;
        float totalDistance = endY - startY;
        float currentDistance = currentY - startY;
        
        return Mathf.Clamp01(currentDistance / totalDistance);
    }
    
    // 현재 스크롤 상태 정보
    public bool IsScrolling => isScrolling;
    public bool IsFastScrolling => isFastScrolling;
    public float CurrentSpeed => currentScrollSpeed;
}