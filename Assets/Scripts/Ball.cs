using UnityEngine;
using System.Collections;

public class Ball : MonoBehaviour {

	private Paddle paddle;
	
	private bool hasGameStarted = false;
	private Vector3 paddleToBallVector;

	// Use this for initialization
	void Start () {
		paddle = GameObject.FindObjectOfType<Paddle>();
		paddleToBallVector = this.transform.position - paddle.transform.position;
	
	}
	
	// Update is called once per frame
	void Update () {
		if (!hasGameStarted) {
			// lock the ball relative to the paddle.
			this.transform.position = paddle.transform.position + paddleToBallVector;
			
			// wait for a mouse press to launch.
			if (Input.GetMouseButtonDown (0)) {
				print ("Mouse Clicked, Launch ball.");
				hasGameStarted = true;
				this.rigidbody2D.velocity = new Vector2 (2f, 10f);
			}
		}
	}
	void OnCollisionEnter2D(Collision2D boing) {
		Vector2 tweak = new Vector2 (Random.Range(0f, 0.2f), Random.Range(0f, 0.2f));
		if (hasGameStarted) {
			audio.Play ();
			rigidbody2D.velocity += tweak;
		}
	}	
}