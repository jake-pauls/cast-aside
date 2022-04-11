using UnityEngine;

public class FixedCamera : MonoBehaviour
{
    void Update() => transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, 0.0f);
}
