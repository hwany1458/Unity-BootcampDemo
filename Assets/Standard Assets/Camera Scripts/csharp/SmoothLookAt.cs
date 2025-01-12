﻿using UnityEngine;
using System.Collections;

public class SmoothLookAt : MonoBehaviour
{
    public Transform target;
    public float damping = 6.0f;
    public bool smooth = true;
    private float alpha = 1.0f;
    public float minDistance = 10.0f;
    private Color color;
    public string property;

    void Start()
    {

        if (GetComponent<Renderer>().material.HasProperty(property))
        {
            color = GetComponent<Renderer>().material.GetColor(property);
        }
        else
        {
            property = "";
        }

        // Make the rigid body not change rotation
        if (GetComponent<Rigidbody>())
            GetComponent<Rigidbody>().freezeRotation = true;
    }

    void LateUpdate()
    {
        if (target)
        {
            if (smooth)
            {
                // Look at and dampen the rotation
                var rotation = Quaternion.LookRotation(target.position - transform.position);
                transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * damping);
            }
            else
            {
                // Just lookat
                transform.rotation = Quaternion.FromToRotation(-Vector3.forward, (new Vector3(target.position.x, transform.position.y, target.position.z) - transform.position).normalized);

                float distance = (target.position - transform.position).magnitude;

                if (distance < minDistance)
                {
                    alpha = Mathf.Lerp(alpha, 0.0f, Time.deltaTime * 2f);
                }
                else
                {
                    alpha = Mathf.Lerp(alpha, 1.0f, Time.deltaTime * 2f);
                }

                if (!string.IsNullOrEmpty(property))
                {
                    color.a = Mathf.Clamp(alpha, 0.0f, 1.0f);

                    GetComponent<Renderer>().material.SetColor(property, color);
                }
            }
        }
    }
}
