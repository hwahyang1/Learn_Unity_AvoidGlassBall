using System.Collections;
using System.Collections.Generic;

using UnityEngine;

/*
 * [Class] PlayerController
 * 플레이어의 움직임을 처리합니다.
 */
public class PlayerController : MonoBehaviour
{
	private SpriteRenderer render;

	private float maxX = 8f;
	private float posY = -2.75f;

	private void Start()
	{
		render = GetComponent<SpriteRenderer>(); // 스크립트가 붙은 gameObject의 컴포넌트를 가져 올 때는 gameObject 호출 생략
	}

	private void Update()
	{
		/*
		 * [Note: Method] Input.GetMouseButtonDown(KeyCode key): bool
		 * 키보드 입력을 인식합니다.
		 * 
		 * <KeyCode key>
		 * 인식할 버튼을 지정합니다.
		 * KeyCode.~~~
		 */
		if (Input.GetKeyDown(KeyCode.A))
		{
			MoveLeft();
		}
		if (Input.GetKeyDown(KeyCode.D))
		{
			MoveRight();
		}
	}

	/*
	 * [Method] MoveLeft(): void
	 * 플레이어를 좌측으로 이동시킵니다.
	 */
	public void MoveLeft()
	{
		if (transform.position.x > -maxX)
		{
			render.flipX = false;
			/*
			 * [Note: Method] Transform.Translate(float x, float y, float y): void
			 * 현재 위치에서 입력 된 수만큼 위치를 이동시킵니다.
			 */
			transform.Translate(-1f, 0f, 0f);
		}
		else
		{
			transform.position = new Vector3(-maxX, posY, 0f);
		}
	}

	/*
	 * [Method] MoveLeft(): void
	 * 플레이어를 우측으로 이동시킵니다.
	 */
	public void MoveRight()
	{
		if (transform.position.x < maxX)
		{
			render.flipX = true;
			transform.Translate(1f, 0f, 0f);
		}
		else
		{
			transform.position = new Vector3(maxX, posY, 0f);
		}
	}
}
