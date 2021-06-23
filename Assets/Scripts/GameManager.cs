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
            // �ν��Ͻ��� ���� ��쿡 �����Ϸ� �ϸ� �ν��Ͻ��� �Ҵ����ش�.
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
