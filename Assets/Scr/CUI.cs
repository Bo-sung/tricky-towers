using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CUI : MonoBehaviour {

    public struct UI
    {
        public float x;
        public float y;
    }
    UI ui;
    public void SetCamX(float _x) { ui.x = _x; }
    public void SetCamY(float _y) { ui.y = _y; }

    public float GetCamX() { return ui.x; }
    public float GetCamY() { return ui.y; }

    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}
}
