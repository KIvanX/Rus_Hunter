using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DayCircle : MonoBehaviour
{
    public float time_of_day = 0.2f;
    public Light Sun;
    public Light Moon;
    public AnimationCurve sun_curve;
    private float sun_intensity;
    public GameObject wolf_obj;
    public Terrain terrain;
    public GameObject menu;


    void Start()
    {
        for (int i=0; i<15; i++) create_wolf();
        sun_intensity = 1;
    }

    void Update()
    {
        time_of_day += Time.deltaTime * (DataHolder.day_speed / 300f + 0.001f);
        if (time_of_day > 1) time_of_day--;
        
        if (time_of_day < 0.6) DataHolder.is_night = false; else DataHolder.is_night = true;

        if (DataHolder.num_wolfs < 15) {
            if (Random.Range(0, 5) < 1f) create_wolf();
        }

        RenderSettings.fogDensity = sun_curve.Evaluate(time_of_day) * 0.03f;
        Sun.transform.localRotation = Quaternion.Euler(time_of_day * 360, 180, 0);
        Sun.intensity = sun_intensity * sun_curve.Evaluate(time_of_day);
        Moon.transform.localRotation = Quaternion.Euler(time_of_day * 360 + 180, 180, 0);
    }

    void create_wolf() 
    {
        float x = 0, z = 0;
        while (x + z < 100) 
        {
            x = Random.Range(0, 200f);
            z = Random.Range(0, 200f);
        }
        float y = terrain.SampleHeight(new Vector3(x, 0f, z));
        GameObject wolf = Instantiate(wolf_obj, new Vector3(x, y, z), Quaternion.identity);
        DataHolder.num_wolfs = DataHolder.num_wolfs + 1;
    }
}
