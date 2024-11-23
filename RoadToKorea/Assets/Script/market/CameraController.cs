using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraController : MonoBehaviour
{
    public float cameraSpeed = 5.0f;

    public GameObject player;

    private void Update()
    {
        if(SceneManager.GetActiveScene().name == "market")
        {
            Vector3 dir = player.transform.position - this.transform.position;
            Vector3 moveVector;
            if ((player.transform.position.x <= 8 && player.transform.position.x >= -8) && SceneManager.GetActiveScene().name == "market")
            {
                moveVector = new Vector3(0, dir.y * cameraSpeed * Time.deltaTime, 0.0f);
            }
            else
            {
                moveVector = new Vector3(0, 0, 0);
                Camera.main.orthographicSize = 11.87f;
                GameObject.Find("Player").transform.parent = null;
                GameObject.Find("Player").transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
            }
            if (player.transform.position.x <= 170 && player.transform.position.x >= 125)
            {
                Camera.main.orthographicSize = 11.9f;
                Camera.main.transform.position = new Vector3(148.54f, 51.7f, -2f);
                GameObject.Find("Player").transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);
            }
            this.transform.Translate(moveVector);
        }
        else{
            moveVector = new Vector3(0,0,0);
            Camera.main.orthographicSize = 11.87f;
            GameObject.Find("Player").transform.parent = null;
            GameObject.Find("Player").transform.localScale = new Vector3(0.8f,0.8f,0.8f);
        }
        if(player.transform.position.x<=170&&player.transform.position.x>=125){
            Camera.main.orthographicSize = 11.9f;
            Camera.main.transform.position = new Vector3 (148.54f,51.7f,-2f);
            GameObject.Find("Player").transform.localScale = new Vector3(1.2f,1.2f,1.2f);
            DialogManager.Instance.DialogTrigger();
        }
        this.transform.Translate(moveVector);
    }
}
