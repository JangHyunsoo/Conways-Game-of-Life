using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    public bool isAlive = false;
    public bool isPreivew = false;
    public TEAM team = TEAM.BLUE;
    public int numNeighbors = 0;

    public void SetAlive(bool alive)
    {
        isAlive = alive;

        if (isPreivew)
        {
            return;
        }

        if (alive)
        {
            GetComponent<SpriteRenderer>().enabled = true;
        }
        else
        {
            GetComponent<SpriteRenderer>().enabled = false;
        }
    }

    public void SetPreivew(bool value)
    {
        isPreivew = value;

        if (isPreivew)
        {
            GetComponent<SpriteRenderer>().enabled = true;
        }
        else
        {
            SetAlive(isAlive);
        }
    }


    private void OnMouseUp()
    {
        RuleManager.Instance.BuildInMap((int)transform.position.x, (int)transform.position.y);
    }
}
