using UnityEngine;

public class Follow_player : MonoBehaviour
{

    public Transform player;
    public float distanceToObject = 5;

    // Update is called once per frame
    void Update()
    {
        // respect the orientation of the camera
        Vector3 translation = -distanceToObject * transform.forward;
        transform.position = player.transform.position + translation;
    }
}
