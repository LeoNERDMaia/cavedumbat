using UnityEngine;
using System.Collections;

public class RandomBorderBase : MonoBehaviour {

	public static int spriteNumber = 0;

	public Sprite[] randomSprites;

	// Use this for initialization
	void Start () {
		((SpriteRenderer)this.GetComponent(typeof(SpriteRenderer))).sprite = randomSprites[spriteNumber];
		spriteNumber++;
		if (spriteNumber >= randomSprites.Length)
			spriteNumber = 0;
	}
}
