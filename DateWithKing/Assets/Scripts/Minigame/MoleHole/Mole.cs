using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using System.Security.Cryptography;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;

public class Mole : MonoBehaviour
{
    [Header(("Graphics"))]
    [SerializeField] private Sprite mole;

    [Header("Game Manager")]
    [SerializeField] private GameMoleHole gameMoleHole;

    // 두더지 숨길 위치
    private Vector2 startPosition = new Vector2(0f, -1.5f);
    private Vector2 endPosition = Vector2.zero;
    // 숨기기/보여주기 매개변수로 할 시간
    private float showDuration = 0.5f;
    private float duration = 1f;
    
    private SpriteRenderer spriteRenderer;
    private BoxCollider2D boxCollider2D;
    private Vector2 boxOffset;
    private Vector2 boxSize;
    private Vector2 boxOffsetHidden;
    private Vector2 boxSizeHidden;
    
    // mole parameters
    private bool hittable = true;
    private int lives;
    private int moleIndex = 0;
    
    // coroutine으로 시작 -> IEnumerator, 시작/종료 위치 전달
    private IEnumerator ShowHide(Vector2 start, Vector2 end)
    {
        // 시작 위치에서 시작
        transform.localPosition = start;

        // 두더지 보여주기
        float elapsed = 0f;
        while (elapsed < showDuration)
        {
            transform.localPosition = Vector2.Lerp(start, end, elapsed / showDuration);
            boxCollider2D.offset = Vector2.Lerp(boxOffsetHidden, boxOffset, elapsed / showDuration);
            boxCollider2D.size = Vector2.Lerp(boxSizeHidden, boxSize, elapsed / showDuration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        // 종료 위치에서 종료
        transform.localPosition = end;
        boxCollider2D.offset = boxOffset;
        boxCollider2D.size = boxSize;

        // duration pass까지 기다림
        yield return new WaitForSeconds(duration);

        // 두더지 숨기기
        elapsed = 0f;
        while (elapsed < showDuration)
        {
            transform.localPosition = Vector2.Lerp(end, start, elapsed / showDuration);
            boxCollider2D.offset = Vector2.Lerp(boxOffset, boxOffsetHidden, elapsed / showDuration);
            boxCollider2D.size = Vector2.Lerp(boxSize, boxSizeHidden, elapsed / showDuration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        // 정확히 시작 위치에서 끝내기
        transform.localPosition = start;
        boxCollider2D.offset = boxOffsetHidden;
        boxCollider2D.size = boxSizeHidden;
        gameMoleHole.RemoveMole(moleIndex);
        
        // 두더지 놓친 경우 -> 아무 변화 없고 hittable 상태만 false로 변경
        if (hittable)
        {
            hittable = false;
        }
    }
    
    public void Hide()
    {
        transform.localPosition = startPosition;
        boxCollider2D.offset = boxOffsetHidden;
        boxCollider2D.size = boxSizeHidden;

        gameMoleHole.RemoveMole(moleIndex);
    }

    private IEnumerator QuickHide()
    {
        yield return new WaitForSeconds(0.1f);
        if (!hittable)
        {
            Hide();
        }
    }
    
    private void OnMouseDown()
    {
        if (hittable)
        {
            gameMoleHole.AddScore(moleIndex);
            StopAllCoroutines();
            StartCoroutine(QuickHide());
            hittable = false;
        }
    }

    private void CreateNext()
    {
        float random = UnityEngine.Random.Range(0f, 1f);
        if (random < 0f)
        {

        }
        else
        {
            lives = 1;
            hittable = true;
            Debug.Log("두더지 생성");
        }
        
    }
    
    private void SetLevel(int level)
    {
        float durationMin = Mathf.Clamp(1 - level * 0.1f, 0.01f, 1f);
        float durationMax = Mathf.Clamp(2 - level * 0.1f, 0.01f, 2f);
        duration = UnityEngine.Random.Range(durationMin, durationMax);
    }
    
    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider2D = GetComponent<BoxCollider2D>();
        boxOffset = boxCollider2D.offset;
        boxSize = boxCollider2D.size;
        boxOffsetHidden = new Vector2(boxOffset.x, -startPosition.y / 2f);
        boxSizeHidden = new Vector2(boxSize.x, 0f);
    }
   
    // 두더지 인덱스 관리
    public void SetIndex(int index)
    {
        moleIndex = index;
    }
    
    // 게임 끝나면 종료하는 메서드
    public void StopGame()
    {
        hittable = false;
        StopAllCoroutines();
    }
    
    public void Activate(int level)
    {
        SetLevel(level);
        CreateNext();
        StartCoroutine(ShowHide(startPosition, endPosition));
    }
}