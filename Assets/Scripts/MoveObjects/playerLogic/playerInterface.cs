using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerInterface : MonoBehaviour {
	public Vector2 touchOffset;
	public Vector3 nowsize;
	public bool isTapped;
	public bool isInStore;
	public int typeOfObject;

	public PlayerInterface() {
		touchOffset = new Vector2();
		nowsize = new Vector3();
		isTapped = false;
		isInStore = true;
	}

	private string tappedObject(Vector2 tapPos) {
		GameObject[] gameObjlist = MonoBehaviour.FindObjectsOfType<GameObject>();
		int maxOrder = -1;
		string maxName = "";
		Vector2 offset = new Vector2();
		for(int i = 0; i < gameObjlist.Length; i++) {
			if (gameObjlist[i].CompareTag("Player")) {
				offset.x = Camera.main.ScreenToWorldPoint(tapPos).x - gameObjlist[i].transform.position.x;
				offset.y = Camera.main.ScreenToWorldPoint(tapPos).y - gameObjlist[i].transform.position.y;
				Vector3 size = gameObjlist[i].GetComponent<SpriteRenderer>().bounds.size;
				if(System.Math.Abs(offset.x) < size.x / 2 && System.Math.Abs(offset.y) < size.y / 2) {//in it
					int nowOrder = gameObjlist[i].GetComponent<SpriteRenderer>().sortingOrder;
					if (nowOrder>maxOrder) {
						maxOrder = nowOrder;
						maxName = gameObjlist[i].name;
					}
				}
			}
		}
		return maxName;
	}

	public void touchMove() {
		if (Input.touchCount > 0) {//move
			Touch touch = Input.GetTouch(0);
			if (touch.phase == TouchPhase.Began) {
				touchOffset.x = Camera.main.ScreenToWorldPoint(touch.position).x - transform.position.x;
				touchOffset.y = Camera.main.ScreenToWorldPoint(touch.position).y - transform.position.y;
				if (tappedObject(touch.position)==gameObject.name)
					isTapped = true;
			}
			if (touch.phase == TouchPhase.Moved) {
				if(isTapped)
					transform.position = new Vector3(Camera.main.ScreenToWorldPoint(touch.position).x - touchOffset.x, Camera.main.ScreenToWorldPoint(touch.position).y - touchOffset.y, 0);
			}
			if (touch.phase == TouchPhase.Ended) {
				isTapped = false;
			}
		}
	}


	public void beLarge(Vector3 largeScale) {
		if (transform.localScale.x < largeScale.x) {//make it larger
			transform.localScale = new Vector3(transform.localScale.x + 0.1f, transform.localScale.y + 0.1f, 1);
			nowsize = GetComponent<SpriteRenderer>().bounds.size;
		}
	}

	public void beSmall(Vector3 smallScale) {
		if (transform.localScale.x > smallScale.x) {//make it smaller
			transform.localScale = new Vector3(transform.localScale.x - 0.1f, transform.localScale.y - 0.1f, 1);
			nowsize = GetComponent<SpriteRenderer>().bounds.size;
		}
	}

	public void correctPos() {
		if (!isTapped && typeOfObject == 0) {
			transform.position = PuzzleStoreArea.Instance().getWorldPos(gameObject.name);
			gameObject.GetComponent<SpriteRenderer>().sortingOrder = PuzzleStoreArea.Instance().getIndex(gameObject.name);
		}
		if(!isTapped && typeOfObject == 1) {
			transform.position = ToolStoreArea.Instance().getWorldPos(gameObject.name);
			gameObject.GetComponent<SpriteRenderer>().sortingOrder = ToolStoreArea.Instance().getIndex(gameObject.name);
		}
	}

	public bool inStoreArea() {
		GameObject storeArea = GameObject.Find("PuzzleStoreArea");
		float offsetY;
		offsetY = transform.position.y - storeArea.transform.position.y;
		Vector3 size = storeArea.GetComponent<SpriteRenderer>().bounds.size;
		if ( System.Math.Abs(offsetY) < size.y / 2) {
			isInStore = true;
			if(typeOfObject==0)
				PuzzleStoreArea.Instance().refreshStore();
			if(typeOfObject==1)
				ToolStoreArea.Instance().refreshStore();
			return true;
		}
		else {
			isInStore = false;
			if (typeOfObject == 0)
				PuzzleStoreArea.Instance().refreshStore();
			if (typeOfObject == 1)
				ToolStoreArea.Instance().refreshStore();
			return false;
		}
	}
}
