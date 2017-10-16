using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spellcast : MonoBehaviour {

	public GameObject Spell;

	public void CastSpell(){
		Instantiate ((Object)Spell,transform.position, transform.rotation, null);
	}
}
