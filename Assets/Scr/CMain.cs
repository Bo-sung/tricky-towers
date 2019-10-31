using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CMain : MonoBehaviour
{

    private float Nowtop = 0.0f;
    public bool blockadd = true;

    public GameObject Cameras;
    public GameObject tetris0;
    public GameObject tetris1;
    public GameObject tetris2;
    public GameObject tetris3;
    public GameObject tetris4;
    public GameObject tetris5;
    public GameObject tetris6;
    public GameObject UI1;
    public GameObject UI2;

    //UI
    public struct UI_Display
    {
        public float now_x;
        public float now_y;
        public float next_x;
        public float next_y;
        public float next_gap;
    }
    UI_Display CUI;
    public void Init_CUI()
    {
        CCam cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CCam>();
        CUI.now_x = Cellsize * 8.5f;
        CUI.now_y = cam.GetCamY() + 3.5f;
        CUI.next_x = 13.5f;
        CUI.next_y = cam.GetCamY();
        CUI.next_gap = 2f;
    }
    public void SetUI_NEXT_Y(float _next_y) { CUI.next_y = _next_y; }
    
    
    
    void Awake()
    {
        CCam cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CCam>();        
        CUI.now_x = Cellsize * 8.5f;
        CUI.now_y = cam.GetCamY() + 3.5f;
        CUI.next_x = 13.5f;
        CUI.next_y = cam.GetCamY() + 0.54f;
        CUI.next_gap = 1.6f;
    }

    enum mapType
    {

    }
    const int mapX = 17;
    const int mapY = 15;
    const float Cellsize = 0.64f;
    int[,] map = {
        { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,},       //1
        { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,},       //2
        { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,},       //3
        { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,},       //4
        { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,},       //5
        { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,},       //6
        { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,},       //7
        { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,},       //8
        { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,},       //9
        { 0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,},       //10
        { 0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,},       //11
        { 0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,},       //12
        { 0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,},       //13
        { 0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,},       //14
        { 0,0,0,0,0,0,0,1,1,1,0,0,0,0,0,0,0 }
      //{ 1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1 },       //15
      //{ 9,8,7,6,5,4,3,2,1,2,3,4,5,6,7,8,9 },
    };
    public float GetCellSize() { return Cellsize; }
    public int GetMapSize_X() { return mapX; }
    public int GetMapSize_Y() { return mapY; }

    List<GameObject> blockArr = new List<GameObject>();
    List<int> blockorder = new List<int>();//7번쨰 자리마다 -1;
    List<int> randarr = new List<int>();
    List<GameObject> TetrisArr = new List<GameObject>();
    List<GameObject> GB = new List<GameObject>();
    List<GameObject> HOLD = new List<GameObject>();
    // Use this for initialization
    public void TetrisArr_Remove(int _arr)
    {
        TetrisArr.RemoveAt(_arr);
    }    
    void Random7()
    {
        randarr.Add(0); randarr.Add(1); randarr.Add(2);
        randarr.Add(3); randarr.Add(4); randarr.Add(5);
        randarr.Add(6);
        for (int i = 0; i < 7; ++i)
        {
            int rand = Random.Range(0, randarr.Count);
            blockorder.Add(randarr[rand]);
            randarr.RemoveAt(rand);
        }
        blockorder.Add(7);
    }
    public void NextBlockArr()
    {
        for (int i = 0; i < GB.Count; ++i)
        {            
            if(i > 2)
            {
                GB[0].GetComponent<CBlock>().BlockTeleport(CUI.now_x, CUI.now_y, -2);
                GB[0].GetComponent<CBlock>().SetBlocks_DISPLAY(false);
                GB[0].GetComponent<CBlock>().SetBlocks_STATE(true);
                GB.RemoveAt(0);
                i = 0;
            }
            GB[i].GetComponent<CBlock>().BlockTeleport(GB[i].GetComponent<CBlock>().transform.position.x, CUI.next_y + (CUI.next_gap * (2 - i)), -2);
            
        }
    }
    private void Generating_Block(int _order, float _x, float _y, float _z)
    {
        GameObject nextblock;
        if (_order == 0)
        {
            nextblock = Instantiate(tetris0, new Vector3(_x - 0.3f, _y - 0.2f, _z), Quaternion.identity);
            CBlock cblock = nextblock.GetComponent<CBlock>();
            cblock.SetBlocks_Gen();
            cblock.SetBlocks_DISPLAY(true);
            TetrisArr.Add(nextblock);
            GB.Add(nextblock);
        }
        if (_order == 1)
        {
            nextblock = Instantiate(tetris1, new Vector3(_x, _y, _z), Quaternion.identity);
            CBlock cblock = nextblock.GetComponent<CBlock>();
            cblock.SetBlocks_Gen();
            cblock.SetBlocks_DISPLAY(true);
            TetrisArr.Add(nextblock);
            GB.Add(nextblock);
        }
        if (_order == 2)
        {
            nextblock = Instantiate(tetris2, new Vector3(_x, _y - 0.3f, _z), Quaternion.identity);
            CBlock cblock = nextblock.GetComponent<CBlock>();
            cblock.SetBlocks_Gen();
            cblock.SetBlocks_DISPLAY(true);
            TetrisArr.Add(nextblock);
            GB.Add(nextblock);
        }
        if (_order == 3)
        {
            nextblock = Instantiate(tetris3, new Vector3(_x, _y, _z), Quaternion.identity);
            CBlock cblock = nextblock.GetComponent<CBlock>();
            cblock.SetBlocks_Gen();
            cblock.SetBlocks_DISPLAY(true);
            TetrisArr.Add(nextblock);
            GB.Add(nextblock);
        }
        if (_order == 4)
        {
            nextblock = Instantiate(tetris4, new Vector3(_x, _y, _z), Quaternion.identity);
            CBlock cblock = nextblock.GetComponent<CBlock>();
            cblock.SetBlocks_Gen();
            cblock.SetBlocks_DISPLAY(true);
            TetrisArr.Add(nextblock);
            GB.Add(nextblock);
        }
        if (_order == 5)
        {
            nextblock = Instantiate(tetris5, new Vector3(_x, _y, _z), Quaternion.identity);
            CBlock cblock = nextblock.GetComponent<CBlock>();
            cblock.SetBlocks_Gen();
            cblock.SetBlocks_DISPLAY(true);
            TetrisArr.Add(nextblock);
            GB.Add(nextblock);
        }
        if (_order == 6)
        {
            nextblock = Instantiate(tetris6, new Vector3(_x, _y, _z), Quaternion.identity);
            CBlock cblock = nextblock.GetComponent<CBlock>();
            cblock.SetBlocks_Gen();
            cblock.SetBlocks_DISPLAY(true);
            TetrisArr.Add(nextblock);
            GB.Add(nextblock);
        }

        //cblock.SetBlocks_TYPE(_order);
        // blockArr.Add(blocks);
    }
    private float blockstop()
    {
        float top = 0.0f;
        for (int i = 0; i < TetrisArr.Count - 1; ++i)
        {
            if (TetrisArr[i].transform.position.y >= TetrisArr[i + 1].transform.position.y)
            {
                top = TetrisArr[i].transform.position.y;
            }
        }
        Debug.Log("top : " + top);
        top = top - (top % 0.5f);
        return top;
    }

    void Start()
    {
        CCam cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CCam>();
        Init_CUI();
        for (int y = 0; y < mapY; ++y)
        {
            for (int x = 0; x < mapX; ++x)
            {
                if (map[y, x] == 0)
                {
                    continue;
                }
                GameObject block = (GameObject)Instantiate(GameObject.FindWithTag("wallblock1"));
                block.transform.SetPositionAndRotation(new Vector3(x * Cellsize, (mapY - y - 1) * Cellsize, -1), Quaternion.identity);
                blockArr.Add(block);
            }
        }
        Random7();
        //for (int i = 0; i < 3; ++i)
        //{
        //    Generating_Block(blockorder[0], CUI.next_x,CUI.next_y, -2);
        //    blockorder.RemoveAt(0);
        //}
    
    }
    // Update is called once per frame
    void Update()
    {

        CCam cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CCam>();
        Init_CUI();
        NextBlockArr();
        if (blockorder[0] == 7)
        {
            Random7();
            blockorder.RemoveAt(0);
        }
        if(GB.Count < 4)
        {
            blockadd = true;
        }
        //float blocktop = 0f;
        //if (TetrisArr.Count >= 2)
        //{
        //    blocktop = blockstop();
        //}
        //if(blocktop >= 7)
        //{
        //    if(camposy <= blocktop)
        //    {                
        //        cam.CamMove(blocktop);
        //        float camposs = blocktop;
        //        Debug.Log("cam_pos"+camposs);
        //    }
        //    else if(camposy > blocktop)
        //    {
        //       // cam.CamMove(7);
        //    }
        //}
        Debug.Log(cam.GetCamY());
        if (blockadd)
        {
            if (Input.GetKeyUp(KeyCode.C))
            {
                Generating_Block(blockorder[0], CUI.next_x, CUI.next_y, -2);
                blockorder.RemoveAt(0);
                blockadd = false;
            }
            if (Input.GetKeyUp(KeyCode.V))
            {
                Generating_Block(blockorder[0], CUI.next_x, CUI.next_y, -2);
                blockadd = false;
            }
        }
        

       for (int i = 0; i < TetrisArr.Count - 1; ++i)
       {
           if (TetrisArr[i].transform.position.y < -3)
           {
                TetrisArr[i].GetComponent<CBlock>().objDestroy();
                TetrisArr.RemoveAt(i);
           }
       }
       if(TetrisArr[0].transform.position.y <-3)
        {
            Destroy(TetrisArr[0]);
            TetrisArr.RemoveAt(0);
        }        
        else
        {
        }
    }
}

