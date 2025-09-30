using UnityEngine;
using System.Collections;

public class CubeBehaviour : MonoBehaviour
{
    [SerializeField] private Color defaultColor = Color.gray;
    private Renderer _renderer;
    private bool _activated = false;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
    }

    private void OnEnable()
    {
        _activated = false;
        _renderer.material.color = defaultColor;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!_activated && collision.gameObject.CompareTag("Platform"))
        {
            _activated = true;
            
            _renderer.material.color = Random.ColorHSV();
            
            float lifetime = Random.Range(2f, 5f);
            StartCoroutine(DeactivateAfterTime(lifetime));
        }
    }

    private IEnumerator DeactivateAfterTime(float time)
    {
        yield return new WaitForSeconds(time);
        CubePool.Instance.ReturnCube(gameObject);
    }
}