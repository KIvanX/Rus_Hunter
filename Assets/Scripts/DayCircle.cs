using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DayCircle : MonoBehaviour
{
    public static int NUM_WOLFS = 15;
    public static int NUM_RESOUCES = 100;
    public float time_of_day = 0.2f;
    public Light Sun;
    public Light Moon;
    public AnimationCurve sun_curve;
    private float sun_intensity;
    public GameObject wolf_obj;
    public GameObject wood, stone;
    public Terrain terrain;
    public GameObject menu;


    void Start()
    {
        for (int i=0; i<NUM_RESOUCES; i++) create_resouce();
        for (int i=0; i<NUM_WOLFS; i++) create_wolf();
        sun_intensity = 1;
    }

    void Update()
    {
        time_of_day += Time.deltaTime * (DataHolder.day_speed / 300f + 0.001f);
        if (time_of_day > 1) time_of_day--;
        
        if (time_of_day < 0.6) DataHolder.is_night = false; else DataHolder.is_night = true;

        if (DataHolder.num_wolfs < NUM_WOLFS && Random.Range(0, 500) < 1f) create_wolf();
        
        if (DataHolder.num_resurces < NUM_RESOUCES && Random.Range(0, 100) < 1f) create_resouce();

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

    void create_resouce() 
    {
        float x = 0, z = 0;
        while (x + z < 100) 
        {
            x = Random.Range(0, 200f);
            z = Random.Range(0, 200f);
        }
        float y = terrain.SampleHeight(new Vector3(x, 0f, z));
        if (Random.Range(0, 2) < 1)
            Instantiate(stone, new Vector3(x, y, z), Quaternion.identity);
        else
            Instantiate(wood, new Vector3(x, y, z), Quaternion.identity);
        DataHolder.num_resurces = DataHolder.num_resurces + 1;
    }
}
