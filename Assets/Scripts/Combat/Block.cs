using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour {	

	private int damageReceived;
	
	private void TakeDamage(object args)
	{
		object[] o = (object[])args;
		damageReceived -= (int)o[0];
	}
}
