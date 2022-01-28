using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



[RequireComponent(typeof(Rigidbody))]

public class Movement : MonoBehaviour
{
    [Header("Movement")]

    [SerializeField] private float speed = 300f;
    private Rigidbody rb;

    [Header("Boost Management")]

    [SerializeField] private bool canUseBoost = true;
    private float speedBoost = 5;
    [SerializeField] private float cdBoost = 0f;

    [Header("BoostUI Picture")]

    [SerializeField] RawImage boostReady;
    [SerializeField] RawImage boost1Sec;
    [SerializeField] RawImage boost2Sec;
    [SerializeField] RawImage boost3Sec;

    

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        BoostIsReady();
    }

    private void Update()
    {
        GetInput();
    }

    private void FixedUpdate()
    {  //-6 , 5.8

       

        if (cdBoost != 0)
        {
            if (cdBoost == 1)
            {
                Boost3SecLeft();
            }
            else if (cdBoost == 2)
            {
                Boost2SecLeft();
            }
            else if (cdBoost == 3)
            {
                Boost1SecLeft();
            }
        }
        else
            BoostIsReady();
    }

    void GetInput()
    {
        rb.velocity = new Vector3(Input.GetAxis("Horizontal") * speed * Time.deltaTime, 0, 0);

        if (Input.GetKeyDown(KeyCode.Space) && canUseBoost)
        {
            boostReady.color = new Color32(104, 104, 104, 100);
            canUseBoost = false;
            speed *= speedBoost;

            StartCoroutine(Boost());
        }
    }

    IEnumerator Boost()
    {
        yield return new WaitForSeconds(3f);
        speed /= speedBoost;

        StartCoroutine(TimerBoost());
    }

    IEnumerator TimerBoost()
    {
        while (true)
        {
            cdBoost++;
            yield return new WaitForSeconds(1f);
            if (cdBoost == 4)
            {
                canUseBoost = true;
                cdBoost = 0f;
                boostReady.color = new Color32(255, 255, 255, 255);

                break;
            }
        }
    }

    private void BoostIsReady()
    {
        boostReady.gameObject.SetActive(true);
        boost1Sec.gameObject.SetActive(false);
        boost2Sec.gameObject.SetActive(false);
        boost3Sec.gameObject.SetActive(false);
    }

    private void Boost1SecLeft()
    {
        boostReady.gameObject.SetActive(false);
        boost1Sec.gameObject.SetActive(true);
        boost2Sec.gameObject.SetActive(false);
        boost3Sec.gameObject.SetActive(false);
    }

    private void Boost2SecLeft()
    {
        boostReady.gameObject.SetActive(false);
        boost1Sec.gameObject.SetActive(false);
        boost2Sec.gameObject.SetActive(true);
        boost3Sec.gameObject.SetActive(false);
    }

    private void Boost3SecLeft()
    {
        boostReady.gameObject.SetActive(false);
        boost1Sec.gameObject.SetActive(false);
        boost2Sec.gameObject.SetActive(false);
        boost3Sec.gameObject.SetActive(true);
    }
}
