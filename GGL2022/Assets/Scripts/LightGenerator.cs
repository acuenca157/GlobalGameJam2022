using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightGenerator : MonoBehaviour
{

    // Start is called before the first frame update
    public Transform[] getPoints(int n)
    {
        Transform[] pointsAr = GetComponentsInChildren<Transform>();
        ArrayList points = new ArrayList(pointsAr);
        Transform[] final = new Transform[n];
        Random.InitState((int)Mathf.Floor(System.DateTime.Now.Second));
        for (int i = 0; i < n; i++) {
            int k = Random.Range(1, points.Count);
            final[i] = (Transform) points[k];
            points.RemoveAt(i);
        }
        return final;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
