using UnityEngine;

public class TrailAnimation : MonoBehaviour
{
    [SerializeField] Transform trail1;
    [SerializeField] Transform trail2;

    private float movementRadius = 0.25f;
    private float movementSpeed = 25f;
    private float angle = 0;

    public bool locked = false;

    [SerializeField] float moveSpeed = 1.0f;

    private float startTime;
    private Vector3 startPosition;
    private Vector3 targetPosition;

    void Start()
    {
        startTime = Time.time;
        startPosition = transform.localPosition;
        targetPosition = new Vector3(0,0,0);
    }

    private void Update()
    {
        if (!locked)
        {
            angle += Time.deltaTime * movementSpeed;

            float xPosition = Mathf.Cos(angle) * movementRadius;
            float yPosition = Mathf.Sin(angle) * movementRadius;

            trail1.localPosition = new Vector3(-xPosition, -yPosition, transform.localPosition.z);
            trail2.localPosition = new Vector3(xPosition, yPosition, transform.localPosition.z);
        }

        float elapsedTime = Time.time - startTime;
        transform.localPosition = Vector3.Lerp(startPosition, targetPosition, elapsedTime * moveSpeed);

        if (elapsedTime >= 1f)
        {
            transform.localPosition = targetPosition;
            enabled = false;
        }
    }
}
