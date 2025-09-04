using UnityEngine;

public class Rotation : MonoBehaviour
{

    [SerializeField] float t = 0;
    private void Update()
    {
        t += Time.deltaTime;
        Rotate();
    }

    void Rotate()
    {
        float x = Mathf.Sin(t) * 135f;
        float y = Mathf.Cos(t * 0.7f) * 180f;
        float z = Mathf.Sin(t * 0.5f) * 90f;
        // Clamp z to avoid extreme values from Tan
        z = Mathf.Clamp(z, -90f, 90f);
        transform.rotation = Quaternion.Euler(x, y, z);
    }
}
