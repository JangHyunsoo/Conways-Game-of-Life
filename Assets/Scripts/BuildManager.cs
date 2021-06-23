using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildManager : MonoBehaviour
{
    private static BuildManager _instance = null;

    public static BuildManager Instance
    {
        get
        {
            // 인스턴스가 없는 경우에 접근하려 하면 인스턴스를 할당해준다.
            if (!_instance)
            {
                _instance = FindObjectOfType(typeof(BuildManager)) as BuildManager;

                if (_instance == null)
                    Debug.Log("no Singleton obj");
            }
            return _instance;
        }
    }

    [SerializeField] private BuildData[] buildPrototype; // all
    [SerializeField] private Image[] buttonImages;
    private BuildData[] buildDatas; // 0 ~ 4
    public int currentIndx = 0;

    public bool Init()
    {
        buildDatas = new BuildData[buttonImages.Length];

        for (int i = 0; i < buttonImages.Length; i++)
        {
            buildDatas[i] = buildPrototype[Random.Range(0, buildPrototype.Length)];
            buttonImages[i].sprite = buildDatas[i].dataSprite;
        }

        return true;
    }

    public void SelectIndex(int value)
    {
        currentIndx = value;
    }

    public BuildData GetCurrentData()
    {
        return buildDatas[currentIndx];
    }
}
