using System.Collections;
using System.Collections.Generic;

using UnityEngine;

/*
 * [Class] GlassBall
 * 플레이어와 유리공의 피격 여부, 유리공 제거를 관리합니다.
 */
public class GlassBall : MonoBehaviour
{
	private GameManager manager;

	public float speed = 0.175f;

	private void Start()
	{
		manager = GameObject.Find("GameManager").GetComponent<GameManager>(); // 스크립트 하나 긁어오기 힘드네
	}

	private void FixedUpdate()
	{
		if (transform.position.y <= -manager.limitY)
		{
			Destroy(gameObject);
		}
		else
		{
			transform.Translate(0f, -speed, 0f);
		}
	}

	/*
	 * [Note: Method] OnTriggerEnter2D(Collider2D collision): void
	 * 스크립트가 붙은 gameObject와 충돌 되었을 때 호출됩니다.
	 * 
	 * [Note: Method] OnTriggerStay2D(Collider2D collision): void
	 * 스크립트가 붙은 gameObject와 충돌 상태일 때 호출됩니다.
	 * 
	 * [Note: Method] OnTriggerExit2D(Collider2D collision): void
	 * 스크립트가 붙은 gameObject와 충돌이 끝났을 때 호출됩니다.
	 */
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.tag == "Player")
		{
			Debug.Log("[GlassBall.OnTriggerEnter2D(Collider2D collision)] 플레이어가 유리공에 맞았습니다.");
			Destroy(gameObject);
		}
	}
}
