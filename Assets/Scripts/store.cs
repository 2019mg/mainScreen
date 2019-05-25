using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class store {
	private static store instance;//singleton
	private float interval;
	private Vector3 startPos;

	private static List<string> storelist;
	GameObject storeArea= GameObject.Find("storeArea");

	private store() {
		storelist = new List<string>();
		interval = storeArea.GetComponent<SpriteRenderer>().bounds.size.y * 2/ 3;
		// (below) 中心点向左减去1/3原storeArea长度，即从1/6处开始堆
		startPos = new Vector3(storeArea.transform.position.x - storeArea.GetComponent<SpriteRenderer>().bounds.size.x / 3, storeArea.transform.position.y, 0);
	}

	public static store Instance() {
		if (instance == null) {
			instance = new store();
		}
		return instance;
	}

	public ref List<string> getlist() {
		return ref storelist;
	}

	public Vector3 getWorldPos(string name) {
		Vector3 vec3 = new Vector3(startPos.x + interval * (storelist.IndexOf(name) + 1), startPos.y, 0);
		return vec3;
	}
}
