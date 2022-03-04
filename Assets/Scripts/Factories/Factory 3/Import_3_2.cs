using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Import_3_2 : MonoBehaviour
{
    public bool stop_search = false;
    public bool start_load = false;
    public bool start_load_f = false;
    public static bool start_produce = false;
    public bool trigger = false;

    public float time_to_load_bp = 0f;
    public float time_to_load_f = 0f;

    public int id = 0;
    public int expanse = 0;
    public int length = 0;
    public int height = 0;

    public GameObject Import;
    public GameObject Import_Entrance;
    public GameObject Player;
    public GameObject resource_2;

    GameObject[] resources_2;
    GameObject resource_2_load;

    public Vector3 lastpos;
    public Vector3 lastpos_load;

    public int[,] matrix =
    {
        {0, 0, 0, 0, 0, 0},
        {0, 0, 0, 0, 0, 0},
        {0, 0, 0, 0, 0, 0},
        {0, 0, 0, 0, 0, 0},
        {0, 0, 0, 0, 0, 0}
    };
    void Start()
    {
        resources_2 = new GameObject[30];
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
        if (PlayerBackPack.trigger_i && trigger && PlayerBackPack.res_2)
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
                    resources_2[id] = Instantiate(resource_2, PlayerBackPack.resources_on_backpack[PlayerBackPack.id_2].transform.position, Import.transform.rotation);
                    resources_2[id].transform.SetParent(Import.transform);
                    Destroy(PlayerBackPack.resources_on_backpack[PlayerBackPack.id_2]);
                    PlayerBackPack.id_backpack--;
                    PlayerBackPack.res_in_backpack[PlayerBackPack.id_2] = 0;
                    lastpos = resources_2[id].transform.localPosition;
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
                    resources_2[id - 1].transform.localPosition = Vector3.Lerp(lastpos, new Vector3(-0.2f, 0.7f, -0.37f) + new Vector3(0.15f * length, 0, 0.15f * expanse), 1 - time_to_load_bp * 8);
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
                resource_2_load.transform.localPosition = Vector3.Lerp(lastpos_load, new Vector3(0, 0, 0), 1 - time_to_load_f * 2);
                time_to_load_f -= Time.deltaTime;
                if (time_to_load_f < 0)
                {
                    start_produce = true;
                    Destroy(resource_2_load);
                    start_load_f = false;
                }
            }
            else
            {
                if (id > 0)
                {
                    resource_2_load = Instantiate(resource_2, resources_2[id - 1].transform.position, Import.transform.rotation);
                    resource_2_load.transform.SetParent(Import_Entrance.transform);
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
                    lastpos_load = resource_2_load.transform.localPosition;
                    Destroy(resources_2[id - 1]);
                    id--;
                    time_to_load_f = 0.5f;
                }
            }
        }
    }
}
