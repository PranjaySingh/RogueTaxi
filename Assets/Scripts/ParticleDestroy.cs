using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleDestroy : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Invoke("DestroyGameObj", 2f);
    }

    public void DestroyGameObj()
    {
        Destroy(gameObject);
    }
}
