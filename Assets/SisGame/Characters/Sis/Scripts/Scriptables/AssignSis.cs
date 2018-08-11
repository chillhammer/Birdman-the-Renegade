using UnityEngine;

namespace SIS.Characters.Sis
{
	[RequireComponent(typeof(Sis))]
    public class AssignSis : MonoBehaviour
    {
        public SisVariable sisVariable;

		private void OnEnable()
		{
			sisVariable.value = GetComponent<Sis>();
			Destroy(this);
		}

	}
}
