using UnityEngine;
public class CameraMovement : MonoBehaviour
{
    [SerializeField] private Camera cam;
    public GameObject camAxis;
    private Vector3 previousPosition;
    float rx, ry;
    public float rotSpeed = 30;
    public float wheelSpeed = 10;
    float zoomMax;
    float zoomValue;

    private void Update()
    {

        if (Input.GetMouseButtonDown(1))
        {
            previousPosition = cam.ScreenToViewportPoint(Input.mousePosition);
        }
        else if (Input.GetMouseButton(1))
        {
            Vector3 newPosition = cam.ScreenToViewportPoint(Input.mousePosition);
            Vector3 direction = previousPosition - newPosition;

            float rotationAroundYAxis = -direction.x * 180; // camera moves horizontally
            float rotationAroundXAxis = direction.y * 180; // camera moves vertically
            rx += rotationAroundXAxis * rotSpeed * Time.deltaTime;
            ry += rotationAroundYAxis * rotSpeed * Time.deltaTime;

            rx = Mathf.Clamp(rx, -45, 45);
            camAxis.transform.eulerAngles = new Vector3(rx, ry, 0);
            
            previousPosition = newPosition;
        }
        void zoomIn()
        {
            Vector3 zoomIn = camAxis.transform.position - cam.transform.position;
            Camera.main.transform.position = cam.transform.position + zoomIn * 1 / 10;
        }
        void zoomOut()
        {

            Vector3 zoomOut = camAxis.transform.position - cam.transform.position;
            Camera.main.transform.position = cam.transform.position - zoomOut * 1 / 10;

            //만약에 카메라 위치가 처음 위치로 돌아온다면 줌아웃 멈추기
            //1) 만약에 카메라가 처음 위치로 돌아온다면
            //zoomMax = Mathf.Clamp(zoomMax, 0, 1);
            //if (zoomMax > 1)
            //{
                
            //}
            ////{
            ////    //2) 줌아웃 멈추기
            //zoomOut = Vector3.ClampMagnitude(zoomOut, zoomMax);
            ////}

        }

        float MouseWheel = Input.GetAxis("Mouse ScrollWheel");

        zoomValue += MouseWheel;
        //print(zoomValue);
        zoomValue = Mathf.Clamp(zoomValue, -1, 1);

        if (MouseWheel > 0)
        {
            zoomIn();

        }

        else if (MouseWheel < 0)
        {
            zoomOut();

        }

        //    //float MouseWheel = Input.GetAxis("Mouse ScrollWheel") * wheelSpeed;
        //    //if (cam.fieldOfView <= 20 && MouseWheel < 0)
        //    //{
        //    //    cam.fieldOfView = 20;
        //    //}
        //    //else if (cam.fieldOfView >= 60 && MouseWheel > 0)
        //    //{
        //    //    cam.fieldOfView = 60;
        //    //}
        //    //else
        //    //{
        //    //    cam.fieldOfView += MouseWheel;
        //    //}



    }
}
