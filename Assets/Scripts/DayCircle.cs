using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DayCircle : MonoBehaviour
{
    public float time_of_day = 0.5f;
    public float day_len = 30f;
    public Light Sun;
    public Light Moon;
    public AnimationCurve sun_curve;
    private float sun_intensity;
    public GameObject wolf_obj;
    public Terrain terrain;
    public Image image;


    void Start()
    {
        for (int i=0; i<10; i++) {
            float x = Random.Range(0f, 100f), z = Random.Range(0f, 100f);
            float y = terrain.SampleHeight(new Vector3(x, 0f, z));
            GameObject wolf = Instantiate(wolf_obj);
            wolf.transform.position = new Vector3(x, y, z);
        }
        
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
