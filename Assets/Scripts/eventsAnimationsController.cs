using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class eventsAnimationsController : MonoBehaviour
{
    public GameObject pelotaEventUI;
    public GameObject jugadorEventUI;

    private Animator pelotaAnimator;
    private Animator jugadorAnimator;

    //Lista de sprites de todos los iconos,
    //iconos predeterminados en pos 0 y 1
    private Sprite[] icons;


    private void Start()
    {
        pelotaAnimator = pelotaEventUI.GetComponent<Animator>();
        jugadorAnimator = jugadorEventUI.GetComponent<Animator>();

        loadIcons();

        Debug.Log(icons[0]);

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.F1))
        {
            pelotaAnimator.Play("lookAtMe");
        }

    }

    public IEnumerator pelotaAnimationCoroutine(float pDuracion)
    {
        pelotaAnimator.Play("EventoPelotaAnimation");
        yield return new WaitForSeconds(pDuracion);
        pelotaAnimator.Play("Default");
    }
    public IEnumerator jugadorAnimationCoroutine(float pDuracion)
    {
        jugadorAnimator.Play("EventoJugadorAnimation");
        yield return new WaitForSeconds(pDuracion);
        jugadorAnimator.Play("Default");
    }

    void loadIcons()
    {
        object[] loadedIcons = Resources.LoadAll<Sprite>("EventIcons");

        icons = new Sprite[loadedIcons.Length];

        for(int i = 0; i<icons.Length;i++)
        {
            icons[i] = (Sprite)loadedIcons[i];
        }

    }

}
