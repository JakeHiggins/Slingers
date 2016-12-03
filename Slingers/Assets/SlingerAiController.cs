using UnityEngine;
using System.Collections;
using UnityStandardAssets._2D;
using System.Collections.Generic;

//avoid bullets
//fire bullets at player
[RequireComponent(typeof(PlatformerCharacter2D))]
public class SlingerAiController : MonoBehaviour {
    public int bullet_prediction_frame_count = 10;
    PlatformerCharacter2D character;

	// Use this for initialization
	void Start () {
        character = GetComponent<PlatformerCharacter2D>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        bool jump = false;

        List<Bullet> incoming_bullets = new List<Bullet>();
        //if a bullet will reach this character within X frames, jump
        Bullet[] all_bullets = GameObject.FindObjectsOfType<Bullet>();
        foreach(Bullet b in all_bullets)
        {
            if (b.deflected)
                continue;
            Rigidbody2D body = b.GetComponent<Rigidbody2D>();
            Vector2 direction = body.velocity;
            float speed = direction.magnitude;
            direction /= speed;
            //assume 100 is the max cause i dunno how else to make an array
            RaycastHit2D[] results = new RaycastHit2D[100];
            Ray2D ray = new Ray2D(b.transform.position, direction);
            float distance_in_X_frames = speed * Time.fixedDeltaTime * bullet_prediction_frame_count;
            Physics2D.RaycastNonAlloc(ray.origin, ray.direction, results, distance_in_X_frames);
            Rigidbody2D character_body = character.GetComponent<Rigidbody2D>();
            foreach (RaycastHit2D result in results)
            {
                if (result == null)
                    break;
                if (result.collider == null)
                    continue;
                if (result.collider.attachedRigidbody != null &&
                    result.collider.attachedRigidbody == character_body)
                {
                    incoming_bullets.Add(b);
                }
            }
        }

        if(incoming_bullets.Count > 0)
        {
            jump = true;
        }

        character.Move(0, false, jump);
	}
}
