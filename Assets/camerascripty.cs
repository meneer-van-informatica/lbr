using UnityEngine;
using UnityEngine.SceneManagement;

public class camerascripty : MonoBehaviour
{
    public Transform player;
    public Transform camera;

    void Update()
    {
        camera.position = new Vector3(player.position.x, 0, -2);
    }

}