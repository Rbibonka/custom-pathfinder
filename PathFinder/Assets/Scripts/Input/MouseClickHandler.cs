using System;
using UnityEngine;

public class MouseClickHandler : MonoBehaviour
{
    public event Action<Player> playerClicked;
    public event Action<Vector3> planeClicked;

    [SerializeField]
    private Camera mainCamera;

    [SerializeField]
    private LayerMask planeLayerMask;

    [SerializeField]
    private LayerMask playerLayerMask;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                if ((planeLayerMask.value & (1 << hit.collider.gameObject.layer)) != 0)
                {
                    planeClicked?.Invoke(hit.point);
                }
                else if ((playerLayerMask.value & (1 << hit.collider.gameObject.layer)) != 0)
                {
                    if (hit.collider.gameObject.TryGetComponent(out Player player))
                    {
                        playerClicked?.Invoke(player);
                    }
                }
            }
        }
    }
}