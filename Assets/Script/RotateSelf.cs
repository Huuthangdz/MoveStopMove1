using UnityEngine;

public class RotateSelf : MonoBehaviour
{
    [SerializeField] float rotate = -2;

    // Update is called once per frame
    void LateUpdate()
    {
        transform.Rotate(Vector3.up * rotate, Space.Self);
    }
}
