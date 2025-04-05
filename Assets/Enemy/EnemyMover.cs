using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyMover : MonoBehaviour
{
    [SerializeField][Range(0f, 5f)] float speed = 1f;

    List<Node> path = new List<Node>();
    Enemy enemy;
    GridManager gridManager;
    PathFinder pathFinder;

    Transform startRotation;
    public Transform StartRotation
    {
        get
        {
            return startRotation;
        }
        set
        {
            startRotation = value;
        }
    }


    void OnEnable()
    {
        ReturnToStart();
        RecalculatePath(true);
    }

    void Awake()
    {
        enemy = GetComponent<Enemy>();
        gridManager = FindAnyObjectByType<GridManager>();
        pathFinder = FindAnyObjectByType<PathFinder>();
    }

    void RecalculatePath(bool resetPath)
    {
        Vector2Int coordinates = new Vector2Int();

        if (resetPath)
        {
            coordinates = pathFinder.StartCoordinates;
        }
        else
        {
            coordinates = gridManager.GetCoordinatesFromPosition(transform.position);
        }

        StopAllCoroutines();
        path.Clear();
        path = pathFinder.GetNewPath(coordinates);
        StartCoroutine(FollowPath());
    }

    void ReturnToStart()
    {
        transform.position = gridManager.GetPositionFromCoordinates(pathFinder.StartCoordinates);
    }

    IEnumerator FollowPath()
    {
        for (int i = 1; i < path.Count; i++)
        {
            Vector3 startPos = transform.position;
            Vector3 endPost = gridManager.GetPositionFromCoordinates(path[i].coordinates);
            float travelPercent = 0;

            Vector3 topPointVector1 = transform.forward;
            Vector3 topPointVector2 = (endPost - startPos).normalized;

            var lengthOfTheFirstVector = Mathf.Sqrt(Mathf.Pow(topPointVector1.x, 2) + Mathf.Pow(topPointVector1.y, 2) + Mathf.Pow(topPointVector1.z, 2));
            var lengthOfTheSecondVector = Mathf.Sqrt(Mathf.Pow(topPointVector2.x, 2) + Mathf.Pow(topPointVector2.y, 2) + Mathf.Pow(topPointVector2.z, 2));

            float scalarProduct = (topPointVector1.x * topPointVector2.x + topPointVector1.y * topPointVector2.y + topPointVector1.z * topPointVector2.z);

            float cosAlpha = scalarProduct / (lengthOfTheFirstVector * lengthOfTheSecondVector);

            float angleCosInRadians = Mathf.Acos(cosAlpha);
            float angleInDegrees = angleCosInRadians * (180f / Mathf.PI);

            Vector3 crossProduct = Vector3.Cross(topPointVector1, topPointVector2);
            if (crossProduct.y < 0)
            {
                angleInDegrees *= -1f;
            }

            Quaternion startRotation = transform.rotation;
            Quaternion targetRotation = transform.rotation * Quaternion.Euler(0, angleInDegrees, 0);

            while (travelPercent < 1)
            {
                travelPercent += Time.deltaTime * speed;
                transform.position = Vector3.Lerp(startPos, endPost, travelPercent);

                if (angleInDegrees != 0)
                {
                    transform.rotation = Quaternion.Lerp(startRotation, targetRotation, travelPercent * 5f);
                }

                yield return new WaitForEndOfFrame();
            }
        }
        FinishPath();
    }

    void FinishPath()
    {
        enemy.SteelGold();
        gameObject.SetActive(false);
    }
}
