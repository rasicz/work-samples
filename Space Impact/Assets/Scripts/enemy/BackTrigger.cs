using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackTrigger : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject[] objects;
    public float[] approachSpeed;
    public enum Position {
        left,
        right,
        down,
    }
    public Position[] positions;
    Renderer renderer;
    void Start()
    {
        renderer = gameObject.GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (renderer.isVisible)
        {
            for(int i = 0; i < objects.Length; i++)
            {
                Vector2 vector;
                float speed = approachSpeed.Length > i ? approachSpeed[i] : 1.0F;
                switch (positions[i])
                {
                    case Position.left :
                        vector = new Vector2(speed, GlobalVariables.playerVerticalSpeed);
                        break;
                    case Position.right:
                        vector = new Vector2(speed * -1, GlobalVariables.playerVerticalSpeed);
                        break;
                    case Position.down:
                        objects[i].transform.position = new Vector2(0, Camera.main.transform.position.y - Camera.main.orthographicSize - 1);
                        vector = new Vector2(0, GlobalVariables.playerVerticalSpeed + speed);
                        Debug.Log("Speed: " + speed);
                        break;
                    default:
                        vector = Vector2.zero;
                        break;
                }
                objects[i].GetComponent<Rigidbody2D>().velocity = vector;

                if(objects[i].GetComponent<EnemyAi>() != null)
                objects[i].GetComponent<EnemyAi>().active = true;

                if(objects[i].GetComponent<AdvancedEnemyAi>() != null)
                objects[i].GetComponent<AdvancedEnemyAi>().active = true;
            }
            Destroy(gameObject);
        }
    }
}
