using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MazeGenerator : MonoBehaviour
{
    [SerializeField] private MazeCell cellPrefab;
    [SerializeField] Transform Goal;

    [Header("Config")]
    [SerializeField] private int width = 10;
    [SerializeField] private int depth = 10;

    private MazeCell[,] grid;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    IEnumerator Start()
    {
        grid = new MazeCell[width, depth];
        for(int x = 0; x < width; x++)
        {
            for (int z = 0; z < depth; z++)
            {
                MazeCell newCell = Instantiate(cellPrefab, new Vector3(x * 4, 0, z * 4), Quaternion.identity);
                newCell.name = $"Cell {x} {z}";
                grid[x, z] = newCell;
            }
        }

        yield return GenerateMaze(null, grid[0, 0]);

        List<MazeCell> deadEnds = new List<MazeCell>();
        for (int x = 0; x < width; x++)
        {
            for (int z = 0; z < depth; z++)
            {
                MazeCell cell = grid[x, z];
                int openWalls = 0;
                // Check each wall: 0=left, 1=right, 2=front, 3=back
                if (cell.leftWall == null || !cell.leftWall.activeSelf) openWalls++;
                if (cell.rightWall == null || !cell.rightWall.activeSelf) openWalls++;
                if (cell.frontWall == null || !cell.frontWall.activeSelf) openWalls++;
                if (cell.backWall == null || !cell.backWall.activeSelf) openWalls++;
                if (openWalls == 1)
                    deadEnds.Add(cell);
            }
        }

        if (deadEnds.Count > 0)
        {
            MazeCell goalCell = deadEnds[Random.Range(0, deadEnds.Count)];
            Goal.position = goalCell.transform.position + new Vector3(0, 1, 0);
        }

        GameObject.FindFirstObjectByType<PlayerLocker>().ConfirmLoad();
        GameObject.FindFirstObjectByType<Timer>().TimerActive = true;
    }

    IEnumerator GenerateMaze(MazeCell Prev, MazeCell Current)
    {
        Current.Visit();
        ClearWalls(Prev, Current);

        MazeCell Next;

        do
        {
            Next = GetnextUnivisited(Current);
            if (Next != null)
            {
                yield return GenerateMaze(Current, Next);
            }
        } while (Next != null);
    }

    MazeCell GetnextUnivisited(MazeCell Current)
    {
        var unvisiteds = GetUnvisited(Current);
        return unvisiteds.OrderBy(_ => Random.Range(1,10)).FirstOrDefault();
    }

    IEnumerable<MazeCell> GetUnvisited(MazeCell Current)
    {
        int x = (int)(Current.transform.position.x / 4);
        int z = (int)(Current.transform.position.z / 4);

        if(x+1 < width)
        {
            var right = grid[x + 1, z];
            if (!right.isVisited) yield return right;
        }
        if(x-1 >= 0)
        {
            var left = grid[x - 1, z];
            if (!left.isVisited) yield return left;
        }
        if(z+1 < depth)
        {
            var front = grid[x, z + 1];
            if (!front.isVisited) yield return front;
        }
        if(z-1 >= 0)
        {
            var back = grid[x, z - 1];
            if (!back.isVisited) yield return back;
        }
    }

    void ClearWalls(MazeCell Prev, MazeCell Current)
    {
        if (Prev == null || Current == null)
        {
            Debug.LogError("Prev or Current is null");
            return;
        }
        int xDiff = (int)(Current.transform.position.x - Prev.transform.position.x);
        int zDiff = (int)(Current.transform.position.z - Prev.transform.position.z);
        if (xDiff == 4) // Current is to the right of Prev
        {
            Prev.ClearWall(1); // Clear right wall of Prev
            Current.ClearWall(0); // Clear left wall of Current
        }
        else if (xDiff == -4) // Current is to the left of Prev
        {
            Prev.ClearWall(0); // Clear left wall of Prev
            Current.ClearWall(1); // Clear right wall of Current
        }
        else if (zDiff == 4) // Current is in front of Prev
        {
            Prev.ClearWall(2); // Clear front wall of Prev
            Current.ClearWall(3); // Clear back wall of Current
        }
        else if (zDiff == -4) // Current is behind Prev
        {
            Prev.ClearWall(3); // Clear back wall of Prev
            Current.ClearWall(2); // Clear front wall of Current
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
