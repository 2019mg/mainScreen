using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sortManager : MonoBehaviour {

	private GameObject[] gameObjlist;
	private static sortManager instance;
	private Dictionary<string, int> orders;
	private static List<string> rangetapped;
	// Start is called before the first frame update
	void Start() {
		gameObjlist = FindObjectsOfType<GameObject>();
		orders = new Dictionary<string, int>();
		rangetapped = new List<string>();
	}

	public static sortManager Instance() {
		if (instance == null) {
			instance = FindObjectOfType(typeof(sortManager)) as sortManager;
			if (instance == null) {
				GameObject obj = new GameObject();
				instance = obj.AddComponent<sortManager>();
			}
		}
		return instance;
	}

	public ref List<string> getTappedList() {
		return ref rangetapped;
	}

	public string maxOrderName() {
		string s="";
		int t = 0;
		for (int i = 0; i < rangetapped.Count; i++) {
			int temp = orders[rangetapped[i]];
			print(temp);
			if (orders[rangetapped[i]] >= t) {
				s = rangetapped[i];
				t = orders[rangetapped[i]];
			}
		}
		return s;
	}

	void Update() {
		gameObjlist = FindObjectsOfType<GameObject>();
		for (int i = 0; i < gameObjlist.Length; i++) {
			if (gameObjlist[i].CompareTag("Player")) {
				orders[gameObjlist[i].name] = gameObjlist[i].GetComponent<SpriteRenderer>().sortingOrder;
			}
		}
	}
}
