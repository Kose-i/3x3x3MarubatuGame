using UnityEngine;
using System.Collections;

public class BallGenerator : MonoBehaviour {

  public GameObject BallPrefab;

  public string NowTurn = "Black";
  public string NextTurn = "White";
  public float xFirstThread = 200;
  public float xSecondThread = 400;
  public float yFirstThread = 200;
  public float ySecondThread = 400;

  public int[,,] BoardData = new int[3,3,3];// 0 (space), 1(black), 2(white)

  // Use this for initialization
  void Start () {
    for (int i = 0;i < 3;++i) {
      for (int j = 0;j < 3;++j) {
        for (int k = 0;k < 3;++k) {
          BoardData[i,j,k] = 0;
        }
      }
    }
  }

  int Isless(float ls, float rs) {
    if (ls < rs) return 1;
    return 0;
  }

  bool checkBoard(int target, int x, int y, int z) {
    if (x < 0 || y < 0 || z < 0 || 2 < x || 2 < y || 2 < z) return false;
    return (BoardData[x,y,z] == target);
  }

  bool isCheck(int x,int y, int z, int target) {
    /*立体斜め*/
    if ((checkBoard(target, x-1,y-1,z-1) && (checkBoard(target, x-2,y-2,z-2) || (checkBoard(target, x+1,y+1,z+1)))) || (checkBoard(target, x+1,y+1,z+1) && (checkBoard(target, x+2,y+2,z+2))) ) return true;
    else if ((checkBoard(target, x+1,y-1,z-1) && (checkBoard(target, x+2,y-2,z-2) || (checkBoard(target, x-1,y+1,z+1)))) || (checkBoard(target, x-1,y+1,z+1) && (checkBoard(target, x-2,y+2,z+2))) ) return true;
    else if ((checkBoard(target, x+1,y+1,z-1) && (checkBoard(target, x+2,y+2,z-2) || (checkBoard(target, x-1,y-1,z+1)))) || (checkBoard(target, x-1,y-1,z+1) && (checkBoard(target, x-2,y-2,z+2))) ) return true;
    else if ((checkBoard(target, x-1,y+1,z-1) && (checkBoard(target, x-2,y+2,z-2) || (checkBoard(target, x+1,y-1,z+1)))) || (checkBoard(target, x+1,y-1,z+1) && (checkBoard(target, x+2,y-2,z+2))) ) return true;
    /*平面斜め*/
    if ((checkBoard(target, x,y-1,z-1) && (checkBoard(target, x,y-2,z-2) || (checkBoard(target, x,y+1,z+1)))) || (checkBoard(target, x,y+1,z+1) && (checkBoard(target, x,y+2,z+2))) ) return true;
    else if ((checkBoard(target, x,y+1,z-1) && (checkBoard(target, x,y+2,z-2) || (checkBoard(target, x,y-1,z+1)))) || (checkBoard(target, x,y-1,z+1) && (checkBoard(target, x,y-2,z+2))) ) return true;
    else if ((checkBoard(target, x-1,y,z-1) && (checkBoard(target, x-2,y,z-2) || (checkBoard(target, x+1,y,z+1)))) || (checkBoard(target, x+1,y,z+1) && (checkBoard(target, x+2,y,z+2))) ) return true;
    else if ((checkBoard(target, x+1,y,z-1) && (checkBoard(target, x+2,y,z-2) || (checkBoard(target, x-1,y,z+1)))) || (checkBoard(target, x-1,y,z+1) && (checkBoard(target, x-2,y,z+2))) ) return true;
    else if ((checkBoard(target, x-1,y-1,z) && (checkBoard(target, x-2,y-2,z) || (checkBoard(target, x+1,y+1,z)))) || (checkBoard(target, x+1,y+1,z) && (checkBoard(target, x+2,y+2,z))) ) return true;
    else if ((checkBoard(target, x+1,y-1,z) && (checkBoard(target, x+2,y-2,z) || (checkBoard(target, x-1,y+1,z)))) || (checkBoard(target, x-1,y+1,z) && (checkBoard(target, x-2,y+2,z))) ) return true;
    /*each equal x,y,z*/
    else if (checkBoard(target, 0,y,z) && checkBoard(target, 1,y,z) && checkBoard(target, 2,y,z) ) return true;
    else if (checkBoard(target, x,0,z) && checkBoard(target, x,1,z) && checkBoard(target, x,2,z) ) return true;
    else if (checkBoard(target, x,y,0) && checkBoard(target, x,y,1) && checkBoard(target, x,y,2) ) return true;
    else return false;
  }

  bool isfinish(string str, int x,int y,int z) {
    //Debug.Log(str+" x:"+x+" y:"+y+" z:"+z);
    if (str == "Black" && isCheck(x,y,z, 1)) {
      Debug.Log("[isfinish]:Black win");
      return true;
    } else if (str == "White" && isCheck(x,y,z,2)){
      Debug.Log("[isfinish]:White win");
      return true;
    } else {
      //Debug.Log("Not knowing str error happend [BallGenerator-isfinished]");
      return false;
    }
  }

  decimal putSelect() {
    if (Input.GetMouseButtonDown(0)) {

      /*Position select*/
      float x_pos = Input.mousePosition.x;
      float y_pos = Input.mousePosition.y;
      int xPos = Isless(xFirstThread, x_pos) + Isless(xSecondThread, x_pos);
      int yPos = Isless(yFirstThread, y_pos) + Isless(ySecondThread, y_pos);

      /*Put GameData*/
      if (!(0<=xPos && xPos<3)) return 0;
      if (!(0<=yPos && yPos<3)) return 0;
      if (BoardData[xPos,yPos,2] != 0) return 0; //max line

      GameObject go = Instantiate(BallPrefab) as GameObject;
      go.transform.position = new Vector3(xPos*2, 10, yPos*2);
      /*color select*/
      if (NowTurn == "Black") {
        int zPos = 0;
        for (int i = 0;i < 3;++i) {
          if (BoardData[xPos,yPos,i] == 0) {
            BoardData[xPos,yPos,i] = 1;
            zPos = i;
            break;
          }
        }
        go.GetComponent<Renderer>().material.color = Color.black;
        if (isfinish("Black", xPos, yPos, zPos)) {
          Debug.Log("Black Win");
        }
      } else {
        int zPos = 0;
        for (int i = 0;i < 3;++i) {
          if (BoardData[xPos,yPos,i] == 0) {
            BoardData[xPos,yPos,i] = 2;
            zPos = i;
            break;
          }
        }
        go.GetComponent<Renderer>().material.color = Color.white;
        if (isfinish("White", xPos, yPos, zPos)) {
          Debug.Log("White Win");
        }
      }
      return 1;
    } else {
      return 0;
    }
  }

  // Update is called once per frame
  void Update () {
    if (putSelect() == 1) {
      string tmpTurn = NextTurn;
      NextTurn = NowTurn;
      NowTurn = tmpTurn;
    }
  }
}
