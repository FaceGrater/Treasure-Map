using UnityEngine;
using System.Collections;

public class Brick : MonoBehaviour {

	public AudioClip crack;
	public Sprite[] hitSprites;
	public static int breakableCount = 0;
	public GameObject smoke;
	
	private LevelManager levelManager;
	private int timesHits;
	private bool isBreakable;
	
	// Use this for initialization
	void Start () {
		isBreakable  = (this.tag == "Breakable");
		// Here is where i will keep track of breakable bricks
		if (isBreakable){
			breakableCount++;
		}
		timesHits = 0;
		levelManager = GameObject.FindObjectOfType<LevelManager>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void OnCollisionEnter2D (Collision2D trigger) {
		AudioSource.PlayClipAtPoint (crack, transform.position);
		if (isBreakable) {
			HandleHits();
		}
	}
	
	void HandleHits () {
		timesHits ++;
		print ("Brick hit");
		int maxHits = hitSprites.Length + 1;
		if (timesHits >= maxHits){
			breakableCount--;
			levelManager.BrickDestroyed();
			PuffSmoke();
			Destroy(gameObject);
		} else {
			LoadSprites();
		}
	}
	
	void PuffSmoke() {
		GameObject smokePuff = Instantiate (smoke, gameObject.transform.position, Quaternion.identity) as GameObject;
		smokePuff.particleSystem.startColor = gameObject.GetComponent<SpriteRenderer>().color;
	}
	
	void LoadSprites () {
		int spriteIndex = timesHits - 1;
		if (hitSprites[spriteIndex]) {
			this.GetComponent<SpriteRenderer>().sprite = hitSprites[spriteIndex];
		} else {
			Debug.LogError("Brick Sprite Missing");
		}
	}
}
