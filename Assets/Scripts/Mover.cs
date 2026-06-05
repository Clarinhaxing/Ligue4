using UnityEngine;

public class Mover : MonoBehaviour
{
    [SerializeField]
    private float speed = 10f;

    public Vector3 targetPosition;

    private void Update()
    {
        transform.position = Vector3.MoveTowards(
            transform.position,
            targetPosition,
            speed * Time.deltaTime
        );
    }
}