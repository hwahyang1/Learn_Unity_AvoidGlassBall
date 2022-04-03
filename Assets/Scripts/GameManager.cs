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

	public Sprite heartFull;
	public Sprite heartEmpty;

	public Text highScoreText;
	public Text currentScoreText;

	public float limitY = 6.25f;

	public float spawnPeriod = 1f;
	private float nowPeriod = 0f;

	public int defaultScore = 20;
	public int bonusScore = 30;

	private int highScore = 0;
	private int currentScore = 0;
	private bool isNewBest = false;

	private int heart = 3;

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
			return heart;
		}

		private set
		{
			heart = value;
		}
	}

	private void Start()
	{
		highScoreText.text = "최고 " + highScore.ToString() + "점";
		currentScoreText.text = currentScore.ToString() + "점";

		if (highScore == 0)
		{
			highScoreText.gameObject.SetActive(false);
		}
	}

	private void Update()
	{
		if (heart <= 0)
		{
			if (heart == 0)
			{
				Debug.Log("[GameManger.Update()] 플레이어가 모든 체력을 소진하여 게임이 종료되었습니다.");
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
			 * [Note: Method] Instantiate(Object original, Vector3 position, Quaternion rotation): GameObject
			 * Description
			 */
			Instantiate(glassBall, new Vector3(random_x, limitY, 0f), Quaternion.identity);
		}

		currentScoreText.text = currentScore.ToString() + "점";
		if (currentScore >= highScore)
		{
			if (highScore > 0 && !isNewBest)
			{
				currentScore += bonusScore;
				isNewBest = true;

				currentScoreText.gameObject.SetActive(false);
			}
			else
			{
				highScore = currentScore;
				highScoreText.text = "최고 " + highScore.ToString() + "점";
			}
		}
	}
}
