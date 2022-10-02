using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Car : MonoBehaviour
{
    private Vector3 moveForce;
    public float moveSpeed = 30f;
    public float maxSpeed = 10f;
    public float steerAngle = 20f;
    public float traction = 1f;

    public GameObject carParticleSystem;

    float angle1, angle2, angleDiff;
    int rotations = 0;

    bool onDriftMat = false;

    GameObject scoreText;
    GameObject scoreTextParent;
    AudioSource audio;

    private void Start()
    {
        audio = gameObject.GetComponent<AudioSource>();
    }

    void Update()
    {
        //move
        moveForce += transform.forward * moveSpeed * Time.deltaTime;
        transform.position += moveForce * Time.deltaTime;

        //max speed
        moveForce = Vector3.ClampMagnitude(moveForce, maxSpeed);

        //steer input
        angle1 = transform.rotation.eulerAngles.y;
        if (angle1 > 180)
        {
            angle1 -= 360;
        }

        //float steerInput = Input.GetAxis("Horizontal");
        float steerInput;

        if (Input.GetMouseButtonDown(0))
        {
            audio.Play();
        }

        if (Input.GetMouseButton(0))
        {
            if (Input.mousePosition.x < Screen.width / 2)
            {
                steerInput = -1;
            }
            else
                steerInput = 1;
        }
        else
            steerInput = 0;

        if (Input.GetMouseButtonUp(0))
        {
            audio.Stop();
        }

        transform.Rotate(Vector3.up * steerInput * moveForce.magnitude * steerAngle * Time.deltaTime);

        if (((Mathf.Abs(angle1) > 170)&&(Mathf.Abs(angle2) <= 170)) || ((Mathf.Abs(angle1) < 170) && (Mathf.Abs(angle2) >= 170))) 
        {
            angleDiff += Mathf.Sign(angle1 - angle2); // sign() should return 1, 0 or -1
        }

        if(Mathf.Abs(angleDiff) == 2)
        {
            rotations++;
            if (onDriftMat)
            {
                int a = int.Parse(scoreText.GetComponent<TextMeshPro>().text);
                if (a > 1)
                {
                    a--;
                    scoreText.GetComponent<TextMeshPro>().text = a.ToString();
                }
                else
                {
                    Destroy(scoreTextParent.gameObject);
                    onDriftMat = false;
                }
                    
            }
            Debug.Log(rotations);
            angleDiff = 0;
        }
        angle2 = angle1;

        //traction
        moveForce = Vector3.Lerp(moveForce.normalized, transform.forward, traction * Time.deltaTime) * moveForce.magnitude;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "DriftingMat")
        {
            onDriftMat = true;
            scoreTextParent = other.gameObject;
            scoreText = other.gameObject.transform.GetChild(0).gameObject;
        }
        else
        {
            Debug.Log("Player Died");
            Destroy(gameObject);
            if (carParticleSystem != null)
            {
                Vector3 particlePosition = new Vector3(transform.position.x, 1, transform.position.z);
                Instantiate(carParticleSystem, particlePosition, Quaternion.identity);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "DriftingMat")
            onDriftMat = false;
    }
}
