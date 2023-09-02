using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ProceduralGenerationAlgorithms
{
   public static HashSet<Vector2Int> SimpleRandomWalk(Vector2Int startPosition,int walkLenght)
   {
        HashSet<Vector2Int> path = new HashSet<Vector2Int>();

        path.Add(startPosition);
        var previousPosition = startPosition;

        for (int i = 0; i < walkLenght; i++)
        {
            var newPosition = previousPosition + Direction2D.GetRandomDirection();
            path.Add(newPosition);
            previousPosition = newPosition;
        }
        return path;
   }
}

public static class Direction2D
{
    public static List<Vector2Int> AllDirections = new List<Vector2Int>
    {
        new Vector2Int(0,1), //UP
        new Vector2Int(1,0), //RIGHT
        new Vector2Int(-1,0), //LEFT
        new Vector2Int(0,-1) //DOWN
    };

    public static Vector2Int GetRandomDirection()
    {
        return AllDirections[Random.Range(0, AllDirections.Count)];
    }
}
