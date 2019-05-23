using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class store : MonoBehaviour {
	private static store instance;//singleton

	private int storenum;
	private float interval;
	private float startInterval;
	//private float y;
	private Dictionary<string, int> storeMap;
	private Vector3 startPos;
	private static List<string> storelist;

	void Start() {
		storenum = 0;
		interval = GetComponent<SpriteRenderer>().bounds.size.y * 2/ 3;
		startInterval = interval * 2;
		startPos = new Vector3(transform.position.x - GetComponent<SpriteRenderer>().bounds.size.x / 4, transform.position.y, 0);
		//y = GetComponent<SpriteRenderer>().bounds.size.y / 2;
		storeMap = new Dictionary<string, int>();
		storelist = new List<string>();
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

	public void add(string goName) {
		if (!storeMap.ContainsKey(goName)) {
			storeMap.Add(goName, storenum);
			storenum++;
		}
	}

	//public int getOrder(string goName) {
	//	return storeMap[goName];
	//}

	//public Vector3 getWorldPos(string goName) {
	//	Vector2 vec;
	//	vec.x = startInterval + storeMap[goName] * interval;
	//	vec.y = y;
	//	Vector3 vec3 = Camera.main.ScreenToWorldPoint(vec);
	//	vec3.z = 0;
	//	return vec3;
	//}

	//public bool contains(string goName) {
	//	return storeMap.ContainsKey(goName);
	//}



	// Update is called once per frame
	void Update() {

	}
}
