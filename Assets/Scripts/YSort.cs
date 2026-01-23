using UnityEngine;

public class YSort : MonoBehaviour
{
    private Renderer sr;
    public Transform SortPoint;

    private void Awake()
    {
        sr = GetComponent<Renderer>();
    }

    private void LateUpdate()
    {
        // float yPos = sr.bounds.min.y;
        if (SortPoint != null)
        {
            float yPos = -SortPoint.position.y;
            sr.sortingOrder = Mathf.RoundToInt(yPos * 100);
        }
        else
        {
            float yPos = -transform.position.y;
            sr.sortingOrder = Mathf.RoundToInt(yPos * 100);
        }
    }
}
