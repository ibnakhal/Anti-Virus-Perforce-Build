using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MinimapManager : MonoBehaviour {
	public Image miniMap, roomMap, path, player;
	// Use this for initialization
	void Start () {
		FindObjectOfType<RoomGenerator> ().GRevent += PlaceMap;
		FindObjectOfType<RoomGenerator> ().PathEvent += PathMaker;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void PlaceMap(Vector3 loc)
	{
		Image temp = Instantiate (roomMap, miniMap.transform).GetComponent<Image> ();
		temp.rectTransform.localPosition = LocConverter(loc);
	}

	void PathMaker(Vector3 loc1, Vector3 loc2)
	{
		Vector3 newPos = (loc1 + loc2) / 2;
		Image temp = Instantiate (path, miniMap.transform).GetComponent<Image> ();	
		temp.rectTransform.localPosition = LocConverter(newPos);
		if (loc1.z == loc2.z)
			temp.rectTransform.localScale = new Vector3 (0.05f, 0.025f,0f);
		else
			temp.rectTransform.localScale = new Vector3 (0.025f, 0.05f,0f);

	}

	public void PlayerMove(Transform loc)
	{
		Transform temp = loc;
		//LocConverter(loc)
		while (temp.parent != null) {
			temp = temp.parent;
			Debug.Log (temp);
		}

		player.rectTransform.localPosition = LocConverter (temp.position);
	}

	Vector3 LocConverter(Vector3 loc)
	{
		Vector3 tempLoc = loc / 50 * 15;
		return new Vector3(tempLoc.x, tempLoc.z, 0);
	}
}
