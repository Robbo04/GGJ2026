using UnityEngine;
using UnityEngine.Rendering;

public class ForceOutlineCull : MonoBehaviour
{
    void Start ()
    {
        Renderer rend = GetComponent<Renderer>();
        if (rend != null && rend.materials.Length > 1)
        {
            //Pull the multi-material list out of the object
            Material outlineMat = rend.materials[1];
           
            outlineMat.SetInt("_CullMode", (int)CullMode.Front);
            outlineMat.SetInt("_Cull", (int)CullMode.Front);

            outlineMat.renderQueue = (int)RenderQueue.Geometry + 1;
        }
    }
}
