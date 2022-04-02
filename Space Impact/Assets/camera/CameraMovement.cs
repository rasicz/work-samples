using System;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [HideInInspector]
    public GameObject player;
    private float difference;
    private float initalDifference;
    private float cameraStartPosition;
    private float playerStartPosition;
    private float? targetPosition;
    private bool differenceChanged = false;
    public float Difference { get {
            return difference; 
        } set {
            differenceChanged = true;
            if(difference < value)
            {
                cameraStartPosition = transform.position.y;
                playerStartPosition = player.transform.position.y;
                targetPosition = Math.Abs(cameraStartPosition - playerStartPosition) + playerStartPosition;
                Debug.Log("cameraStartPosition: " + cameraStartPosition);
                Debug.Log("playerStartPosition: " + playerStartPosition);
                Debug.Log("targetPosition: " + targetPosition);
            }   
            else
            {
                targetPosition = null;
            }
            difference = value;
        } }
    void Start()
    {
        player = GameObject.Find("PlayerShip");
        difference = transform.position.y - player.gameObject.transform.position.y;
    }
    void Update()
    {
        if (player == null) return;
        if (!differenceChanged)
        {
        transform.position = new Vector3(transform.position.x, player.transform.position.y + difference, transform.position.z);
        }
        else
        {
            if (Math.Abs(player.transform.position.y - transform.position.y + difference) < 0.1F) differenceChanged = false;
            if (targetPosition != null)
            {
                //teleports to targetPosition two times faster then player
                float n = cameraStartPosition - 2 * playerStartPosition + 2 * player.transform.position.y;
                transform.position = new Vector3(0, n, transform.position.z);
            }
        }
        //if (player == null) ChangeShip();
    }

    //
    public void ChangeShip()
    {
        player = GameObject.Find("PlayerShip");
        difference = transform.position.y - player.gameObject.transform.position.y;
    }
}
