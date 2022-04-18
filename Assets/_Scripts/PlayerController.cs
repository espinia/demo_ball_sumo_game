using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    private Rigidbody _rigidBody;
    public float moveForce;    
    
    public GameObject focalPoint;

    public bool hasPowerUp;
    public float powerUpForce;
    public float powerUpTime;

    public GameObject[] powerUpIndicators;

    // Start is called before the first frame update
    void Start()
    {
        _rigidBody = GetComponent<Rigidbody>();
        //es mejor que sea privada y buscarla por nombre
        //focalPoint = GameObject.Find("Focal Point");
    }

    // Update is called once per frame
    void Update()
    {
        float forwardInput = Input.GetAxis("Vertical");
        _rigidBody.AddForce(focalPoint.transform.forward * moveForce * forwardInput,
                            ForceMode.Force);

        foreach (GameObject indicator in powerUpIndicators)
		{
            indicator.transform.position = transform.position+ 0.5f*Vector3.down;
		}

        /*UNA FORMA POR ALTURA
        if (transform.position.y < -10)
        {
            SceneManager.LoadScene("Prototype 4");
        }*/
    }

	private void OnTriggerEnter(Collider other)
	{
		if(other.CompareTag("PowerUp"))
		{
            Debug.Log("Trigger power up");
            hasPowerUp = true;
            Destroy(other.gameObject);

            //iniciar la corrutina
            StartCoroutine(PowerUpCountdown());
        }
        else if (other.gameObject.name.CompareTo("KillZone")==0)
		{
            SceneManager.LoadScene("Prototype 4");
        }
	}

	private void OnCollisionEnter(Collision collision)
	{
		if(hasPowerUp && collision.gameObject.CompareTag("Enemy"))
		{
            Rigidbody enemyRigidBody = collision.gameObject.GetComponent<Rigidbody>();
            //no se normaliza porque siemre es la misma distancia entre los radios de las esferas
            Vector3 awayFromPlayer = collision.gameObject.transform.position - transform.position;
            
            enemyRigidBody.AddForce(awayFromPlayer * powerUpForce, ForceMode.Impulse);            

            Debug.Log("Jugador colisiona contra " + collision.gameObject +
                " tiene power up");
		}
	}

	//se autogobierna sin bloquear el hilo principal, es sin frenar el frame
    //las corrutinas pueden esperar
	IEnumerator PowerUpCountdown()
	{
        //con yield return se termina la corrutina
        float indicatorTime = powerUpTime / powerUpIndicators.Length;

		foreach (GameObject indicator in powerUpIndicators)
        {
            indicator.gameObject.SetActive(true);
            // esperar alguna condición por ejemplo con Wait
            yield return new WaitForSeconds(indicatorTime);

            //cuando termina de esperar sigue
            indicator.gameObject.SetActive(false);
        }

        hasPowerUp = false;		
	}
}
