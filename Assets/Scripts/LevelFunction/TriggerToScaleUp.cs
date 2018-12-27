using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Build.Content;
using System.Globalization;
using System.Xml.Schema;

public class TriggerToScaleUp : MonoBehaviour {

    public GameObject ScaleTarget;

    [Range(1,10)][SerializeField]
    int magScaleY = 1;
    [SerializeField][Header("長到最高需要的時間")]
    float growingTime = 5f;
    float time = 0;
    private bool isGoing = true;
    private bool _isTrigger;
    public  bool isTrigger
    {
        get { return _isTrigger; }
    }
    private Vector3 original_scale;
    Vector3 _scale;

	// Use this for initialization
	void Start () {
        if (ScaleTarget == null)
        {
            Debug.LogError(ScaleTarget);
            return;
        }
        original_scale = ScaleTarget.transform.localScale;
	}

    private void FixedUpdate()
    {
        if (ScaleTarget == null) 
        {
            Debug.LogError(ScaleTarget);
            return;
        }


        if (_isTrigger)
        {
            _scale = Vector3.Lerp(original_scale, new Vector3(original_scale.x,original_scale.y * magScaleY,original_scale.z), time/growingTime);

            if (time / growingTime <= 1)
            {
                time += Time.deltaTime;
            }
        }
        else
        {
            _scale = Vector3.Lerp(original_scale, new Vector3(original_scale.x, original_scale.y * magScaleY, original_scale.z), time / growingTime);

            if (time / growingTime >= 0)
            {
                time -= Time.deltaTime;
            }
        }

        ScaleTarget.transform.localScale = _scale;

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag != "Player") return;
        _isTrigger = true;

    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag != "Player") return;
        _isTrigger = true;

    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag != "Player") return;
        _isTrigger = false;

    }
}
