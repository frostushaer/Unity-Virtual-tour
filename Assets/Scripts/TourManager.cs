using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TourManager : MonoBehaviour
{
    // list of gates
    public GameObject[] objGates;
    // mian menu
    public GameObject canvasMainMenu;
    // should the camera move
    public bool isCameraMove = false;
    // Start is called before the first frame update
    void Start()
    {
        ReturnToMenu();
    }

    // Update is called once per frame
    void Update()
    {
        if(isCameraMove) {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                ReturnToMenu();
            }
        }
        if(Input.GetMouseButtonDown(0)) {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if(Physics.Raycast(ray, out hit, 100.0f)) {
                if (hit.transform.gameObject.tag == "Sound")
                {
                    hit.transform.gameObject.GetComponent<MediaAudio>().PlayAudio();
                }
                else if (hit.transform.gameObject.tag == "Image")
                {
                    hit.transform.gameObject.GetComponent<MediaImage>().ShowImage();
                }
                else if (hit.transform.gameObject.tag == "NextSite")
                {
                    LoadGate(hit.transform.gameObject.GetComponent<NextSite>().GetSiteToLoad());
                }
            }
        }
    }

    public void LoadGate(int gateNumber) {
         // hide sites
        for (int i = 0; i < objGates.Length; i++)
        {
            objGates[i].SetActive(false);
        }
        // show gate
        objGates[gateNumber].SetActive(true);
        // hide menu
        canvasMainMenu.SetActive(true);
        // enable the camera
        isCameraMove = true;
        GetComponent<CameraContoller>().ResetCamera();
    }

    public void ReturnToMenu() {
        // show menu 
        canvasMainMenu.SetActive(true);
        // hide sites
        for (int i = 0; i < objGates.Length; i++)
        {
            objGates[i].SetActive(false);
        }
        // disable the camera
        isCameraMove = false;
    }

    public void ReturnToSite() {
        isCameraMove = true;
    }

    public void OpenMedia() {
        isCameraMove = false;
    }
}
