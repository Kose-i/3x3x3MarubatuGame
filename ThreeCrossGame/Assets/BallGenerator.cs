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

  bool isfinish(string str) {
    if (str == "Black") {
      return true;
    } else if (str == "White"){
      return true;
    } else {
      Debug.Log("Not knowing str error happend [BallGenerator-isfinished]");
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
      Debug.Log("xPos:"+xPos+"yPos:"+yPos+"BoardData[xPos,yPos,i]:"+BoardData[xPos,yPos,1]);

      GameObject go = Instantiate(BallPrefab) as GameObject;
      go.transform.position = new Vector3(xPos*2, 10, yPos*2);
      /*color select*/
      if (NowTurn == "Black") {
        for (int i = 0;i < 3;++i) {
          if (BoardData[xPos,yPos,i] == 0) {
            BoardData[xPos,yPos,i] = 1;
            break;
          }
        }
        go.GetComponent<Renderer>().material.color = Color.black;
        if (isfinish("Black")) {
          Debug.Log("Black Win");
        }
      } else {
        for (int i = 0;i < 3;++i) {
          if (BoardData[xPos,yPos,i] == 0) {
            BoardData[xPos,yPos,i] = 2;
            break;
          }
        }
        go.GetComponent<Renderer>().material.color = Color.white;
        if (isfinish("White")) {
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
