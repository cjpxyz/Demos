using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grappling : MonoBehaviour
{
    public GameObject hook;
    public GameObject hookHolder;

    public float hookTravelSpeed;
    public float playerTravelSpeed;

    public static bool fired;
    public bool hooked;
    public GameObject hookedObj;

    public float maxDitance;
    private float currentDitance;
    private bool grounded;

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && fired == false)
        {
            fired = true;
        }

        if (fired)
        {
            LineRenderer rope = hook.GetComponent<LineRenderer>();
            rope.positionCount = 2;
            rope.SetPosition(0, hookHolder.transform.position);
            rope.SetPosition(1, hook.transform.position);
        }

        if (fired == true && hooked == false)
        {
            //hook.transform.Translate(Camera.main.transform.forward * Time.deltaTime * hookTravelSpeed);

            hook.transform.position += Camera.main.transform.forward * (Time.deltaTime * hookTravelSpeed);
            currentDitance = Vector3.Distance(transform.position, hook.transform.position);

            if (currentDitance >= maxDitance)
            {
                ReturnHook();
            }
        }

        if(hooked == true && fired == true)
        {
            GetComponent<Rigidbody>().useGravity = false;
            hook.GetComponent<MeshRenderer>().enabled = false;

            hook.transform.parent = hookedObj.transform;

            transform.position = Vector3.MoveTowards(transform.position, hook.transform.position, Time.deltaTime * playerTravelSpeed);
            float distanceToHook = Vector3.Distance(transform.position, hook.transform.position);

            if(distanceToHook < 1)
            {
                if(grounded == false)
                {
                    this.transform.Translate(Vector3.forward * Time.deltaTime * 13f);
                    this.transform.Translate(Vector3.up * Time.deltaTime * 18f);
                }

                StartCoroutine("Climb");
            }
        }
        else
        {
            GetComponent<Rigidbody>().useGravity = true;
            hook.transform.parent = hookHolder.transform;
            hook.GetComponent<MeshRenderer>().enabled = false;
        }
    }

    IEnumerator Climb()
    {
        yield return new WaitForSeconds(0.1f);
        ReturnHook();
    }

    private void ReturnHook()
    {
        hook.transform.parent = hookHolder.transform;
        hook.transform.rotation = hookHolder.transform.rotation;
        hook.transform.position = hookHolder.transform.position;
        fired = false;
        hooked = false;

        hook.transform.localPosition = new Vector3(0, 0, 0);
        hook.transform.localScale = new Vector3(0.28f, 0.28f, 0.28f);

        LineRenderer rope = hook.GetComponent<LineRenderer>();
        rope.positionCount = 0;
    }

    void CheckIfGrounded()
    {
        RaycastHit hit;
        float distance = 1f;
        Vector3 dir = new Vector3(0, -1);

        if (Physics.Raycast(transform.position, dir, out hit, distance))
        {
            grounded = true;
        }
        else
        {

        }
    }
}
