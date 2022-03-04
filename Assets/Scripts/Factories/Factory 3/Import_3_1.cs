using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Import_3_1 : MonoBehaviour
{
    bool start_load = false;
    bool start_load_f = false;
    public static bool start_produce = false;
    bool trigger = false;

    float time_to_load_bp = 0f;
    float time_to_load_f = 0f;

    int id = 0;
    int expanse = 0;
    int length = 0;

    public GameObject Import;
    public GameObject Import_Entrance;
    public GameObject Player;
    public GameObject resource_1;

    GameObject[] resources_1;
    GameObject resource_1_load;

    Vector3 lastpos;
    Vector3 lastpos_load;

    int[,] matrix =
    {
        {0, 0, 0, 0, 0, 0},
        {0, 0, 0, 0, 0, 0},
        {0, 0, 0, 0, 0, 0},
        {0, 0, 0, 0, 0, 0},
        {0, 0, 0, 0, 0, 0}
    };
    void Start()
    {
        resources_1 = new GameObject[30];
    }
    void OnTriggerStay(Collider other)
    {
        if (other.name == "Player")
        {
            trigger = true;
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.name == "Player")
        {
            trigger = false;
        }
    }
    void FixedUpdate()
    {
        if (PlayerBackPack.trigger_i && trigger && PlayerBackPack.res_1)
        {
            start_load = true;
        }
        if (start_load)
        {
            if (time_to_load_bp <= 0)
            {
                if (id < 30)
                {
                    while (matrix[length, expanse] != 0)
                    {
                        expanse++;
                        if (expanse == 6)
                        {
                            if (length == 4)
                            {
                                break;
                            }
                            else
                            {
                                expanse = 0;
                                length++;
                            }

                        }
                    }
                    resources_1[id] = Instantiate(resource_1, PlayerBackPack.resources_on_backpack[PlayerBackPack.id_1].transform.position, Import.transform.rotation);
                    resources_1[id].transform.SetParent(Import.transform);
                    Destroy(PlayerBackPack.resources_on_backpack[PlayerBackPack.id_1]);
                    PlayerBackPack.id_backpack--;
                    PlayerBackPack.res_in_backpack[PlayerBackPack.id_1] = 0;
                    lastpos = resources_1[id].transform.localPosition;
                    id++;
                    time_to_load_bp = 0.125f;
                }
            }
            else
            {
                time_to_load_bp -= Time.deltaTime;
                if (id > 0)
                {
                    matrix[length, expanse] = 2;
                    resources_1[id - 1].transform.localPosition = Vector3.Lerp(lastpos, new Vector3(-0.2f, 0.7f, -0.37f) + new Vector3(0.15f * length, 0, 0.15f * expanse), 1 - time_to_load_bp * 8);
                }
                if (time_to_load_bp < 0)
                {
                    start_load = false;
                }
            }
        }
        if (matrix[0, 0] == 2)
        {
            start_load_f = true;
        }
        if (start_load_f && !start_produce)
        {
            if (time_to_load_f > 0)
            {
                resource_1_load.transform.localPosition = Vector3.Lerp(lastpos_load, new Vector3(0, 0, 0), 1 - time_to_load_f * 2);
                time_to_load_f -= Time.deltaTime;
                if (time_to_load_f < 0)
                {
                    start_produce = true;
                    Destroy(resource_1_load);
                    start_load_f = false;
                }
            }
            else
            {
                if (id > 0)
                {
                    resource_1_load = Instantiate(resource_1, resources_1[id - 1].transform.position, Import.transform.rotation);
                    resource_1_load.transform.SetParent(Import_Entrance.transform);
                    int del_length = ((id - 1) - ((id - 1) % 6)) / 6;
                    int del_expanse = (id - 1) % 6;
                    if (expanse == 0)
                    {
                        if (length != 0)
                        {
                            length--;
                            expanse = 5;
                        }
                    }
                    else
                    {
                        expanse--;
                    }
                    matrix[del_length, del_expanse] = 0;
                    lastpos_load = resource_1_load.transform.localPosition;
                    Destroy(resources_1[id - 1]);
                    id--;
                    time_to_load_f = 0.5f;
                }
            }
        }
    }
}
