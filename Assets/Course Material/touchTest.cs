using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class touchTest : MonoBehaviour {
    PhotonView view;

    Ray ray;
    RaycastHit hit;
    // Use this for initialization
    public GameObject ex;
    public GameObject oh;
    private float count = 0;

    void Start () {
        view = GetComponent<PhotonView>();
    }

    // Update is called once per frame
    void Update() 
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            Debug.DrawRay(ray.origin, ray.direction * 20, Color.red);

            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                Debug.Log("Count" + count);
                view.RPC("exoh_prefab", PhotonTargets.All, hit.transform.position, Quaternion.identity);
            }
        }
	}

    [PunRPC]
    void exoh_prefab(Vector3 Pos, Quaternion quaat)
    {
        if (count % 2 == 0)
        {
            GameObject Go = Instantiate(oh, Pos, quaat) as GameObject;
        }
        else if (count % 1 == 0)
        {
            GameObject Go = Instantiate(ex, Pos, quaat) as GameObject;
        }
        count++;
    }
  


}
