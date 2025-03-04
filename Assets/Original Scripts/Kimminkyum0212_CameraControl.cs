using UnityEngine;

public class Kimminkyum0212_CameraControl : MonoBehaviour
{
    public Transform player;


    void Start()
    {
        transform.position = new Vector3(player.position.x, player.position.y, -10);
    }


    void Update()
    {

        if (Kimminkyum0212_GameManager.instance.isPlaying)
        {
            transform.position = new Vector3(player.position.x, player.position.y, -10);
        }

    }
}
