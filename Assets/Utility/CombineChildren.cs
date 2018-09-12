using UnityEngine;
using System.Collections;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class CombineChildren : MonoBehaviour
{
	void Start()
	{
		MeshFilter[] meshFilters = GetComponentsInChildren<MeshFilter>();
		CombineInstance[] combine = new CombineInstance[meshFilters.Length - 1];
		Material firstMaterial = meshFilters[1].GetComponent<MeshRenderer>().material;
		int i = 0;
		while (i < combine.Length)
		{
			combine[i].mesh = meshFilters[i + 1].sharedMesh;
			combine[i].transform = meshFilters[i + 1].transform.localToWorldMatrix;
			//meshFilters[i].gameObject.SetActive(false);
			meshFilters[i].gameObject.GetComponent<MeshRenderer>().enabled = false;
			i++;
		}
		transform.GetComponent<MeshFilter>().mesh = new Mesh();
		transform.GetComponent<MeshFilter>().mesh.CombineMeshes(combine);
		//transform.gameObject.SetActive(true);
		MeshRenderer renderer = GetComponent<MeshRenderer>();
		renderer.enabled = true;
		renderer.material = firstMaterial;
	}
}