using System.Collections.Generic;
using UnityEngine;
using Photon;
using UnityEditor;
using System.Linq;
using System;

public class CenterDataSync : PunBehaviour
{
    #region public attribute
    public bool isSyncInvisible = true;
    public bool isSyncMove = true;
    public bool isSyncScale = true;
    public GameObject[] scaleObjects;
    public GameObject[] invisibleObjects;
    public GameObject[] moveObjects;
    public GameObject GameManager;
    public int Score
    { get { 
            return GameManager.GetComponent<GameManager>().score;
        }}
    public GameStage stage
    { get { return GameManager.GetComponent<GameManager>().stage; }}
    #endregion

    #region define 3 types of Level Object
    public  Invisible _invisible;
    public struct Invisible
    {
        public bool isActive;
        public GameObject[] invisibleObjects;

    }
    public  Move _move = new Move();
    public struct Move
    {
        public bool isActive;
        public GameObject[] moveObjects;
    }
    public  Scale _scale = new Scale();
    public struct Scale
    {
        public bool isActive;
        public GameObject[] scaleObjects;
    }

    #endregion
    void InitializeData()
    {
        _invisible.isActive = isSyncInvisible;
        _move.isActive = isSyncMove;
        _scale.isActive = isSyncScale;

        _invisible.invisibleObjects = GameObject.FindGameObjectsWithTag("invisibleObject");
        _move.moveObjects = GameObject.FindGameObjectsWithTag("moveObject");
        _scale.scaleObjects = GameObject.FindGameObjectsWithTag("scaleObject");
        //scaleObjects = GameObject.FindGameObjectsWithTag("scaleObject");
        //moveObjects = GameObject.FindGameObjectsWithTag("moveObject");
        //invisibleObjects = GameObject.FindGameObjectsWithTag("invisibleObject");

        GameManager = GameObject.FindWithTag("GameManager");
        if(_invisible.invisibleObjects == null ||
           _scale.scaleObjects == null)
        {
            Debug.LogWarning("CenterDataSync : You forget to setting tags of the GameObject");
        }
        if(_move.moveObjects == null)
        {
            
        }
    }
    // Use this for initialization
    void Start () {

        InitializeData();

    }
    
    private void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if(stream.isWriting)
        {
            if(_scale.isActive)
            {
                foreach (GameObject _scaleObject in _scale.scaleObjects)
                {
                    Vector3 scale = _scaleObject.transform.localScale;
                    stream.SendNext(scale);
                }
            }

            if(_move.isActive)
            {
                foreach (GameObject _moveObject in _move.moveObjects)
                {
                    Vector3 position = (Vector3)_moveObject.transform.position;
                    stream.SendNext(position);
                }
            }

            if(_invisible.isActive)
            {
                foreach (GameObject _invisibleObject in _invisible.invisibleObjects)
                {
                    bool acitve = _invisibleObject.GetComponent<MeshRenderer>().enabled;
                    stream.SendNext(acitve);
                }
            }
        }
        else if(stream.isReading)
        {
            if (_scale.isActive)
            {
                foreach (GameObject _scaleObject in _move.moveObjects)
                {
                    Vector3 scale = (Vector3)stream.ReceiveNext();
                    _scaleObject.transform.localScale = scale; ;
                }
            }
            if (_invisible.isActive)
            {
                foreach (GameObject _invisibleObject in _invisible.invisibleObjects)
                {
                    bool active = (bool)stream.ReceiveNext();
                    _invisibleObject.GetComponent<MeshRenderer>().enabled = active;
                }
            }
            if (_move.isActive)
            {
                foreach (GameObject _moveObject in _move.moveObjects)
                {
                    Vector3 position = (Vector3)stream.ReceiveNext();
                    _moveObject.transform.position = position;
                }
            }
        }
    }
}
