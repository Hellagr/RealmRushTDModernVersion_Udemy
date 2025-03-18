using UnityEngine;

public class StartRotation : MonoBehaviour
{
    [SerializeField] Transform tile1;
    [SerializeField] Transform tile2;

    public static Transform startPosition;

    void Awake()
    {
        startPosition.position = (tile2.position - tile1.position).normalized;
    }

    void Start()
    {

    }
}
