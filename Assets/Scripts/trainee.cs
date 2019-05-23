using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class trainee : MonoBehaviour {
	public Text debugText;
	public GameObject storeArea;

	private float offset_x;
	private float offset_y;
	private Vector3 nowsize;
	private Vector3 smallscale;

	void Start() {
		offset_x = 0;
		offset_y = 0;
		nowsize = GetComponent<SpriteRenderer>().bounds.size;
		debugText.text = "";
		smallscale = new Vector3(0.3f, 0.3f, 1);
	}

	void Update() {
		if (Input.touchCount > 0) {
			Touch touch = Input.GetTouch(0);

			switch (touch.phase) {
				case TouchPhase.Began:
					// Record initial touchOffset position.
					offset_x = Camera.main.ScreenToWorldPoint(touch.position).x - transform.position.x;
					offset_y = Camera.main.ScreenToWorldPoint(touch.position).y - transform.position.y;
					break;

				case TouchPhase.Moved:
					if (offset_x < nowsize.x / 2 && offset_x > -nowsize.x / 2 && offset_y < nowsize.y / 2 && offset_y > -nowsize.y / 2){//tapped
						if (!sortManager.Instance().getTappedList().Contains(gameObject.name))
							sortManager.Instance().getTappedList().Add(gameObject.name);
						if (sortManager.Instance().maxOrderName() == gameObject.name)//order is max
							transform.position = new Vector3(Camera.main.ScreenToWorldPoint(touch.position).x - offset_x, Camera.main.ScreenToWorldPoint(touch.position).y - offset_y, 0);

					}
					else {
						if (sortManager.Instance().getTappedList().Contains(gameObject.name)) {
							sortManager.Instance().getTappedList().Remove(gameObject.name);
						}
					}
					break;
			}
		}

		if (!inArea(transform.position)) {//in operation area
			if (transform.localScale.x < 1) {//make it larger
				transform.localScale = new Vector3(transform.localScale.x + 0.1f, transform.localScale.y + 0.1f, 1);
				nowsize = GetComponent<SpriteRenderer>().bounds.size;
			}
			if (Input.touchCount == 0) {
				if (store.Instance().getlist().Contains(gameObject.name)) {
					//erase and update index automatically
					store.Instance().getlist().Remove(gameObject.name);
				}
			}
		}

		if (inArea(transform.position)) {//in store area
			if (transform.localScale.x > smallscale.x) {//make it smaller
				transform.localScale = new Vector3(transform.localScale.x - 0.1f, transform.localScale.y - 0.1f, 1);
				nowsize = GetComponent<SpriteRenderer>().bounds.size;
			}
			if (Input.touchCount == 0) {//correct position in store area
				if (!store.Instance().getlist().Contains(gameObject.name)) {
					store.Instance().getlist().Add(gameObject.name);
				}
				else {
					transform.position = store.Instance().getWorldPos(gameObject.name);
					gameObject.GetComponent<SpriteRenderer>().sortingOrder = store.Instance().getlist().IndexOf(gameObject.name);
				}
			}
		}
		

		//if (inArea(transform.position) && Input.touchCount == 0) {
		//	if (!store.Instance().contains(gameObject.name))
		//		store.Instance().add(gameObject.name);
		//	transform.position = store.Instance().getWorldPos(gameObject.name);
		//	gameObject.GetComponent<SpriteRenderer>().sortingOrder = store.Instance().getOrder(gameObject.name);
		//}
	}

	private bool inArea(Vector3 vec3) {
		Vector2 offset;
		offset.x = vec3.x - storeArea.transform.position.x;
		offset.y = vec3.y - storeArea.transform.position.y;
		Vector3 size = storeArea.GetComponent<SpriteRenderer>().bounds.size;
		if (offset.x < size.x / 2 && offset.x > -size.x / 2 && offset.y < size.y / 2 && offset.y > -size.y / 2)
			return true;
		else
			return false;
	}
}
