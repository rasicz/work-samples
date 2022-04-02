using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiHealth : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject healthBar;
    public GameObject healthImage;
    public GameObject[] images;
    public Sprite[] sprites;
    private HealthManager healthManager;
    int health;
    void Start()
    {
        healthManager = GameObject.Find("PlayerShip").GetComponent<HealthManager>();
        health = healthManager.health;
        SetupHealth(healthManager.health);
        //ChangeHealth();
    }

    // Update is called once per frame
    void Update()
    {
        if (healthManager == null) return;
        if(health != healthManager.health)
        {
            health = healthManager.health;
            ChangeHealth();
        }
    }

    private void SetupHealth(int count)
    {
        count = Mathf.Clamp(count, 1, 10);
        float positionY = (-100 * count / 2) + 50;
        //Vector2 minPosition = new Vector2(0, 0.5F - sizeY * count / 2);
        //Vector2 maxPosition = minPosition;
        //maxPosition.y += sizeY;
        if (healthImage == null) return;
        for(int i = 0; i < count; i++)
        {
            Debug.Log("--setting up--");
            GameObject health = GameObject.Instantiate(healthImage, healthBar.transform);
            health.GetComponent<RectTransform>().anchorMin = new Vector2(0, 0.5F);
            health.GetComponent<RectTransform>().anchorMax = new Vector2(1, 0.5F);
            health.GetComponent<RectTransform>().localPosition = new Vector3(0, positionY);

            images[i] = health;
            positionY += 100;
        }
    }
    private void ChangeHealth()
    {
        for(int i = 0; i < images.Length; i++)
        {
            if (i < health) images[i].GetComponent<Image>().sprite = sprites[1];
            else images[i].GetComponent<Image>().sprite = sprites[0];
        }
    }
}
