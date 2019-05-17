using UnityEngine;
using System.Collections;

public class BallController : MonoBehaviour {

  float move_under = -1;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
    if (transform.position.y > 0) transform.Translate(0, this.move_under, 0);
	}
}
