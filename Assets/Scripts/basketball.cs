using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class basketball : playerInterface {
	public GameObject storeArea;
	public GameObject cxk;
	public Text debugText;

	private Vector3 smallScale;//target small scale
	private Vector3 largeScale;

	//--------------cxk 彩蛋
	public AudioSource cxkSound;
	public Text cxkText;
	private Vector2 cxkoffset;
	private bool played;
	//----------------------


	void Start() {
		smallScale = new Vector3(0.7f, 0.7f, 1);
		largeScale = new Vector3(1, 1, 1);
		nowsize = GetComponent<SpriteRenderer>().bounds.size;
		debugText.text = "";
		cxkText.text = "";
		played = false;
	}


	void Update() {
		touchMove();
		if (!inStoreArea()) {//in operation area
			beLarge(largeScale);
		}
		if (inStoreArea()){//in store area
			beSmall(smallScale);
			if(Input.touchCount==0)
				correctPos();
		}

		//=========================cxk 彩蛋===============================
		cxkoffset.x = transform.position.x - cxk.transform.position.x;
		cxkoffset.y = transform.position.y - cxk.transform.position.y;
		if (cxkoffset.x < 0)
			cxkoffset.x = -cxkoffset.x;
		if (cxkoffset.y < 0)
			cxkoffset.y = -cxkoffset.y;
		if (cxkoffset.x < 0.25 && cxkoffset.y < 1.45 && cxkoffset.y > 1.05 && (!(transform.localScale.x < 1)) && (!(cxk.transform.localScale.x < 1))) {
			cxkText.text = "基尼太美！";
			if (!played) {
				cxkSound.Play();
				played = true;
			}
		}
		else {
			cxkText.text = "";
			cxkSound.Stop();
			played = false;
		}
		//================================================================

	}

}
