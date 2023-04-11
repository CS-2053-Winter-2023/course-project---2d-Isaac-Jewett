using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightController : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject player;
    public GameObject playerLight;
    public GameObject flashlight;

    // Update is called once per frame
    void LateUpdate()
    {
        playerLight.transform.position = player.transform.position;
        flashlight.transform.position = player.transform.position;

        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Vector3 direction = mousePos - player.transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + 270;

        flashlight.transform.rotation = Quaternion.Euler(0, 0, angle);
        

    }
}
