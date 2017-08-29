using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Bird : MonoBehaviour 
{
	public float upForce;                   //Upward force of the "flap".
	private bool isDead = false;            //Has the player collided with a wall?
	private Animator anim;                  //Reference to the Animator component.
	private Rigidbody2D rb2d;               //Holds a reference to the Rigidbody2D component of the bird.

	void Start()
	{
		//Get reference to the Animator component attached to this GameObject.
		anim = GetComponent<Animator> ();
		//Get and store a reference to the Rigidbody2D attached to this GameObject.
		rb2d = GetComponent<Rigidbody2D>();
	}

	void Update()
	{
		//Don't allow control if the bird has died.
		if (isDead == false) 
		{
			//Look for input to trigger a "flap".
			if (gameObject.tag == "Player1") {
				if (Input.GetMouseButtonDown (0) || Input.GetKeyDown(KeyCode.W)) {
					//Play associate sound
					GetComponent<AudioSource>().Play();
					//...tell the animator about it and then...
					anim.SetTrigger ("Flap");
					//...zero out the birds current y velocity before...
					rb2d.velocity = Vector2.zero;
					//  new Vector2(rb2d.velocity.x, 0);
					//..giving the bird some upward force.
					rb2d.AddForce (new Vector2 (0, upForce));
				}
			}
			if (gameObject.tag == "Player2") {
				if (Input.GetMouseButtonDown (1) || Input.GetKeyDown(KeyCode.UpArrow)) {
					GetComponent<AudioSource>().Play();
					anim.SetTrigger ("Flap");
					rb2d.velocity = Vector2.zero;
					rb2d.AddForce (new Vector2 (0, upForce));
				}
			}
		}
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "End") {
			// Zero out the bird's velocity
			rb2d.velocity = Vector2.zero;
			// If the bird collides with something set it to dead...
			isDead = true;
			//...tell the Animator about it...
			anim.SetTrigger ("Die");
			//...and tell the game control about it.
			GameControl.instance.BirdDied ();
		}
	}
}