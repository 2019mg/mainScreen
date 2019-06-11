using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleStoreArea {
	private static PuzzleStoreArea instance;//singleton
	GameObject puzzleStore;
	private float interval;
	private Vector3 startPos;

	private List<string> storelist;
	private PuzzleStoreArea() {
		puzzleStore = GameObject.Find("PuzzleStoreArea");
		storelist = new List<string>();
		interval = puzzleStore.GetComponent<SpriteRenderer>().bounds.size.y * 2 / 3;
		//(below)中心点向左减去1 / 3原storeArea长度，即从1 / 8处开始堆
		startPos = new Vector3(puzzleStore.transform.position.x - puzzleStore.GetComponent<SpriteRenderer>().bounds.size.x * 3 / 8, puzzleStore.transform.position.y, 0);
	}

	public void refreshStore() {
		PlayerInterface[] playerList = MonoBehaviour.FindObjectsOfType<PlayerInterface>();
		foreach (PlayerInterface player in playerList) {
			if (player.typeOfObject == 0) {
				if (player.isInStore && !storelist.Contains(player.name))
					storelist.Add(player.name);
				else {
					if (!player.isInStore && storelist.Contains(player.name))
						storelist.Remove(player.name);
				}
			}
		}
	}

	public static PuzzleStoreArea Instance() {
		if (instance == null) {
			instance = new PuzzleStoreArea();
		}
		return instance;
	}

	public int getIndex(string name) {
		return storelist.IndexOf(name);
	}

	public Vector3 getWorldPos(string name) {
		Vector3 vec3 = new Vector3(startPos.x + interval * (storelist.IndexOf(name) + 1), startPos.y, 0);
		return vec3;
	}
}
