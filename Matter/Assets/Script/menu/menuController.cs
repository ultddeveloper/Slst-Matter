﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class menuController : MonoBehaviour
{
    public GameObject logointro;
    public GameObject canvas, background, logo, startGame, options;
    public GameObject slttl, backbtn, sl1, sl2, sl3;
    public GameObject entername, et_input, et_confirm, et_return;
    private int instatus; // 1 = canvas, 2 = chose game, 3 = options menu
    public bool enableLogoAnimation;
    private bool firstrun, inOptions;
    void Start()
    {
        canvas.SetActive(false);
        slttl.SetActive(false);
        sl1.SetActive(false);
        sl2.SetActive(false);
        sl3.SetActive(false);
        backbtn.SetActive(false);
        firstrun = true;
        inOptions = false;
        if (enableLogoAnimation){ logointro.SetActive(true); playlogo();}
        else{canvasin();}
    }

    void playlogo()
    {
        logointro.GetComponent<Animator>().Play("logo");
    }

    public void canvasin()
    {
        instatus = 1;
        StartCoroutine(passiveCanvasin());
    }

    public void canvasout()
    {
        StartCoroutine(passiveCanvasout());
    }

    public void choseSlot()
    {
        instatus = 2;
        StartCoroutine(passiveCanvasout());
        GetComponent<datacontrol>().setslotvals();
        StartCoroutine(passiveCallSlot());
    }

    public void backSlot()
    {
        StartCoroutine(passiveExitSlot());
    }

    void optionsbuttonshow()
    {
        options.SetActive(true);
        options.GetComponent<Animator>().Play("options-intro-fade");
    }

    public void optionsclicked()
    {   
        if (!inOptions && instatus == 1) {StopCoroutine(passiveCanvasin()); canvasout();}
        else if (!inOptions && instatus == 2) {StopCoroutine(passiveCallSlot()); backSlot();}
        if (!inOptions) {options.GetComponent<optionsMenu>().entermenu(); inOptions = true;}

    }

    public void returnFromMenu()
    {
        Debug.Log("mC returnFromMenu called");
        if (instatus == 1) {canvasin();}
        else if (instatus == 2) {choseSlot();}
    }
    // passive programs below -------------------------------------------------

    IEnumerator passiveCanvasin()
    {   
        canvas.SetActive(true);
        logo.SetActive(false);
        startGame.SetActive(false);
        if (firstrun) {optionsbuttonshow(); yield return new WaitForSeconds(1); firstrun = false;}
        logo.SetActive(true);
        logo.GetComponent<Animator>().Play("logo-intro");
        yield return new WaitForSeconds(1);
        startGame.SetActive(true);
        startGame.GetComponent<Animator>().Play("startgame-intro-fade");
        inOptions = false;
    }

    IEnumerator passiveCanvasout()
    {
        logo.GetComponent<Animator>().Play("logo-outro");
        startGame.GetComponent<Animator>().Play("startgame-outro-fade");
        yield return new WaitForSeconds(1);
        logo.SetActive(false);
        startGame.SetActive(false);
    }

    IEnumerator passiveCallSlot()
    {
        yield return new WaitForSeconds(1);
        slttl.SetActive(true);
        backbtn.SetActive(true);
        slttl.GetComponent<Animator>().Play("slttl-intro-fade");
        backbtn.GetComponent<Animator>().Play("slotBack-intro-fade");
        yield return new WaitForSeconds(0.2f);
        sl1.SetActive(true);
        sl1.GetComponent<Animator>().Play("s1-intro-fade");
        yield return new WaitForSeconds(0.2f);
        sl2.SetActive(true);
        sl2.GetComponent<Animator>().Play("s2-intro-fade");
        yield return new WaitForSeconds(0.2f);
        sl3.SetActive(true);
        sl3.GetComponent<Animator>().Play("s3-intro-fade");
        inOptions = false;
    }

    IEnumerator passiveExitSlot()
    {
        slttl.GetComponent<Animator>().Play("slttl-outro-fade");
        backbtn.GetComponent<Animator>().Play("slotBack-outro-fade");
        yield return new WaitForSeconds(0.2f);
        sl1.GetComponent<Animator>().Play("s1-outro-fade");
        yield return new WaitForSeconds(0.2f);
        sl2.GetComponent<Animator>().Play("s2-outro-fade");
        yield return new WaitForSeconds(0.2f);
        sl3.GetComponent<Animator>().Play("s3-outro-fade");
        yield return new WaitForSeconds(1);
        slttl.SetActive(false);
        backbtn.SetActive(false);
        sl1.SetActive(false);
        sl2.SetActive(false);
        sl3.SetActive(false);
        canvasin();
    }

}
