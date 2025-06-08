using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    // 전역에서 접근 가능한 싱글톤 인스턴스
    public static T Instance { get; private set; }

    // 싱글톤 인스턴스를 설정하고 중복 오브젝트가 존재하면 파괴
    protected virtual void Awake()
    {
        if (Instance != null && Instance != this) Destroy(gameObject);
        else Instance = this as T;
    }
}
