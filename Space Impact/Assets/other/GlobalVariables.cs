using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class GlobalVariables
{
    /// <summary>
    /// 0 - player, 1 - enemy, 2 - playerProjectile, 3 - enemyProjectile
    /// </summary>
    public static int[] layers;
    [HideInInspector]
    public static float playerVerticalSpeed;
    [HideInInspector]
    public static float backgroundOffset;
    public static Vector2 toVector2(Vector3 vector)
    {
        return new Vector2(vector.x, vector.y);
    }
    public static void ChangeScene(int scene)
    {
        SceneManager.LoadScene(scene);
    }

    public static float reloadSpead = 1;
    public static float bossHealth = 1;
    public static float dropChance = 1;

    public static float shipsStartSpeed = 0;

    public static float playAreaSize = Camera.main.orthographicSize * Camera.main.aspect - 0.4F;

    private static int gameDifficulty;
    /// <summary>
    /// 0 - easy, 1 - medium, 2 - hard
    /// </summary>
    public static int GameDifficulty
    {
        get
        {
            return gameDifficulty;
        }
        set
        {
            if (value < 0 || value > 2) return;
            SetDifficulty(value);
            gameDifficulty = value;
        }
    }
    public static void SetDifficulty(int difficulty)
    {
        switch (difficulty)
        {
            case 0:
                reloadSpead = 0.8F;
                bossHealth = 0.8F;
                dropChance = 1.2F;
                break;
            case 1:
                reloadSpead = 1.0F;
                bossHealth = 1.0F;
                dropChance = 1.0F;
                break;
            case 2:
                reloadSpead = 1.0F;
                bossHealth = 1.2F;
                dropChance = 0.8F;
                break;
        }
    }
    private static bool paused;
    public static bool Paused
    {
        get { return paused; }
        set
        {
            Time.timeScale = value ? 0 : 1;
            paused = value;
        }
    }
    static System.Random random = new System.Random(Guid.NewGuid().GetHashCode());
    public static float RandomFloat(float a, float b)
    {
        return RandomFloat(a, b, 10);
    }
    public static float RandomFloat(float a, float b, int multiplier)
    {
        a *= multiplier;
        b *= multiplier;
        int r = random.Next((int)a, (int)b);
        return (float)r / (float)multiplier;
    }
    public static IEnumerator ChangeCanvasAlpha(CanvasGroup canvas, float time, bool additive)
    {
        float alpha = additive ? 0 : 1;
        bool alphaChanged = false;
        while (!alphaChanged)
        {
            if (additive && alpha >= 1) alphaChanged = true;
            else if (!additive && alpha <= 0) alphaChanged = true;
                alpha += additive ? 0.05F : -0.05F;
            canvas.alpha = alpha;
            yield return new WaitForSeconds(time * 0.05F);
        }
        alpha = additive ? 1 : 0;
        canvas.alpha = alpha;
    }
}
