using UnityEngine;

public class CameraController : MonoBehaviour
{
    private bool doMovement = true;

    public float panSpeed = 30f;
    public float panBorderThhickness = 10f;

    public float scrollSpeed = 5f;
    private float minY = 10f;
    private float maxY = 160f;
    // Update is called once per frame
    void Update()
    {
        if (GameManager.gameEnd)
        {
            this.enabled = false;
            return;
        }
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            doMovement = !doMovement;
        }
        if (!doMovement)
        {
            return;
        }
        if (Input.GetKey("w") || Input.mousePosition.y>=Screen.height-panBorderThhickness)
        {
            transform.Translate(Vector3.forward * panSpeed * Time.deltaTime,Space.World);
        }
        if (Input.GetKey("s") || Input.mousePosition.y <= panBorderThhickness)
        {
            transform.Translate(Vector3.back * panSpeed * Time.deltaTime, Space.World);
        }
        if (Input.GetKey("a") || Input.mousePosition.x <=  panBorderThhickness)
        {
            transform.Translate(Vector3.left * panSpeed * Time.deltaTime, Space.World);
        }
        if (Input.GetKey("d") || Input.mousePosition.x >= Screen.width - panBorderThhickness)
        {
            transform.Translate(Vector3.right * panSpeed * Time.deltaTime, Space.World);
        }
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        Vector3 pos = transform.position;

        pos.y -= scroll *1000* scrollSpeed * Time.deltaTime;
        pos.y = Mathf.Clamp(pos.y,minY,maxY);

        transform.position = pos;
    }
}
