using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LightController : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject player;
    public GameObject playerLight;
    public GameObject flashlight;
    public GameObject globalLight;
    public GameObject exitTrigger;
    public bool isHidden;
    public Vector3 sarLocation;

    private void Start()
    {
        if (!(SceneManager.GetActiveScene().name == "Room1")) {

            globalLight.SetActive(false);

        }
        isHidden = false;

    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (!isHidden)
        {
            playerLight.SetActive(true);
            flashlight.SetActive(true);
            playerLight.transform.position = player.transform.position;
            flashlight.transform.position = player.transform.position;

            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            Vector3 direction = mousePos - player.transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + 270;

            flashlight.transform.rotation = Quaternion.Euler(0, 0, angle);
        }
        else
        {
            playerLight.SetActive(false);
            flashlight.SetActive(false);
        }
        if (exitTrigger.activeInHierarchy) {
            globalLight.SetActive(false);
        }
        

    }
}
