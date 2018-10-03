using UnityEngine;

namespace SO
{
    public class AssignLocalPosition : MonoBehaviour
    {
        public Vector3Variable vectorVariable;

		private void OnEnable()
		{
			vectorVariable.value = this.transform.position;
			Destroy(this);
		}

	}
}
