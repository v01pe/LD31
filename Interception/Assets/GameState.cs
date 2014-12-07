using UnityEngine;
using System.Collections;

public class GameState : MonoBehaviour {

	public float freezeTime;
	public GUIText countdownLabel;

	public Vector2 timeScaleRange;
	public float timeScaleDuration;
	public float timeScaleLerpExp;

	private Player thrower;
	private Ball caughtBall;

	private float startFixedDeltaTimeTime;
	private bool frozen;
	private float unfreezeTimestamp;

	void Start ()
	{
		thrower = null;
		caughtBall = null;
		countdownLabel.enabled = false;

		startFixedDeltaTimeTime = Time.fixedDeltaTime;

		unfreezeTimestamp = 0f;
		Freeze(0.5f);
	}

	void Update ()
	{
		if (frozen)
		{
			float timeToUnfreeze = unfreezeTimestamp - Time.realtimeSinceStartup;
			if (timeToUnfreeze <= 10f)
			{
				countdownLabel.enabled = true;
				countdownLabel.text = Mathf.Ceil(timeToUnfreeze).ToString();
			}
			if (Time.realtimeSinceStartup >= unfreezeTimestamp)
			{
				Unfreeze();
				countdownLabel.enabled = false;
			}
		}
		else
		{
			float timeScaleLerp = Mathf.Clamp01((Time.realtimeSinceStartup - unfreezeTimestamp) / timeScaleDuration);
			timeScaleLerp = Mathf.Pow(timeScaleLerp, timeScaleLerpExp);
			SetTimeScale(Mathf.Lerp(timeScaleRange[0], timeScaleRange[1], timeScaleLerp));
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
			gameObject.GetComponent<AudioSource>().Play();
			Reset();
		}

	}

	public void SetTimeScale(float timeScale)
	{
		Time.timeScale = timeScale;
		Time.fixedDeltaTime = startFixedDeltaTimeTime * timeScale;
	}

	private void Freeze(float duration)
	{
		Time.timeScale = 0f;
		frozen = true;
		unfreezeTimestamp = Time.realtimeSinceStartup + duration;
	}

	private void Unfreeze()
	{
		if (caughtBall != null)
		{
			caughtBall.gameObject.renderer.enabled = true;
			caughtBall.OnThrow(thrower);
			thrower.OnThrow();
		}

		SetTimeScale(timeScaleRange.x);
		frozen = false;
	}

	private void Reset()
	{
		thrower = null;
		caughtBall = null;

		for (int i=1; i<=4; i++)
		{
			GameObject.Find("player" + i).GetComponent<Player>().Reset();
		}

		GameObject.Find("ball").GetComponent<Ball>().Reset();

		Freeze(3f);
	}
}
