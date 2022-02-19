using UnityEngine;

[DisallowMultipleComponent]
public class Customer : MonoBehaviour
{
    public int id;
    public float interarrivalTime;
    public float serviceTime;

    [SerializeField] private float speed = 10;
    private Vector3 targetPosition;

    [SerializeField] private MeshRenderer meshRenderer;
    [SerializeField] private Material defaultMat;
    [SerializeField] private Material serviceMat;

    public void SetData(CustomerData data)
    {
        id = data.id;
        interarrivalTime = data.interarrivalTime;
        serviceTime = data.serviceTime;
    }

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
    }

    public void SetTargetPosition(Vector3 position)
    {
        targetPosition = position;
    }

    public void SnapToTarget()
    {
        transform.position = targetPosition;
    }

    public void BeginService()
    {
        meshRenderer.material = serviceMat;
    }

    public void EndService()
    {
        meshRenderer.material = defaultMat;
    }
}
