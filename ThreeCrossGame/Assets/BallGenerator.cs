﻿using UnityEngine;
using System.Collections;

public class BallGenerator : MonoBehaviour {

  public GameObject spherePrefab;

  public string NowTurn = "Black";
  public string NextTurn = "White";

  // Use this for initialization
  void Start () {
  }
  decimal putSelect() {
    if (Input.GetKeyDown(KeyCode.Space)) {
      GameObject go = Instantiate(spherePrefab) as GameObject;
      go.transform.position = new Vector3(0, 10, 0);
      return 1;
    } else if (Input.GetKeyDown(KeyCode.RightArrow)) {
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