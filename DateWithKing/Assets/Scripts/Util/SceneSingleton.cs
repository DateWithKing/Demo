using UnityEngine;

/// <summary>
/// 씬이 변경될 시 데이터를 초기화하는 싱글톤<br/>
/// 한 씬 내부에서만 사용하는 게 일반적이나 자유롭게 활용 가능 <br/>
/// 일반 싱글톤과 마찬가지로 상속해서 사용 가능
/// </summary>
/// <typeparam name="T">싱글톤으로 구현할 클래스</typeparam>
public class SceneSingleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T _instance;
    public static T Instance { get { Init(); return _instance; } }

    public void Awake()
    {
        if(Init())
            Destroy(gameObject);
    }

    static bool Init()
    {
        if (_instance == null)
        {
            _instance = FindObjectOfType<T>();
            
            if (_instance == null)
            {
                GameObject obj = new GameObject { name = typeof(T).ToString() + "(Singleton)" };
                _instance = obj.AddComponent<T>();
                ErrorHandler.PrintWarning(Warning.SceneSingleton을_사용하지_않는_씬에서_싱글톤_접근을_함, 
                    "SceneSingleton<T>을 정상적으로 사용하기 위해 오브젝트를 생성했습니다. 의도하지 않은 스크립트에서 일어난 접근이라면 확인 부탁");
                return true;
            }

            return false;
        }

        return true;
    }
}
