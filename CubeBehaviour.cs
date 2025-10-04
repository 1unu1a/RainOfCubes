using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Renderer))]
[RequireComponent(typeof(Rigidbody))]
public class CubeBehaviour : MonoBehaviour, IPoolObject
{
    [Header("Visuals")]
    [SerializeField] private Color defaultColor = Color.gray;
    [SerializeField] private Renderer _renderer;

    [Header("Physics")]
    [SerializeField] private Rigidbody _rigidbody;

    private bool _activated;

    private void Reset()
    {
        _renderer = GetComponent<Renderer>();
        _rigidbody = GetComponent<Rigidbody>();
    }

    public void Init()
    {
        _activated = false;
        _renderer.material.color = defaultColor;
        ResetPhysics();
    }

    public void DeInit()
    {
        StopAllCoroutines();
        _activated = false;
        _renderer.material.color = defaultColor;
    }

    public void ResetPhysics()
    {
        _rigidbody.linearVelocity = Vector3.zero;
        _rigidbody.angularVelocity = Vector3.zero;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (_activated) return;

        if (collision.gameObject.TryGetComponent(out Platform _))
        {
            _activated = true;
            _renderer.material.color = Random.ColorHSV();

            float lifetime = Random.Range(2f, 5f);
            StartCoroutine(ReturnToPoolAfterTime(lifetime));
        }
    }

    private IEnumerator ReturnToPoolAfterTime(float time)
    {
        yield return new WaitForSeconds(time);
        CubeSpawner.Pool.Return(this);
    }
}