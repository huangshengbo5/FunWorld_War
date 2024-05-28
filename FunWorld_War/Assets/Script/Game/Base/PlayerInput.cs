using UnityEngine;

namespace Script.Game.Base
{
    public class PlayerInput : MonoBehaviour
    {
        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    var terrian = hit.collider.gameObject.GetComponent<Terrain>();
                    if (terrian != null)
                    {
                        var postion = hit.point;
                    }
                }
            }
        }
    }
}