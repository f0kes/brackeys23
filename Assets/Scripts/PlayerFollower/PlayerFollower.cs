using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFollower : MonoBehaviour
{
	public Transform Player;
	void Start()
	{
	}


	private void Update()
	{
		if(Player == null) return;

		var position = Player.position;
		var t = transform;
		t.position = new Vector3(position.x, position.y, t.position.z);
	}
}