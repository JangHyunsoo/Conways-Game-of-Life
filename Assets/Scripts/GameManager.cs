using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance = null;

    public static GameManager Instance
    {
        get
        {
            // 인스턴스가 없는 경우에 접근하려 하면 인스턴스를 할당해준다.
            if (!_instance)
            {
                _instance = FindObjectOfType(typeof(GameManager)) as GameManager;

                if (_instance == null)
                    Debug.Log("no Singleton obj");
            }
            return _instance;
        }
    }

    [SerializeField] public float TimeScale = 1f;
    [SerializeField] public float DeltaTime = 0f;

    private void Start()
    {
        RuleManager.Instance.Init();
        BuildManager.Instance.Init();
    }

    private void Update()
    {
        DeltaTime = Time.deltaTime * TimeScale;
        RuleManager.Instance.OnUpdate();
    }
}
