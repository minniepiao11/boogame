using System.Collections.Generic;
using UnityEngine;
using Photon;
using UnityEditor;
using System.Linq;

public class CenterDataSync : PunBehaviour{
    #region public attribute
    public bool isSyncInvisible = true;
    public bool isSyncMove = true;
    public bool isSyncScale = true;

    #endregion

    #region define 3 types of Level Object
    private Invisible _invisible = new Invisible();
    class Invisible
    {
        public bool isActive;
        public GameObject[] invisibleObjects;

    }
    private Move _move = new Move();
    struct Move
    {
        public bool isActive;
        public GameObject[] moveObjects;
    }
    private Scale _scale = new Scale();
    struct Scale
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

        if(_invisible.invisibleObjects == null ||
           _move.moveObjects == null ||
           _scale.scaleObjects == null)
        {
            Debug.LogWarning("CenterDataSync : You forget to setting tags of the GameObject");
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
            foreach (GameObject _scaleObject in _move.moveObjects)
            {
                Vector3 scale = (Vector3)stream.ReceiveNext();
                _scaleObject.transform.localScale = scale;;
            }
            foreach ( GameObject _invisibleObject in _invisible.invisibleObjects)
            {
                bool active = (bool)stream.ReceiveNext();
                _invisibleObject.GetComponent<MeshRenderer>().enabled = active;
            }
            foreach (GameObject _moveObject in _move.moveObjects)
            {
                Vector3 position = (Vector3)stream.ReceiveNext();
                _moveObject.transform.position = position;
            }
        }
    }
}
