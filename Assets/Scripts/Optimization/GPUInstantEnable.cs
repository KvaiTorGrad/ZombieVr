using UnityEngine;

public class GPUInstantEnable : MonoBehaviour
{
    private void Awake()
    {
        MaterialPropertyBlock materialPropertyBlock = new MaterialPropertyBlock();
        if(transform.TryGetComponent(out MeshRenderer meshRenderer))
            meshRenderer.SetPropertyBlock(materialPropertyBlock);
        else
        {
            SkinnedMeshRenderer skinnedMeshRenderer = transform.GetComponent<SkinnedMeshRenderer>();
            skinnedMeshRenderer.SetPropertyBlock(materialPropertyBlock);
        }
    }
}
