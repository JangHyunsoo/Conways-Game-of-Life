using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuleManager : MonoBehaviour
{
    private static RuleManager _instance = null;

    public static RuleManager Instance
    {
        get
        {
            // 인스턴스가 없는 경우에 접근하려 하면 인스턴스를 할당해준다.
            if (!_instance)
            {
                _instance = FindObjectOfType(typeof(RuleManager)) as RuleManager;

                if (_instance == null)
                    Debug.Log("no Singleton obj");
            }
            return _instance;
        }
    }

    private int width = 64;  // 1024 pixel
    private int height = 48; // 768 pixel
    private Cell[,] grid;

    [SerializeField] float Rand = 75f;
    [SerializeField] float MoveTerm = 0.1f;
    float delay = 0f;

    public bool Init()
    {
        grid = new Cell[width, height];
        return true;
    }

    public void OnUpdate()
    {
        delay += GameManager.Instance.DeltaTime;
        if (!CheckDelay()) return;
        CountNeighbors();
        PopulationControl();
    }

    private bool CheckDelay()
    {
        if (delay >= MoveTerm)
        {
            delay -= MoveTerm;
            return true;
        }
        else
        {
            return false;
        }
    }

    public void PlaceCells(Transform holder)
    {
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                Cell cell = Instantiate(Resources.Load("Prefabs/Cell", typeof(Cell)), new Vector2(x, y), Quaternion.identity) as Cell;
                cell.transform.SetParent(holder);
                grid[x, y] = cell;
                grid[x, y].SetAlive(RandomAliveCell());
            }
        }
    }

    private static Vector2Int[] surround = new Vector2Int[] { new Vector2Int( 1,1 ), new Vector2Int( 1, 0 ), new Vector2Int( 1, -1 ),
        new Vector2Int( 0, 1 ), new Vector2Int( 0, -1 ), new Vector2Int( -1, 1 ), new Vector2Int( -1, 0 ), new Vector2Int( -1, -1 ) };

    private void CountNeighbors()
    {
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                int numNeighbors = 0;

                foreach (var sur in surround)
                {
                    if (IsRangeInMap(x + sur.x, y + sur.y))
                    {
                        if (grid[x + sur.x, y + sur.y].isAlive)
                        {
                            numNeighbors++;
                        }
                    }
                }

                grid[x, y].numNeighbors = numNeighbors;
            }
        }
    }

    bool IsRangeInMap(int x, int y)
    {
        return y < height && y >= 0 && x < width && x >= 0;
    }

    void PopulationControl()
    {
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                if (grid[x, y].isAlive)
                {
                    if (grid[x, y].numNeighbors != 2 && grid[x, y].numNeighbors != 3)
                    {
                        grid[x, y].SetAlive(false);
                    }
                }
                else
                {
                    if (grid[x, y].numNeighbors == 3)
                    {
                        grid[x, y].SetAlive(true);
                    }
                }
            }
        }
    }

    private bool RandomAliveCell()
    {
        int rand = UnityEngine.Random.Range(0, 100);

        if (rand > Rand)
        {
            return true;
        }

        return false;
    }

    public void BuildInMap(int x, int y)
    {
        if (!IsRangeInMap(x, y)) return;

        BuildData data = BuildManager.Instance.GetCurrentData();

        foreach (Vector2 iter in data.dataValue)
        {
            if(IsRangeInMap(x + (int)iter.x, y + (int)iter.y))
            {
                grid[x + (int)iter.x, y + (int)iter.y].SetAlive(true);
            }
        }
    }
}

