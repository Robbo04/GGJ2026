using UnityEditorInternal;
using UnityEngine;

public class WaterInteraction : MonoBehaviour
{
    public GameObject Ocean;
    public float waterLevel;
    public GameObject playerWaterHeightMarker;
    public bool isUnderwater => this.transform.position.y < waterLevel;

    void Start()
    {
        if (Ocean != null)
        {
            waterLevel = Ocean.GetComponent<Transform>().position.y;
        }
    }

    // public GameObject hitObject;
    // public Vector3 collision = Vector3.zero;

    // void Update()
    // {
    //     var ray = new Ray(this.transform.position + transform.forward, this.transform.forward);
    //     RaycastHit hit;
    //     if (Physics.Raycast(ray, out hit, 100))
    // {
    //         hitObject = hit.transform.gameObject;
    //         collision = hit.point;
    //         Debug.DrawRay(transform.position, transform.forward * 100, Color.green);
    //     }
    // }

    // void OnDrawGizmos()
    // {
    //     Gizmos.color = Color.red;
    //     Gizmos.DrawWireSphere(collision, 0.2f);
    // }
}
