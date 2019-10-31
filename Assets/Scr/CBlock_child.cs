using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CBlock_child : MonoBehaviour {

    
    Renderer rd;
    Animator ani;
    Rigidbody2D rb;

    private struct Child_Blocks
    {
        public float x; //x pos
        public float y; //y pos
        public int rot; //블럭 방향
        public bool state;  //상태
        public bool display;    //출력
        public int type;
    }
    Child_Blocks blocks;
    // Use this for initialization
    public float GetBlocks_X() { return blocks.x; }
    public float GetBlocks_Y() { return blocks.y; }
    public int GetBlocks_ROT() { return blocks.rot; }
    public bool GetBlocks_STATE() { return blocks.state; }
    public bool GetBlocks_DISPLAY() { return blocks.display; }
    public int GetBlocks_TYPE() { return blocks.type; }

    public void SetBlocks_X(float _x) { blocks.x = _x; }
    public void SetBlocks_Y(float _y) { blocks.y = _y; }
    public void SetBlocks_ROT(int _rot) { blocks.rot = _rot; }
    public void SetBlocks_STATE(bool _state) { blocks.state = _state; }
    public void SetBlocks_DISPLAY(bool _disp) { blocks.display = _disp; }
    public void SetBlocks_TYPE(int _type) { blocks.type = _type; }

    void Start()
    {
        ani = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        SetBlocks_STATE(true);
        SetBlocks_ROT(1);
        
    }
    // Update is called once per frame
    void Update ()
    {
        if(blocks.state)
        {
            Debug.Log(blocks.state);
            rb.gravityScale = 0;
            transform.position.Set((transform.position.x), transform.position.y -1, transform.position.z);
        }
	}
}
