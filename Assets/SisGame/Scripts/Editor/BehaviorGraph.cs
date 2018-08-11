using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SIS.States;
using SIS.Characters;

public class abc { }
namespace SIS.BehaviorEditor
{
    //[CreateAssetMenu]
    public class BehaviorGraph<C> : ScriptableObject where C : Character
    {
	[SerializeField]
        public List<BaseNode<C>> windows = new List<BaseNode<C>>();
	[SerializeField]
        public int idCount;
        List<int> indexToDelete = new List<int>();

        #region Checkers
        public BaseNode<C> GetNodeWithIndex(int index)
        {
            for (int i = 0; i < windows.Count; i++)
            {
                if (windows[i].id == index)
                    return windows[i];
            }

            return null;
        }

        public void DeleteWindowsThatNeedTo()
        {
            for (int i = 0; i < indexToDelete.Count; i++)
            {
                BaseNode<C> b = GetNodeWithIndex(indexToDelete[i]);
                if(b != null)
                    windows.Remove(b);
            }

            indexToDelete.Clear();
        }

        public void DeleteNode(int index)
        {
			if(!indexToDelete.Contains(index))
				indexToDelete.Add(index);
        }

        public bool IsStateDuplicate(BaseNode<C> b)
        {
            for (int i = 0; i < windows.Count; i++)
            {
                if (windows[i].id == b.id)
                    continue;

                if (windows[i].stateRef.currentState == b.stateRef.currentState &&
                    !windows[i].isDuplicate)
                    return true;
            }
             
            return false;
        }

        public bool IsTransitionDuplicate(BaseNode<C> b)
        {
            BaseNode<C> enter = GetNodeWithIndex(b.enterNode);
            if (enter == null)
            {
                Debug.Log("false");
                return false;
            }
            for (int i = 0; i < enter.stateRef.currentState.transitions.Count; i++)
            {
                Transition<C> t = enter.stateRef.currentState.transitions[i];
                if (t.condition == b.transRef.previousCondition && b.transRef.transitionId != t.id)
                {
                    return true;
                }
            }

            return false;
        }

        #endregion

    }
}
