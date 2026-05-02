using UnityEngine;

public class FlashlightAim : MonoBehaviour
{
    public Transform flashlight;

    void Update()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;

        Vector2 dir = mousePos - transform.position;

        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        flashlight.position = transform.position;
        flashlight.rotation = Quaternion.Euler(0, 0, angle - 90);
    }
}