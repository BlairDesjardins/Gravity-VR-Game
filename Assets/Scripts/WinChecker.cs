using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Valve.VR.InteractionSystem
{
    public class WinChecker : MonoBehaviour
    {
        public SteamVR_ActionSet actionSetEnable;
        public Canvas WinUI;

        private GameObject[] pins;
        private float timeForWin = 3f;
        private float winTimer;

        void Start()
        {
            pins = GameObject.FindGameObjectsWithTag("Pin");
            winTimer = timeForWin;
        }

        void Update()
        {
            int pinsKnocked = 0;
            foreach (GameObject pin in pins)
            {
                if (pin.GetComponent<KnockedChecker>().getKnockedDown())
                    pinsKnocked++;
                else
                    winTimer = timeForWin;
                if (pinsKnocked == pins.Length)
                    winTimer -= Time.deltaTime;
            }

            if (winTimer <= 0)
                Victory();
        }

        private void Victory()
        {
            actionSetEnable.ActivatePrimary();
            WinUI.gameObject.SetActive(true);

        }
    }
}
