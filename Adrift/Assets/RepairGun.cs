using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepairGun : MonoBehaviour
{
    public LineRenderer repairRay;
    public Transform startPos;

    bool repairing = false;

    GameObject currPart;
    // Start is called before the first frame update
    void Start()
    {
        repairRay.SetPosition(0, startPos.position);
    }

    // Update is called once per frame
    void Update()
    {
        if (repairing)
        {
            // Bit shift the index of the layer (6) to get a bit mask
            int layerMask = 1 << 6;

            RaycastHit hit;
            // Does the ray intersect any objects excluding the player layer
            if (Physics.Raycast(startPos.position, startPos.forward, out hit, 5f))
            {
                if (hit.collider.gameObject.layer == 6)
                {
                    if (currPart != hit.collider.gameObject)
                    {
                        if (currPart != null)
                        {
                            currPart.GetComponent<ShipPart>().repairing = false;
                        }

                        //Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);

                        //set what part we are currently fixing
                        currPart = hit.collider.gameObject;
                        currPart.GetComponent<ShipPart>().repairing = true;
                    }
                }
                else
                {
                    if (currPart != null)
                    {
                        currPart.GetComponent<ShipPart>().repairing = false;
                    }
                }

                repairRay.SetPosition(0, startPos.position);
                repairRay.SetPosition(1, hit.point);
                repairRay.enabled = true;
            }
            else
            {
                if (currPart != null)
                {
                    currPart.GetComponent<ShipPart>().repairing = false;
                }
                repairRay.enabled = false;
            }
        }
        else
        {
            repairRay.enabled = false;  
        }
    }

    public void toggleRepair(bool state)
    {
        if (!repairing)
        {
            repairing = state;
        }
        else
        {
            repairing = state;
        }
    }
}
