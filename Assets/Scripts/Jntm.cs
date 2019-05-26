using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Jntm : PlayerInterface {

	private Vector3 smallScale;//target small scale
	private Vector3 largeScale;

	void Start() {
		typeOfObject = 1;
		smallScale = new Vector3(0.6f, 0.6f, 1);
		largeScale = new Vector3(1, 1, 1);
		nowsize = GetComponent<SpriteRenderer>().bounds.size;
	}

	void Update() {
		touchMove();
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
