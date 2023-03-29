using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayCircle : MonoBehaviour
{
    public float time_of_day = 0.5f;
    public float day_len = 30f;
    public Light Sun;
    public Light Moon;
    public AnimationCurve sun_curve;
    private float sun_intensity;

    void Start()
    {
        sun_intensity = Sun.intensity;
    }

    void Update()
    {
        time_of_day += Time.deltaTime / day_len;
        if (time_of_day > 1) time_of_day--;

        RenderSettings.fogDensity = sun_curve.Evaluate(time_of_day) * 0.03f;
        Sun.transform.localRotation = Quaternion.Euler(time_of_day * 360, 180, 0);
        Sun.intensity = sun_intensity * sun_curve.Evaluate(time_of_day);
        Moon.transform.localRotation = Quaternion.Euler(time_of_day * 360 + 180, 180, 0);
    }
}
