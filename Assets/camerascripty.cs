using UnityEngine;
using UnityEngine.SceneManagement;

public class camerascripty : MonoBehaviour
{
    public Transform player;
    public Transform camera;

    private float cameray = -1.1f;

    void Update()
    {

        if (player.position.x > 98 && player.position.y > -0.4f && SceneManager.GetActiveScene().name == "Scheikundeklaar") //and current level is scheikunde
        {
            cameray = 2f;
        }
        else
        {
            cameray = -1.1f;
        }

        camera.position = new Vector3(player.position.x, cameray, -100);
    }

}