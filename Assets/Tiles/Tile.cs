using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] Tower towerPrefab;
    [SerializeField] bool isPlaceable;
    public bool IsPlaceable { get { return isPlaceable; } }

    GridManager gridManager;
    PathFinder pathFinder;
    Vector2Int coordinates = new Vector2Int();

    void Awake()
    {
        gridManager = FindAnyObjectByType<GridManager>();
        pathFinder = FindAnyObjectByType<PathFinder>();
    }

    void Start()
    {
        if (gridManager != null)
        {
            coordinates = gridManager.GetCoordinatesFromPosition(transform.position);

            if (!isPlaceable && gridManager.Grid.ContainsKey(coordinates))
            {
                gridManager.BlockNode(coordinates);
            }
        }
    }

    void OnMouseDown()
    {
        bool isSuccessful = towerPrefab.CreateTower(towerPrefab, transform.position);

        if (isSuccessful && gridManager.GetNode(coordinates).isWalkable && !pathFinder.WillBlockPath(coordinates))
        {
            gridManager.BlockNode(coordinates);
            pathFinder.NotifyReceivers();
        }
    }
}
