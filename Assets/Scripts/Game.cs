using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    private static int SCREEN_WIDTH = 64;  // 1024 pixel
    private static int SCREEN_HEIGHT = 48; // 768 pixel

    Cell[,] grid = new Cell[SCREEN_WIDTH, SCREEN_HEIGHT];

    private void Start()
    {
        PlaceCells();
    }

    private void Update()
    {
        CountNeighbors();
    }

    private void PlaceCells()
    {
        for (int y = 0; y < SCREEN_HEIGHT; y++)
        {
            for (int x = 0; x < SCREEN_WIDTH; x++)
            {
                Cell cell = Instantiate(Resources.Load("Prefabs/Cell", typeof(Cell)), new Vector2(x, y), Quaternion.identity) as Cell;
                grid[x, y] = cell;
                grid[x, y].SetAlive(RandomAliveCell());

            }
        }
    }

    private static Vector2Int[] surround = new Vector2Int[] { new Vector2Int( 1,1 ), new Vector2Int( 1, 0 ), new Vector2Int( 1, -1 ),
        new Vector2Int( 0, 1 ), new Vector2Int( 0, -1 ), new Vector2Int( -1, 1 ), new Vector2Int( -1, 0 ), new Vector2Int( -1, -1 ) };

    private void CountNeighbors()
    {
        for (int y = 0; y < SCREEN_HEIGHT; y++)
        {
            for (int x = 0; x < SCREEN_WIDTH; x++)
            {
                int numNeighbors = 0;

                foreach (var sur in surround)
                {
                    if(y + sur.y < SCREEN_HEIGHT && y + sur.y  >= 0 && x + sur.x < SCREEN_WIDTH && x + sur.x >= 0)
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

    private bool RandomAliveCell()
    {
        int rand = UnityEngine.Random.Range(0, 100);

        if(rand > 75)
        {
            return true;
        }

        return false;
    }
}



/*
1. ����ִ� �̿��� 2 �� �̸��� ����ִ� ������ ��ġ �α� �������� �״� ��ó�� �׽��ϴ�.
2. 2 ~ 3 ���� ����ִ� �̿����ִ� ��� ����ִ� ������ ���� ���븦 ���� ��ư��ϴ�.
3. �� �� �̻��� ����ִ� �̿����ִ� ����ִ� ������ ��ġ �α� �������� �״� ��ó�� �׽��ϴ�.
4. ��Ȯ�� �� ���� ����ִ� �̿����ִ� ���� ������ ��ġ ���Ŀ� ���� ��ó�� ����ִ� �������˴ϴ�.

1. 2 �� �Ǵ� 3 ���� ����ִ� �̿����ִ� ����ִ� ������ ��Ƴ����ϴ�.
2. �� ���� ����ִ� �̿����ִ� ���� ������ ����ִ� �������˴ϴ�.
3. �ٸ� ��� ����ִ� ������ ���� ���뿡 �׽��ϴ�. ���������� �ٸ� ��� ���� ������ ���� ���·� �����˴ϴ�.
 */