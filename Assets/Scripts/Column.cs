using UnityEngine;
using System.Collections;

public class Column : MonoBehaviour 
{

	//private int cont = 0;		//Used in cooperative mode

	void OnTriggerEnter2D(Collider2D other)
	{
		//Cooperative version
		/*
		if (other.tag == "Player1")
			cont++;
		if (other.tag == "Player1")
			cont++;
		if(cont == 2)
		{
			//If the both birds hits the trigger collider in between the columns then
			//tell the game control that scored.
			GameControl.instance.BirdScored();
		}
		cont = 0;
		*/

		//Competitive version
		if (other.tag == "Player1") {
			GameControl.instance.Bird1Scored ();
		}
		if (other.tag == "Player2") {
			GameControl.instance.Bird2Scored ();
		}
	}
}