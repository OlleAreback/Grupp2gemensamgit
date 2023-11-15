using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainScript : MonoBehaviour
{
    // Start is called before the first frame update
    private void Start()
    {
        FindObjectOfType<AudioManager>().Play("Birds");
        FindObjectOfType<AudioManager>().Play("MainTheme");
    }

    // Update is called once per frame
    private void Update()
    {
    }
}