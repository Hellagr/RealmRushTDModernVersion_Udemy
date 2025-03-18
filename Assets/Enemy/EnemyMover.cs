using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyMover : MonoBehaviour
{
    [SerializeField] List<Waypoint> path = new List<Waypoint>();
    [SerializeField][Range(0f, 5f)] float speed = 1f;
    [SerializeField] GameObject pathObject;

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

    Enemy enemy;

    void OnEnable()
    {
        pathObject = GameObject.FindGameObjectWithTag("PathMain").gameObject;
        FindPath();
        ReturnToStart();
        StartCoroutine(FollowPath());
    }

    void Start()
    {
        enemy = GetComponent<Enemy>();
    }

    void FindPath()
    {
        path.Clear();

        Waypoint[] waypoints = pathObject.GetComponentsInChildren<Waypoint>();

        foreach (var waypoint in waypoints)
        {
            path.Add(waypoint);
        }
    }

    void ReturnToStart()
    {
        transform.position = path[0].transform.position;
    }

    IEnumerator FollowPath()
    {
        foreach (var waypoint in path)
        {
            if (waypoint != path[0])
            {
                Vector3 startPos = transform.position;
                Vector3 endPost = waypoint.transform.position;
                float travelPercent = 0;

                Vector3 topPointVector1 = transform.forward;
                Vector3 topPointVector2 = (endPost - startPos).normalized;
                //if (StartRotation == null)
                //{
                //    StartRotation.position = topPointVector2;
                //}

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
        }
        enemy.SteelGold();
        gameObject.SetActive(false);
    }
}
