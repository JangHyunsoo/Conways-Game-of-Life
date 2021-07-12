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
            // �ν��Ͻ��� ���� ��쿡 �����Ϸ� �ϸ� �ν��Ͻ��� �Ҵ����ش�.
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
    public BuildData[] buildDatas; // 0 ~ 4
    public int currentIndx = 0;
    public int BlockRot = 0;

    public bool Init()
    {
        buildDatas = new BuildData[currentIndx + BlockRot];
        return true;
    }

    public void SelectIndex(int value)
    {
        currentIndx = value;
    }

    public BuildData UseCurrentData()
    {
        BuildData data = buildPrototype[currentIndx + BlockRot];
        buttonImages[currentIndx / 4].sprite = buildPrototype[currentIndx].dataSprite;
        return data;
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) currentIndx = 0 * 4;
        if (Input.GetKeyDown(KeyCode.Alpha2)) currentIndx = 1 * 4;
        if (Input.GetKeyDown(KeyCode.Alpha3)) currentIndx = 2 * 4;
        if (Input.GetKeyDown(KeyCode.Alpha4)) currentIndx = 3 * 4;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            BlockRot = (BlockRot + 1) % 4;
            buttonImages[currentIndx / 4].sprite = buildPrototype[currentIndx + BlockRot].dataSprite;
        }
    }
}
