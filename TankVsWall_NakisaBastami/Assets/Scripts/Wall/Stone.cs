using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stone : MonoBehaviour
{
    static float baseHitDuration = 0;
    static bool calculateColorDelta = false;

    const int timeDivisionFactor = 2;

    Material material = null;
    Color myBaseColor = new Color();
    Color colorDelta = new Color();
    public Color GetColor => material.color;
    float hitDuration = 0;
    public bool IsHit => hitDuration > 0;

    void Awake()
    {
        material = GetComponent<MeshRenderer>().material;
    }

    public static void SetHitDuration(float _duration)
    {
        baseHitDuration = _duration;
        calculateColorDelta = true;
    }

    public void Hit(Color _color)
    {
        if (IsHit)
            return;
        material.color = _color;
        hitDuration = baseHitDuration;
    }

    public void SetBaseColor(Color _color)
    {
        myBaseColor = _color;
        material.color = myBaseColor;
        CalculateColorDelta();
    }
    
    /// <summary>
    /// Calculate change rate for color per second
    /// </summary>
    void CalculateColorDelta()
    {
        colorDelta = myBaseColor / baseHitDuration * timeDivisionFactor;
        calculateColorDelta = false;
    }

    private void Update()
    {
        if (calculateColorDelta)
            CalculateColorDelta();
        if(IsHit)
        {
            hitDuration -= Time.deltaTime;
            if(hitDuration < baseHitDuration / timeDivisionFactor)
            {
                material.color += colorDelta * Time.deltaTime;
            }
        }
    }
}
