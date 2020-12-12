using System;
using UnityEngine;
using System.Collections;

public class CreateCells : MonoBehaviour
{
    static int sizeX = 25, sizeY = 25;
    public float FrogRange = 3f;
    public GameObject CubeCreator;
    public GameObject TrueFrog, FalseFrog;
    public Material TrueMaterial, FalseMaterial;
    GameObject[,] CubeArray = new GameObject[sizeX, sizeY];
    bool[,] State = new bool[sizeX, sizeY];
    bool[] Rules = new bool[1<<9];

    void Start()
    {
        for (int x = 0; x < sizeX; x++)
        {
            for (int y = 0; y < sizeY; y++)
            {
                //print("X:" + x);
                CubeArray[x, y] = (GameObject)Instantiate(CubeCreator, new Vector3(x * CubeCreator.transform.localScale.x, 0, y * CubeCreator.transform.localScale.z), transform.rotation);
            }
        }
        RandomizeState();
        RandomizeRules();
        TrueFrog = GameObject.Find("TrueFrog");
        FalseFrog = GameObject.Find("FalseFrog");
    }

    void Update()
    {
        if (Time.frameCount%5==0) {
            bool[,] buff = new bool[sizeX, sizeY];
            for (int x = 0; x < sizeX; x++)
            {
                for (int y = 0; y < sizeY; y++)
                {
                    int newVal = 0;
                    for (int i = 0; i < 9; i++)
                    {
                        int x2 = x + i % 3 - 1, y2 = y + i / 3 - 1;
                        if (x2 >= 0 && x2 < sizeX && y2 >= 0 && y2 < sizeY)
                        {
                            newVal += (1 << i) * Convert.ToInt32(State[x2, y2]);
                        }
                    }
                    buff[x, y] = Rules[newVal];
                }
            }
            for (int x = 0; x < sizeX; x++)
            {
                for (int y = 0; y < sizeY; y++)
                {
                    float trueFrogDist = (TrueFrog.transform.position - CubeArray[x, y].transform.position).sqrMagnitude,
                          falseFrogDist = (FalseFrog.transform.position - CubeArray[x, y].transform.position).sqrMagnitude;
                    if (trueFrogDist<falseFrogDist && trueFrogDist<FrogRange*FrogRange)
                    {
                        State[x, y] = true;
                        CubeArray[x, y].GetComponent<Renderer>().material = TrueMaterial;
                    }
                    else if (falseFrogDist < trueFrogDist && falseFrogDist < FrogRange * FrogRange)
                    {
                        State[x, y] = false;
                        CubeArray[x, y].GetComponent<Renderer>().material = FalseMaterial;
                    }
                    else
                    {
                        State[x, y] = buff[x, y];
                        if (State[x, y])
                        {
                            CubeArray[x, y].GetComponent<Renderer>().material = TrueMaterial;
                        }
                        else
                        {
                            CubeArray[x, y].GetComponent<Renderer>().material = FalseMaterial;
                        }
                    }
                }
            }
        }
    }

    void RandomizeRules()
    {
        for(int i=0; i<1<<9; i++)
        {
            Rules[i] = (UnityEngine.Random.value > 0.5f);
        }
    }

    void RandomizeState()
    {
        for (int x = 0; x < sizeX; x++)
        {
            for (int y = 0; y < sizeY; y++)
            {
                State[x, y] = (UnityEngine.Random.value > 0.5f);
            }
        }
    }
}
