using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Trainee : PlayerInterface {
	public Text debugText;

	//private Vector3 nowsize;
	private Vector3 smallScale;//target small scale
	private Vector3 largeScale;

	void Start() {
		typeOfObject = 0;
		smallScale = new Vector3(0.3f, 0.3f, 1);
		largeScale = new Vector3(1, 1, 1);
		nowsize = GetComponent<SpriteRenderer>().bounds.size;
		debugText.text = "";
	}

	void Update() {
		touchMove();
		debugText.text = transform.position.ToString();
		if (!inStoreArea()) {//in operation area
			beLarge(largeScale);
		}

		if (inStoreArea()) {//in store area
			beSmall(smallScale);
			if (!isTapped)
				correctPos();
		}
	}

}
