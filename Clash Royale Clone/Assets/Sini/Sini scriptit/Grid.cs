using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid 
{

    private int columns;
    private int rows;
    private int[,] gridArray;

    public Transform gridPrefab;
    int row = 4;
    int col = 4;

    public Grid (int columns, int rows)
    {
        this.columns = columns;
        this.rows = rows;



        //gridArray = new int[columns, rows]; 

        //for(int x = 0; x<gridArray.GetLength(0); x++)
        //{
        //    for(int y = 0; y<gridArray.GetLength(1); y++)
        //    {
        //        Debug.DrawLine(GetWorkdPosition(x,y), GetWorkdPosition(x, y+1)
        //    }
        //}
    }



}
