using UnityEngine;
using System.Collections;

public class BallController : MonoBehaviour {

  float move_under = -1;

	// Use this for initialization
	void Start () {
	}

  private void OnTriggerEnter(Collider collision)
  {
    this.move_under = 0;
    // 物体がトリガーに接触しとき、１度だけ呼ばれる
  }

	// Update is called once per frame
	void Update () {
    if (transform.position.y > 0) transform.Translate(0, this.move_under, 0);
	}
}
