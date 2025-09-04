using UnityEngine;

public class MazeCell : MonoBehaviour
{
    public GameObject leftWall;
    public GameObject rightWall;
    public GameObject frontWall;
    public GameObject backWall;
    [SerializeField] GameObject unvisited;

    public bool isVisited = false;

    public void Visit()
    {
        isVisited = true;
        unvisited.SetActive(false);
    }

    public void ClearWall(int wallIndex)
    {
        switch (wallIndex)
        {
            case 0:
                leftWall.SetActive(false);
                break;
            case 1:
                rightWall.SetActive(false);
                break;
            case 2:
                frontWall.SetActive(false);
                break;
            case 3:
                backWall.SetActive(false);
                break;
            default:
                Debug.LogError("Invalid wall index");
                break;
        }
    }
}
