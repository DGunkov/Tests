using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Export_2 : MonoBehaviour
{
    bool start_load = false;
    bool trigger = false;

    float time_to_produce = 0f;
    float time_to_load = 0f;
    float time_to_new_info = 0f;

    int id = 0;
    int expanse = 0;
    int length = 0;

    public GameObject Export;
    public GameObject Export_Exit;
    public GameObject Player;
    public GameObject resource_2;

    GameObject[] resources_2;

    public Text info;

    Vector3 lastpos;

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
        info.text = ("Завод №2: функционирует");
        if(Import_2.start_produce)
        {
            time_to_new_info = 1;
            if (time_to_produce <= 0)
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
                    resources_2[id] = Instantiate(resource_2, Export_Exit.transform.position, Export_Exit.transform.rotation);
                    resources_2[id].transform.SetParent(Export.transform);
                    id++;
                    time_to_produce = 0.25f;
                }
                else
                {
                    info.text = ("Завод №2: нет места на складе");
                }
            }
            else
            {
                time_to_produce -= Time.deltaTime;
                if(id > 0)
                {
                    matrix[length, expanse] = 2;
                    resources_2[id - 1].transform.localPosition = Vector3.Lerp(Export_Exit.transform.localPosition, new Vector3(-0.2f, 0.7f, -0.37f) + new Vector3(0.15f * length, 0, 0.15f * expanse), 1 - time_to_produce * 4);
                }
                if(time_to_produce < 0)
                {
                    Import_2.start_produce = false;
                }
            }
        }
        else
        {
            time_to_new_info -= Time.deltaTime;
            if(time_to_new_info < 0)
            {
                info.text = ("Завод №2: недостаточно ресурсов");
            }
        }
        if (PlayerBackPack.trigger_e && trigger && matrix[0, 0] != 0 && PlayerBackPack.id_backpack < 10)
        {
            start_load = true;
        }
        if (start_load)
        {
            if (time_to_load > 0)
            {
                PlayerBackPack.resources_on_backpack[PlayerBackPack.id_backpack - 1].transform.localPosition = Vector3.Lerp(lastpos, new Vector3(0f, 0.5f, -1.25f) + new Vector3(0, 0.3f * (PlayerBackPack.id_backpack - 1), 0), 1 - time_to_load * 8);
                time_to_load -= Time.deltaTime;
                if (time_to_load < 0)
                {
                    start_load = false;
                    PlayerBackPack.resources_on_backpack[PlayerBackPack.id_backpack - 1].transform.localPosition = new Vector3(0f, 0.5f, -1.25f) + new Vector3(0, 0.3f * (PlayerBackPack.id_backpack - 1), 0);
                }
            }
            else
            {
                if (PlayerBackPack.id_backpack < 10 && id > 0)
                {
                    PlayerBackPack.resources_on_backpack[PlayerBackPack.id_backpack] = Instantiate(resource_2, resources_2[id - 1].transform.position, Player.transform.rotation);
                    PlayerBackPack.resources_on_backpack[PlayerBackPack.id_backpack].transform.SetParent(Player.transform);
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
                    lastpos = PlayerBackPack.resources_on_backpack[PlayerBackPack.id_backpack].transform.localPosition;
                    Destroy(resources_2[id - 1]);
                    id--;
                    PlayerBackPack.res_in_backpack[PlayerBackPack.id_backpack] = 2;
                    PlayerBackPack.id_backpack++;
                    time_to_load = 0.125f;
                }
            }
        }
    }
}
