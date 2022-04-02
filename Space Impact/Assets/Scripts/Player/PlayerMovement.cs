using System;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    public bool mouse;
    public float horizontalSpeed;
    public float horizontalAcceleration;

    public float verticalSpeed;
    //public Text text;

    [HideInInspector]
    public bool leftHitbox;
    [HideInInspector]
    public bool rightHitbox;

    float targetPositionX;
    Rigidbody2D rigidbody;
    
    private bool movementActive = true;
    public bool MovementActive{ 
        get { return movementActive; } 
        set { movementActive = value; } }
    void Start()
    {

#if UNITY_ANDROID && !UNITY_EDITOR
        mouse = false;
#else
        mouse = true;
#endif
        Input.simulateMouseWithTouches = true;
        rigidbody = gameObject.GetComponent<Rigidbody2D>();
        rigidbody.velocity = new UnityEngine.Vector2(0, verticalSpeed);
        GlobalVariables.playerVerticalSpeed = verticalSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        if (GlobalVariables.Paused) return;
        if (movementActive)
        {
            getTargetPosition();
            Movement();
        }
    }
    //gets main touch position
    private void getTargetPosition()
    {
        Touch mainTouch;
        mainTouch = new Touch();
        bool touching = false;
        if (mouse)
        {
            mainTouch.position = Input.mousePosition;
            touching = true;
        }
        if(Input.touches.Length > 0)
        {
            mainTouch = Input.touches[0];
            touching = true;
        }
        if (touching)
        {
            if (isOnTouchField(mainTouch))
            {
                targetPositionX = Camera.main.ScreenToWorldPoint(mainTouch.position).x;
            }
        }
    }
    private bool isOnTouchField(Touch touch)
    {
        if (mouse) return true;
        if (touch.position.y <= Camera.main.pixelHeight / 2)
            return true;
        else return false;
    }

    private void Movement()
    {
        float velocity = rigidbody.velocity.x;
        velocity = math.abs(velocity);
        float distance = math.distance(transform.position.x, targetPositionX);
        //bool enoughDistance = false;//distance > 0.1F;
                                    //makes sure it won't move past borders
        /**
        if ((!rightHitbox || transform.position.x > targetPositionX) && (!leftHitbox || transform.position.x < targetPositionX))
        {
            if (enoughDistance)
            {
                
                 if (velocity < horizontalSpeed)
                 {
                        velocity += horizontalAcceleration;
                 }
                 else
                 {
                        velocity = horizontalSpeed;
                 }
            }
            else
            {
                velocity = 0;
            }
        }
        else
        {
            velocity = 0;
        }

        if (transform.position.x > targetPositionX) velocity *= -1;
        rigidbody.velocity = new Vector2(velocity, rigidbody.velocity.y);
        **/
        if (targetPositionX > 2.9F) targetPositionX = 2.9F;
        if (targetPositionX < -2.9F) targetPositionX = -2.9F;
        transform.position = new Vector2(targetPositionX, transform.position.y);
    }
}
