using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spaw : MonoBehaviour
{

    public float distancia = 4.5f;
    public GameObject prefab;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) {

            Instantiate(prefab,new Vector3(this.transform.position.x - distancia, this.transform.position.y, this.transform.position.z), transform.rotation);
            Destroy(this.gameObject);
        }
    }

}
