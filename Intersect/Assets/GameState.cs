using UnityEngine;
using System.Collections;

public class GameState : MonoBehaviour {

	public static float freezeTime = 2.0f;

	public Player thrower;
	public Ball caughtBall;

//	private float startFixedDeltaTimeTime;
	private float unfreezeTime;

	void Start ()
	{
		thrower = null;
		caughtBall = null;

//		startFixedDeltaTimeTime = Time.fixedDeltaTime;
		unfreezeTime = float.PositiveInfinity;
	}

	void Update ()
	{
		if (Time.realtimeSinceStartup >= unfreezeTime)
		{
			Unfreeze();
		}
	}

	public void OnCatch(Player catcher, Ball ball)
	{
		thrower = catcher;
		caughtBall = ball;
		caughtBall.gameObject.renderer.enabled = false;

		if (ball.isInGoal == null || thrower.goal != ball.isInGoal)
		{
			Freeze(freezeTime);
		}
		else
		{
			ball.isInGoal.Score();
			Reset();
		}

	}

	public void SetTimeScale(float timeScale)
	{
		Time.timeScale = timeScale;
//		Time.fixedDeltaTime = startFixedDeltaTimeTime * timeScale;
	}

	private void Freeze(float duration)
	{
		Time.timeScale = 0.0f;
		unfreezeTime = Time.realtimeSinceStartup + duration;
	}

	private void Unfreeze()
	{
		caughtBall.gameObject.renderer.enabled = true;
		caughtBall.OnThrow(thrower);


		SetTimeScale(1.0f);
		unfreezeTime = float.PositiveInfinity;
	}

	private void Reset()
	{
		for (int i=1; i<=4; i++)
		{
			GameObject.Find("player" + i).GetComponent<Player>().Reset();
		}

		GameObject.Find("ball").GetComponent<Ball>().Reset();

		Freeze(3f);
	}
}
