using UnityEngine;

public class CameraControlExam06 : MonoBehaviour
{
    public GameObject player1;
    public GameObject player2;
    public float offset;
    public Camera targetCamera;

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 player1Pos = player1.transform.position; //Vertical on x    
        Vector3 player2Pos = player2.transform.position; //Horizontal on z  

        Vector3 centerPoint = new Vector3((player1Pos.x + player2Pos.x) / 2, targetCamera.transform.position.y, (player1Pos.z + player2Pos.z) / 2);

        // Student code ...

        targetCamera.transform.position = centerPoint;
        targetCamera.orthographicSize = Mathf.Sqrt(Mathf.Pow(Mathf.Abs(centerPoint.x - player1Pos.x),2) + Mathf.Pow(Mathf.Abs(centerPoint.z - player1Pos.z),2)) + offset;

    }
}
