using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class FaceMainCamera : MonoBehaviour
{
    [SerializeField] private Vector3 offset;
    [SerializeField] private Transform targetTransform;
    [SerializeField] private RectTransform rectTransform;

    private void LateUpdate()
    {
        if (!Camera.main) return;

        Vector3 sp = Camera.main.WorldToScreenPoint(targetTransform.position + offset);
        rectTransform.anchoredPosition = sp;
    }
}
