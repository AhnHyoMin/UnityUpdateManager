using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CUpdate : MonoBehaviour {

    private Transform m_TrCache = null;

    public void Awake()
    {
        m_TrCache = this.transform;
    }


    // Update is called once per frame
    void Update () {
        m_TrCache.position += Vector3.forward * Time.deltaTime * 4;

    }
}
