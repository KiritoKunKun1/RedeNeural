﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public bool train;
    private bool canTrain;
    private bool canShowProgress;

    private double[][] inputs;

    private double[][] outputs;

    private RedeNeural nn;

    // Start is called before the first frame update
    void Start() {
        train = true;
        canTrain = true;
        canShowProgress = true;

        nn = new RedeNeural(2, 3, 1);

        //XOR Problem
        
        inputs = new double[][]
        {
            new double[] { 1, 1},
            new double[] { 1, 0},
            new double[] { 0, 1},
            new double[] { 0, 0}
        };

        outputs = new double[][] 
        { 
            new double[] {0},
            new double[] {1},
            new double[] {1},
            new double[] {0}
        };

        // for (int i = 0; i < 1000; i++) {
        //     int index = Random.Range(0, 2);
        //     nn.train(inputs[index], outputs[index]);
        // }
    }

    // Update is called once per frame
    void Update() {
        for (int i = 0; i < 10; i++) {
            int index = Random.Range(0, 2);
            nn.train(inputs[index], outputs[index]);
        }
        if (canTrain) {
            //StartCoroutine("invokeTrain");
        } else {
            StopCoroutine("invokeTrain");
            canTrain = true;
        }

        if (canShowProgress) {
            //StartCoroutine("showTrainProgress");
        } else { 
            StopCoroutine("showTrainProgress");
            canShowProgress = true;
        }
    }

    IEnumerator invokeTrain() {
        yield return new WaitForSeconds(1f);
        
        if (train) {
            for (int i = 0; i < 1000; i++) {
                int index = Random.Range(0, 4);
                nn.train(inputs[index], outputs[index]);
            }

            if (nn.predict(new double[] {0, 0})[0] < 0.04 && nn.predict(new double[] {1, 0})[0] > 0.98) {
                train = false;
                Debug.Log("Terminou!");
            }
        }

        canTrain = false;
    }

    IEnumerator showTrainProgress() {
        yield return new WaitForSeconds(2f);
        
        if (train) {
            int index = Random.Range(0, 4);
            nn.train(inputs[index], outputs[index]);
            Debug.Log(nn.predict(new double[] {0, 0})[0]);
        }

        canShowProgress = false;
    }
}
