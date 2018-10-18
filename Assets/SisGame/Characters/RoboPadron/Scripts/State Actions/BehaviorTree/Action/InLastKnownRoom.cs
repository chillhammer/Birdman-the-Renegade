using UnityEngine;

namespace SIS.Characters.Robo
{
	//StateAction that checks if agent is in last known room of player location
	[CreateAssetMenu(menuName = "Characters/RoboPadron/State Actions/Behavior Tree/In Last Known Room")]
	public class InLastKnownRoom : RoboPadronBTAction
	{
		public Map.Dungeon dungeon;
		public override AivoTree.AivoTreeStatus Act(float timeTick, RoboPadron owner)
		{
			Vector3 pos = owner.playerLastKnownLocation.position;
			Room room = dungeon.GetRoom((int)pos.x, (int)pos.z);

			if (room == null)
				return AivoTree.AivoTreeStatus.Failure;

			if (room.rect.Contains(new Vector2(owner.mTransform.position.x, owner.mTransform.position.z)))
			{
				Debug.Log("RoboPadron is in Last Known Room");
				return AivoTree.AivoTreeStatus.Success;
			}

			return AivoTree.AivoTreeStatus.Failure;


		}
	}
}