using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayCastFov : MonoBehaviour {

    public float range = 50f;
  
    private Camera camera;                                            
    private WaitForSeconds shotDuration = new WaitForSeconds(0.07f);
    private LineRenderer laserLine;


    void Start () 
    {
        laserLine = GetComponent<LineRenderer>();
		camera = Camera.main;
        // fpsCam = GetComponentInParent<Camera>();
    }
    

    void Update () 
    {
        if (Input.GetButtonDown("Fire1")) 
        {
            StartCoroutine (ShotEffect());

            Vector3 rayOrigin = camera.ViewportToWorldPoint (new Vector3(0.5f, 0.5f, 0.0f));

            RaycastHit hit;

            laserLine.SetPosition (0, transform.position);

            if (Physics.Raycast (rayOrigin, camera.transform.forward, out hit, range))
            {
                laserLine.SetPosition (1, hit.point);
            }
            else
            {
                laserLine.SetPosition (1, rayOrigin + (camera.transform.forward * range));
            }
        }
    }


    private IEnumerator ShotEffect()
    {
        // gunAudio.Play ();

        laserLine.enabled = true;

        yield return shotDuration;

        laserLine.enabled = false;
    }
}
