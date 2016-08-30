using UnityEngine;
using System.Collections;

public class GameOfLife : MonoBehaviour {


    public float t;
    public int height;
    public int width;
    bool[,] board;

    public float chanceOfStartAlive;


    void OnDrawGizmos()
    {
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                if (board[i, j])
                    Gizmos.DrawCube(new Vector2(i, j), new Vector3(1, 1, 1));
            }

        }
    }
    // Use this for initialization
    void Start () {
        board = new bool[width, height];
        for(int i=0;i<width;i++)
        {
            for (int j = 0; j < height; j++)
            {
                if (Random.Range(0.0f, 1.0f) <= chanceOfStartAlive)
                {
                    board[i, j] = true;
                }
                else
                {
                    board[i, j] = false;
                }
            }
        }
	}

    void Step(bool[,] oldBoard)
    {
        bool[,] newBoard = new bool[width, height];
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                int count = NeighbourCheck(oldBoard,i,j);
                if (count < 2 || count > 3)
                    newBoard[i, j] = false;
                else if (count == 3)
                    newBoard[i, j] = true;
                else
                    newBoard[i, j] = oldBoard[i, j];
            }
        }
        board = newBoard;

    }


    int NeighbourCheck(bool[,] oldBoard,int x,int y)
    {
        int count = 0;
        for (int i = -1; i < 2; i++)
        {
            for (int j = -1; j < 2; j++)
            {
                int neighbour_x = x + i;
                int neighbour_y = y + j;

                if (i == 0 && j == 0)
                {

                }
                else if (oldBoard[(neighbour_x+width)%width, (neighbour_y+height)%height])
                    count = count + 1;
            }
        }
        return count;
    }

    void Update()
    {
        Time.timeScale = t;
    }

	// Update is called once per frame
	void FixedUpdate () {
        Step(board);
	}
}
