using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField]private Rigidbody rb;
    [SerializeField]private AudioSource source;
    [SerializeField]private AudioClip[] clips;
    [SerializeField]private GameObject poolPrefab;
    private bool _isGhost;

    public void Init(Vector3 velocity , bool isGhost)
    {
        _isGhost = isGhost;
        rb.AddForce(velocity,ForceMode.Impulse );
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (_isGhost) return;
        Instantiate(poolPrefab, collision.contacts[0].point, Quaternion.Euler(collision.contacts[0].normal));
        source.clip = clips[Random.Range(0,clips.Length)];
        source.Play();
    }

}
