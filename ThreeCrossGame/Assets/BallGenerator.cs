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

  decimal[,,] BoardData = new decimal[3,3,3];// 0 (space), 1(black), 2(white)

  // Use this for initialization
  void Start () {
  }

  float Isless(float ls, float rs) {
    if (ls < rs) return 1;
    return 0;
  }

  decimal putSelect() {
    if (Input.GetMouseButtonDown(0)) {
      GameObject go = Instantiate(BallPrefab) as GameObject;

      /*Position select*/
      float x_pos = Input.mousePosition.x;
      float y_pos = Input.mousePosition.y;
      float xPos = Isless(xFirstThread, x_pos) + Isless(xSecondThread, x_pos);
      float yPos = Isless(yFirstThread, y_pos) + Isless(ySecondThread, y_pos);
		  Debug.Log(x_pos);
      Debug.Log("y_ps:"+y_pos);
      go.transform.position = new Vector3(xPos*10, 10, yPos*10);

      /*color select*/
      if (NowTurn == "Black") {
        go.GetComponent<Renderer>().material.color = Color.black;
      } else {
        go.GetComponent<Renderer>().material.color = Color.white;
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
