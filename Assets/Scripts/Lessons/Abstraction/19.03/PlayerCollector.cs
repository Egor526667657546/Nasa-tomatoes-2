using UnityEngine;

public class PlayerCollector : MonoBehaviour
{
    [SerializeField] private float collectDistance;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, collectDistance))
            {
                Resource resource = hit.collider.GetComponent<Resource>();
                if (resource != null)
                {
                    resource.Collect();
                    resource.ShowAmmount();
                }
            }
        }
    }
}
