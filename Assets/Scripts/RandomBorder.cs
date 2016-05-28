using UnityEngine;
using System.Collections;

public class RandomBorder : MonoBehaviour {

	public GameObject borderSprite;
	public GameObject baseSprite;
	public bool applyUpdate = false;

	public float opositeSide;
	public float adjacentSide;

	public float angle;
	public float hypotenuse = 8.0f;

	public void ApplyAngle() {
		borderSprite.transform.rotation = Quaternion.identity;
		borderSprite.transform.Rotate(0, 0, angle);
		opositeSide = Mathf.Sin(angle * Mathf.Deg2Rad) * hypotenuse;
		adjacentSide = Mathf.Cos(angle * Mathf.Deg2Rad) * hypotenuse;
		baseSprite.transform.localScale = new Vector3(adjacentSide, baseSprite.transform.localScale.y, 0);
		baseSprite.transform.localPosition = new Vector3(0, 0, 15);
		if ((angle * borderSprite.transform.localScale.y)< 0)
			baseSprite.transform.Translate(0, angle / 60.0f, 0);
		applyUpdate = false;
	}
}
