using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CBlock : MonoBehaviour {
       
    PolygonCollider2D rp;
    Rigidbody2D rb;
    Renderer rd;
    Animator ani;
    GameObject block;

    private int time = 0;
    private struct Blocks
    {
        public float x; //x pos
        public float y; //y pos
        public int rot; //블럭 방향
        public float mass;  //무계
        public bool state;  //상태
        public bool display;    //출력
        public bool Genesis;
    }
    private struct Maps
    {
        public float max_x;
        public float min_x;
        public float Cellsize;
    }
    public bool destroy = false;
    Blocks blocks;
    Maps map;
    
    // Use this for initialization
    public float GetBlocks_X() { return blocks.x; }
    public float GetBlocks_Y() { return blocks.y; }
    public float GetBlocks_MASS() { return blocks.mass; }
    public int GetBlocks_ROT() { return blocks.rot; }
    public bool GetBlocks_STATE() { return blocks.state; }
    public bool GetBlocks_DISPLAY() { return blocks.display; }

    public void SetBlocks_X(float _x) { blocks.x = _x; }
    public void SetBlocks_Y(float _y) { blocks.y = _y; }
    public void SetBlocks_MASS(float _mass) { blocks.mass = _mass; }
    public void SetBlocks_ROT(int _rot) { blocks.rot = _rot; }
    public void SetBlocks_STATE(bool _state) { blocks.state = _state; }
    public void SetBlocks_Gen() { blocks.Genesis = false; }    
    public void SetBlocks_DISPLAY(bool _disp)
    {
        blocks.display = _disp;
        if (_disp)
        {
            SetBlocks_STATE(false);
        }        
    }
    public void objDestroy()
    {
        Destroy(gameObject);
    }
    public void BlockTeleport(float _x, float _y, float _z)
    {
        transform.position = new Vector3(_x, _y, _z);
    }
    void Awake()
    {
        map.max_x = 10.64f;
        map.min_x = 0.64f;
        map.Cellsize = 0.64f;//cmain.GetCellSize();
        SetBlocks_STATE(false);
        SetBlocks_ROT(1);
        blocks.Genesis = true;
        ani = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        rp = GetComponent<PolygonCollider2D>();
        rb.gravityScale = 0;
        rp.isTrigger = true;
        //Debug.Log("awake");
    }

    void Start ()
    {
    }
    public void toggle_4_P()
    {
        if (blocks.rot == 4)
        {
            blocks.rot = 1;
        }
        else
        {
            blocks.rot++;
        }
    }
    public void toggle_4_M()
    {
        if (blocks.rot == 4)
        {
            blocks.rot = 1;
        }
        else
        {
            blocks.rot--;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
        blocks.x = transform.position.x;
        blocks.y = transform.position.y;
        if (blocks.display)
        {
            blocks.state = false;
        }
        else if (!blocks.Genesis)
        {
            if (transform.position.x <= 0.64)
            {
                transform.position = new Vector3(blocks.x + (map.Cellsize / 2), blocks.y, transform.position.z);
            }
            if (transform.position.x >= 10.64)
            {
                transform.position = new Vector3(blocks.x - (map.Cellsize / 2), blocks.y, transform.position.z);
            }
        }
        if (blocks.state)
        {
            transform.position = new Vector3(blocks.x, blocks.y - 0.02f, transform.position.z);
            if (Input.GetKeyDown(KeyCode.A))
                transform.position = new Vector3(blocks.x - (map.Cellsize / 2), blocks.y, transform.position.z);
            if (Input.GetKeyDown(KeyCode.D))
                transform.position = new Vector3(blocks.x + (map.Cellsize / 2), blocks.y, transform.position.z);
            if (Input.GetKey(KeyCode.S))
                transform.position = new Vector3(blocks.x, blocks.y - 0.09f, transform.position.z);

            if (Input.GetKeyDown(KeyCode.Q))
            {
                transform.rotation = Quaternion.Euler(0, 0, 90 * blocks.rot);
                toggle_4_P();
            }
            if (Input.GetKeyDown(KeyCode.E))
            {
                transform.rotation = Quaternion.Euler(0, 0, -90 * blocks.rot);
                toggle_4_M();
            }
        }
        
    }
    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag.Equals("wallblock1"))
        {
            if (!blocks.Genesis)
            {
                if (blocks.state)
                {
                        CMain cmain = GameObject.Find("CMain").GetComponent<CMain>();
                        cmain.blockadd = true;
                }
            }
            blocks.state = false;
            rb.gravityScale = 1;
            rp.isTrigger = false;
            
        }
        if (coll.gameObject.tag.Equals("tetris_block"))
        {
            if (!blocks.Genesis)
            {
                time++;
                if (time >= 50)
                {
                    CMain cmain = GameObject.Find("CMain").GetComponent<CMain>();
                    cmain.blockadd = true;
                }
            }
            blocks.state = false;
            rb.gravityScale = 1;
            rp.isTrigger = false;
        }
        
    }
}
