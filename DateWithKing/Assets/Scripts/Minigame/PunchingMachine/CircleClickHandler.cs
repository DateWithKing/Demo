using UnityEngine;

public class CircleClickHandler : MonoBehaviour
{
    private PunchingManager gameManager;

    void Start()
    {
        gameManager = FindObjectOfType<PunchingManager>();
    }

    void OnMouseDown()
    {
        if (gameManager != null)
        {
            gameManager.OnCircleClick();
        }
    }
}