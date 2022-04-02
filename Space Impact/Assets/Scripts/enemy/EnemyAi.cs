using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAi : MonoBehaviour
{
    public Weapon[] weapons;
    protected Renderer renderer;
    protected Rigidbody2D rigidbody;
    protected Collider2D collider;
    public float TimeBeforeStaying;
    public float StayOnSceneTime;
    protected float startTime;
    [HideInInspector]
    public bool active = false;
    protected System.Random random = new System.Random(System.Guid.NewGuid().GetHashCode());
    // Start is called before the first frame update
    protected void Start()
    {
        renderer = gameObject.GetComponent<Renderer>();
        rigidbody = gameObject.GetComponent<Rigidbody2D>();
        collider = gameObject.GetComponent<Collider2D>();
        weapons[0].enemy = true;
        weapons[0].lastTimeFired += random.Next(0, 20) / 10F;
        foreach(Weapon w in weapons)
        {
            w.reloadSpeed *= GlobalVariables.reloadSpead;
        }
    }

    // Update is called once per frame
    protected void Update()
    {
        if (renderer.isVisible) active = true;
        if (active)
        {
            gameObject.GetComponent<HealthManager>().enabled = true;
            Ai();
        }
    }
    protected void Ai()
    {
        Vector2 velocity = rigidbody.velocity;
        if (velocity.y == 0) velocity.y = GlobalVariables.shipsStartSpeed;
        if (startTime == 0) { startTime = Time.time; }
        
        //make sure ship is inside camera view
        if (Time.time >= startTime + TimeBeforeStaying) {
            velocity.x = 0;
            velocity.y = GlobalVariables.playerVerticalSpeed;
            if (StayOnSceneTime > 0 && Time.time >= startTime + StayOnSceneTime)
            {
                if (transform.position.x > 0) velocity.x = 2;
                else velocity.x = -2;

                if (!renderer.isVisible)
                {
                    GameObject.Destroy(gameObject);
                }
            }
        }else if (CameraResolution.IsColliderFullyInCameraView(collider))
        {
            weapons[0].Active = true;
        }
        if(Time.time < startTime + StayOnSceneTime)
        {
            if (!CameraResolution.IsColliderFullyInCameraView(collider))
            {

            if (transform.position.x > 0) velocity.x = -2;
            else velocity.x = 2;
            }
            else
            {
                velocity.x = 0;
            }
        }
        rigidbody.velocity = velocity;
    }
}
