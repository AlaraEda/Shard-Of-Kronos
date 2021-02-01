using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class MeshColliderScript : MonoBehaviour
{
// Copy meshes from children into the parent's Mesh.
// CombineInstance stores the list of meshes.  These are combined
// and assigned to the attached Mesh.
  private MeshFilter[] meshFilters;
    public Mesh meshTemp;
    

    void Start()
    {
        CombineMesh();
        
    }

    private void CombineMesh()
    {
        
        meshFilters = GetComponentsInChildren<MeshFilter>();
        CombineInstance[] combine = new CombineInstance[meshFilters.Length];

        int i = 1;
        while (i < meshFilters.Length)
        {
         
            combine[i].mesh = meshFilters[i].sharedMesh;
            combine[i].transform = meshFilters[i].transform.localToWorldMatrix;
            
            //meshFilters[i].transform.localToWorldMatrix = meshFilters[i].transform.localToWorldMatrix * transform.localToWorldMatrix;
            meshFilters[i].gameObject.SetActive(false);

            i++;
        }

        //var meshFilter = transform.GetComponent<MeshFilter>();
   
        var x =    transform.GetComponent<MeshFilter>().mesh = new Mesh();
        meshTemp = x;
        x.indexFormat = UnityEngine.Rendering.IndexFormat.UInt32;
      
        x.CombineMeshes(combine, true, true);
        x.Optimize();
        //x.indexFormat = UnityEngine.Rendering.IndexFormat.UInt32;
        
        transform.gameObject.SetActive(true);
    }
}