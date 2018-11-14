using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SIS.Characters.Sis;
using SIS.Items.Weapons;
using TMPro;

namespace SIS.HUD
{
	//Also Manages Stage End and Win Condition
	public class StageTextHUD : MonoBehaviour
	{
		public TextMeshProUGUI middleText;
		public TextMeshProUGUI topLeftText;
		public SO.IntVariable stageIndexVar;

		float moveSpeed = 1f;
		public float moveTowardsSpeed = 1f;
		public float moveAwaySpeed = 2f;
		public float firstStageSpeed = 2f;
		public SO.GameEvent stageStart;
		public SO.GameEvent gameWon;
		public SIS.GameControl.StageListing stageListing;
		public TextMeshProUGUI topRightText;
		public SO.IntVariable score;

		TextMeshProUGUI text;
		Vector3 targetPosition;
		Vector3 targetScale;
		public bool targetIsMiddle = true;

		private void Start()
		{
			text = GetComponent<TextMeshProUGUI>();
			targetPosition = middleText.rectTransform.position;
			targetScale = middleText.rectTransform.localScale;
		}


		// Update is called once per frame
		void Update()
		{
			moveSpeed = (stageIndexVar.value == 0 ? firstStageSpeed : (targetIsMiddle ? moveTowardsSpeed : moveAwaySpeed));
			text.rectTransform.position = Vector3.Lerp(text.rectTransform.position, targetPosition, moveSpeed * Time.deltaTime);
			text.rectTransform.localScale = Vector3.Lerp(text.rectTransform.localScale, targetScale, moveSpeed * Time.deltaTime);

			if (!stageListing.IsLastStage())
			{
				text.SetText("Stage " + (stageIndexVar.value + 1).ToString());
				if (stageListing.IsEndlessMode())
					text.SetText("Endless  Mode");
			}
			else
				text.SetText("Final Stage");

			if (targetIsMiddle)
			{
				targetPosition = middleText.rectTransform.position;
				targetScale = middleText.rectTransform.localScale;
			}
			else
			{
				targetPosition = topLeftText.rectTransform.position;
				targetScale = topLeftText.rectTransform.localScale;
			}

			if (stageListing.IsEndlessMode())
			{
				topRightText.text = "Score: " + score.value;
			} else
			{
				topRightText.text = "";
			}
		}


		public void StageEnded()
		{
			StartCoroutine("StageEndSequence");
		}

		//Outside, usually will be from Initial Stage Start
		public void StageStarted()
		{
			targetIsMiddle = false;
		}

		public void EnterEndlessMode()
		{
			//Increase stage index in game control
		}

		IEnumerator StageEndSequence()
		{
			yield return new WaitForSeconds(1.5f);
			if (stageListing.IsLastStage())
			{
				gameWon.Raise();
			}
			else
			{
				float additionalTime = (stageIndexVar.value == 0 ? 1f : 0f);
				targetIsMiddle = true;
				yield return new WaitForSeconds(2f + additionalTime * 2);
				stageIndexVar.value++;
				yield return new WaitForSeconds(2f + additionalTime);
				stageStart.Raise();
			}
		}
	}
}