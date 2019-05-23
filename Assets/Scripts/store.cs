using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class store : MonoBehaviour {
	private static store instance;//singleton

	private float interval;
	private Vector3 startPos;

	private static List<string> storelist;

	void Start() {
		storelist = new List<string>();

		interval = GetComponent<SpriteRenderer>().bounds.size.y * 2/ 3;
		// (below) 中心点向左减去1/3原storeArea长度，即从1/6处开始堆
		startPos = new Vector3(transform.position.x - GetComponent<SpriteRenderer>().bounds.size.x / 3, transform.position.y, 0);
	}

	public static store Instance() {
		if (instance == null) {
			instance = FindObjectOfType(typeof(store)) as store;
			if (instance == null) {
				GameObject obj = new GameObject();
				instance = obj.AddComponent<store>();
			}
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

	void Update() {

	}
}
