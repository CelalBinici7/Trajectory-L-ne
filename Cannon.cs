using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    [SerializeField] private Projection projection;
    [Header("Handle Movement")]
    [SerializeField] private Ball ballPrefab;
    [SerializeField] private float force = 20f;
    [SerializeField] private Transform rightWheel, leftWheel;
    [SerializeField] private Transform ballSpawn;
    [SerializeField] private Transform barrelPivot;
    [SerializeField] private float rotateSpeed = 30f;
    [SerializeField] private AudioClip clip;
    [SerializeField] private AudioSource source;
    [SerializeField] private ParticleSystem launchParticles;

    void Update()
    {
        HandleControls();
        projection.simulateTrajectory(ballPrefab,ballSpawn.position,ballSpawn.forward * force);
    }

    private void HandleControls()
    {
        if (Input.GetKey(KeyCode.S))
        {
            barrelPivot.Rotate(Vector3.right * rotateSpeed * Time.deltaTime);

        }else if (Input.GetKey(KeyCode.W))
        {
            barrelPivot.Rotate(Vector3.left * rotateSpeed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.down * rotateSpeed * Time.deltaTime);
            leftWheel.Rotate(Vector3.forward * rotateSpeed *Time.deltaTime);
            rightWheel.Rotate(Vector3.back * rotateSpeed * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(Vector3.up * rotateSpeed * Time.deltaTime);
            leftWheel.Rotate(Vector3.back * rotateSpeed * Time.deltaTime);
            rightWheel.Rotate(Vector3.forward * rotateSpeed * Time.deltaTime);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            var spawned = Instantiate(ballPrefab,ballSpawn.position,ballSpawn.rotation);
            spawned.Init(ballSpawn.forward * force,false);
            launchParticles.Play();
            source.PlayOneShot(clip);
        }
    }
}
