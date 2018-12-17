using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class ScaleableFunction : MonoBehaviour {

    public float speed;
    public Vector3 startSize;
    public Vector3 endSize;
    public bool isUseBoxColliderScaleAsEndSize = true;
    public bool isAutoScaling = true;

    private float time;
    private bool isGoing;
    private Vector3 centerPoint;
    private Vector3 _scale = Vector3.zero;
    // Use this for initialization
    void Start()
    {
        initialize();
    }

    // Update is called once per frame
    void Update()
    {
        if(isAutoScaling)
            scaling();
        else{
            if (isGoing)
                return;
            time -= Time.deltaTime * speed;
            if (time <= 0)
            {
                time = 0;
                isGoing = true;
            }
            _scale = Vector3.Lerp(startSize, endSize, time);
            this.transform.localScale = _scale;
        }
    }

    public void initialize()
    {
        
        //attribute seting 
        time = 0;
        speed = 1;
        startSize = Vector3.one;

        if (isAutoScaling)
            isGoing = true;
        
        if(isUseBoxColliderScaleAsEndSize)
            endSize = GetComponent<BoxCollider>().bounds.size;
        
        GetComponent<BoxCollider>().size = startSize;
        GetComponent<BoxCollider>().center = Vector3.zero;
        //centerPoint = (startPoint.position + endPoint.position) / 2 + this.transform.parent.position;


    }
    //private void OnTriggerEnter(Collider other)
    //{
    //    if (isAutoScaling)
    //        return;

    //    if (other.gameObject.tag != "Player")
    //        return;

    //    //isGoing = true;
    //}
    private void OnTriggerStay(Collider other)
    {
        if (isAutoScaling)
            return;

        if (other.gameObject.tag != "Player")
            return;

        //Vector3 _scale = Vector3.zero;

        time += Time.deltaTime * speed;
        if (time >= 1)
        {
            time = 1;
        }
         
        _scale = Vector3.Lerp(startSize, endSize, time);
        this.transform.localScale = _scale;
    }
    private void OnTriggerExit(Collider other)
    {
        if (isAutoScaling)
            return;

        if (other.gameObject.tag != "Player")
            return;

        isGoing = false;
    }



    public void scaling()
    {
        //Vector3 _scale =Vector3.zero;
        if (isGoing)
        {
            time += Time.deltaTime * speed;
            if (time >= 1)
            {
                time = 1;
                isGoing = false;
            }
        }
        else
        {
            time -= Time.deltaTime * speed;
            if (time <= 0)
            {
                time = 0;
                isGoing = true;
            }
        }
        _scale = Vector3.Lerp(startSize, endSize, time);
        this.transform.localScale = _scale;
    }

}
