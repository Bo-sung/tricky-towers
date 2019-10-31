using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CCam : MonoBehaviour {

    
    // Use this for initialization
    public struct CAMERA
    {
        public float x;
        public float y;
        public bool Lock;
    }
    CAMERA cam;
    public void SetCamX(float _x) { cam.x = _x; }
    public void SetCamY(float _y) { cam.y = _y; }
    public void SetCamLock(bool _on) { cam.Lock = _on; }

    public float GetCamX() { return cam.x; }
    public float GetCamY() { return cam.y; }
    public bool GetCamLock() { return cam.Lock; }

    void Awake()
    {
        cam.x = transform.position.x;
        cam.y = transform.position.y;
        CMain cmain = GameObject.Find("CMain").GetComponent<CMain>();
        if (cmain.blockadd)
            SetCamLock(true);
        else
            SetCamLock(false);
    }
    void Start ()
    {
        cam.x = transform.position.x;
        cam.y = transform.position.y;
    }
    public void CamMove(float _y)
    {        
        if (!cam.Lock)
        {
            CMain cmain = GameObject.Find("CMain").GetComponent<CMain>();
            cmain.Init_CUI();
            cmain.SetUI_NEXT_Y(transform.position.y);
            cmain.NextBlockArr();
            transform.position = new Vector3(transform.position.x, _y, transform.position.z);
                       
        }
    }
        
	
	// Update is called once per frame
	void Update ()
    {
        Debug.Log(GetCamY());
        cam.x = transform.position.x;
        cam.y = transform.position.y;
        if (Input.GetKeyUp(KeyCode.UpArrow))
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + 1, transform.position.z);
        }
        if (Input.GetKeyUp(KeyCode.DownArrow))
        {
            transform.position = new Vector3(transform.position.x, transform.position.y - 1, transform.position.z);
        }
    }
}

