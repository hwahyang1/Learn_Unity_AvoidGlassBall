using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

/*
 * [Class] GameManager
 * 게임의 전반적인 실행을 관리합니다.
 */
public class GameManager : MonoBehaviour
{
	public GameObject player;
	public GameObject glassBall;

	public GameObject heartObject;

	public GameObject gameOver;

	public Sprite heartFull;
	public Sprite heartEmpty;

	public Text highScoreText;
	public Text currentScoreText;

	public float limitY = 6.25f;

	public float spawnPeriod = 1f;

	public int defaultScore = 20;
	public int bonusScore = 30;

	private GameObject ballParent;

	private float nowPeriod = 0f;

	private int highScore = 0;
	private int currentScore = 0;
	private bool isNewBest = false;

	private int maxHeart = 3;
	private int currentHeart = 3;

	/*
	 * [Note] Property
	 * 변수의 값을 대입시키거나 값을 가져올 때 권한(접근제한자)를 설정 할 수 있는 기능
	 * 일반 변수처럼 사용 가능 (읽기 전용으로 만들거나 입력값을 검사 할 때 사용함)
	 * 
	 * get -> 호출 되었을 때 반환 할 내용
	 * set -> 호출 되었을 때 어디에 저장할지
	 */
	public int Heart
	{
		get
		{
			return currentHeart;
		}

		private set
		{
			currentHeart = value;
		}
	}

	private void Start()
	{
		InitGame();
	}

	private void Update()
	{
		if (currentHeart <= 0)
		{
			if (currentHeart == 0)
			{
				Debug.Log("[GameManger.Update()] 플레이어가 모든 체력을 소진하여 게임이 종료되었습니다.");
				gameOver.SetActive(true);
				Heart--;
			}
			else
			{
				return;
			}
		}

		if (nowPeriod >= spawnPeriod)
		{
			nowPeriod = 0f;

			float random_x = Random.Range(-9f, 9f);

			/*
			 * [Note: Method] Instantiate(GameObject original, Vector3 position, Quaternion rotation, Transform parent): GameObject
			 * GameObject를 스폰시킵니다.
			 * 
			 * <GameObject original>
			 * 스폰시킬 GameObject(Prefab)를 지정합니다.
			 * 
			 * <Vector3 position>
			 * 스폰시킬 좌표를 지정합니다.
			 * 2D 환경에서는 Vector2도 사용 할 수 있습니다.
			 * 
			 * <Quaternion rotation>
			 * 회전시킬 각도를 지정합니다.
			 * 
			 * <Transform parent> (Optional)
			 * 스폰 될 GameObject의 Parent가 될 GameObject의 Transform을 지정합니다.
			 */
			Instantiate(glassBall, new Vector3(random_x, limitY, 0f), Quaternion.identity, ballParent.transform);
			/*
			 * GameObject가 이미 스폰되어 있는 경우 이 방식을 사용합니다: 
			 * GameObject spawnedObject = Instantiate(glassBall, new Vector3(random_x, limitY, 0f), Quaternion.identity);
			 * spawnedObject.transform.SetParent(ballParent.transform);
			 */
		}

		if (currentScore >= highScore)
		{
			if (highScore > 0 && !isNewBest)
			{
				currentScore += bonusScore;
				isNewBest = true;

				highScoreText.gameObject.SetActive(true);
				currentScoreText.gameObject.SetActive(false);
			}
			else
			{
				highScore = currentScore;
				highScoreText.text = "최고 " + highScore.ToString() + "점";
			}
		}
		currentScoreText.text = currentScore.ToString() + "점";

		nowPeriod += Time.deltaTime;
	}

	/*
	 * [Method] OnPlayerHit(): void
	 * 플레이어가 유리공에 맞았을 때 체력을 감소시킵니다.
	 */
	public void OnPlayerHit()
	{
		if (currentHeart > 0)
		{
			currentHeart--;
			heartObject.transform.GetChild(currentHeart).GetComponent<Image>().sprite = heartEmpty;
		}
	}

	/*
	 * [Method] OnPlayerAvoid(): void
	 * 플레이어가 유리공을 피했을 때 점수를 증가시킵니다.
	 */
	public void OnPlayerAvoid()
	{
		currentScore += defaultScore;
	}

	/*
	 * [Method] InitGame(): void
	 * 게임을 초기 상태로 설정합니다. (최고점수 제외)
	 */
	public void InitGame()
	{
		gameOver.SetActive(false);

		highScoreText.text = "최고 " + highScore.ToString() + "점";
		currentScoreText.text = currentScore.ToString() + "점";

		if (highScore == 0)
		{
			highScoreText.gameObject.SetActive(false);
		}

		currentHeart = maxHeart;
		for (int i = 0; i < currentHeart; i++)
		{
			heartObject.transform.GetChild(i).GetComponent<Image>().sprite = heartFull;
		}

		ballParent = GameObject.Find("GlassBalls");
		nowPeriod = spawnPeriod;

		player.transform.position = new Vector3(0f, -2.75f, 0f);
	}
}
