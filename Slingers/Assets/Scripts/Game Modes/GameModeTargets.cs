using UnityEngine;
using System.Collections;

public class GameModeTargets : MonoBehaviour {
    bool game_over = false;
    bool game_started = false;
    public TextMesh timer_text;
    Target[] all_targets;
    System.DateTime start, finish;

	// Use this for initialization
	void Start ()
    {
        all_targets = GameObject.FindObjectsOfType<Target>();
        //for(int i = 0; i < )
    }
	
	// Update is called once per frame
	void Update () {
        System.TimeSpan timer_time = new System.TimeSpan();
        Color timer_color = Color.white;

        Target[] all_active_targets = GameObject.FindObjectsOfType<Target>();
        if (!game_started && all_active_targets.Length != all_targets.Length)
        {
            game_started = true;
            start = System.DateTime.Now;
        }
        if(game_started && !game_over)
        {
            timer_color = Color.red;
            bool all_targets_destroyed = all_active_targets.Length == 0;
            if (all_targets_destroyed)
            {
                game_over = true;
                finish = System.DateTime.Now;
            }
            else
            {
                timer_time = (System.DateTime.Now - start);
            }
        }
        if(game_over)
        {
            timer_color = Color.green;
            timer_time = finish - start;
        }
        timer_text.text = string.Format("{0:d2}.{1:d2}", (int)timer_time.TotalSeconds, timer_time.Milliseconds/10);
        timer_text.color = timer_color;

        if(Input.GetKeyDown(KeyCode.BackQuote))
        {
            
        }
	}
}
