using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBackPack : MonoBehaviour
{
    public static int id_backpack = 0;
    public static int id_1 = 0;
    public static int id_2 = 0;
    public static int id_3 = 0;

    public static bool trigger_e = false;
    public static bool trigger_i = false;

    public static bool res_1 = false;
    public static bool res_2 = false;
    public static bool res_3 = false;
    //Массив для записи вида ресурса
    public static int[] res_in_backpack= {0,0,0,0,0,0,0,0,0,0};

    public static GameObject[] resources_on_backpack;

    public Text text_number_1;
    public Text text_number_2;
    public Text text_number_3;

    int number_1;
    int number_2;
    int number_3;
    //Нахождение в триггерной зоне игрока
    void OnTriggerStay(Collider other)
    {
        if(other.name == "Export")
        {
            trigger_e = true;
        }
        if (other.name == "Import" || other.name == "Import_1" || other.name == "Import_2")
        {
            trigger_i = true;
        }
    }
    //Выход из триггерной зоны игрока
    void OnTriggerExit(Collider other)
    {
        if (other.name == "Export")
        {
            trigger_e = false;
        }
        if (other.name == "Import" || other.name == "Import_1" || other.name == "Import_2")
        {
            trigger_i = false;
        }
    }
    void Start()
    {
        resources_on_backpack = new GameObject[10];
    }

    void Update()
    {
        //Подсчёт каждого вида ресурсов и вывод в UI интерфейс
        for (int id = 0; id < 10; id++)
        {
            if (res_in_backpack[id] == 1)
            {
                number_1++;
            }
            if (res_in_backpack[id] == 2)
            {
                number_2++;
            }
            if (res_in_backpack[id] == 3)
            {
                number_3++;
            }
        }
        text_number_1.text = ("" + number_1);
        text_number_2.text = ("" + number_2);
        text_number_3.text = ("" + number_3);
        number_1 = 0;
        number_2 = 0;
        number_3 = 0;
        //Вычисление id верхнего блока каждого вида на спине игрока
        for (int id = 9; id != -1; id--)
        {
            if(res_in_backpack[id] == 1)
            {
                id_1 = id;
                res_1 = true;
                break;
            }
            if(id == 0)
            {
                res_1 = false;
            }
        }
        for (int id = 9; id != -1; id--)
        {
            if (res_in_backpack[id] == 2)
            {
                id_2 = id;
                res_2 = true;
                break;
            }
            if (id == 0)
            {
                res_2 = false;
            }
        }
        for (int id = 9; id != -1; id--)
        {
            if (res_in_backpack[id] == 3)
            {
                id_3 = id;
                res_3 = true;
                break;
            }
            if (id == 0)
            {
                res_3 = false;
            }
        }
        //Заполнение пустого места в рюкзаке
        for (int id = 0; id < id_backpack; id++)
        {
            if(res_in_backpack[id] == 0)
            {
                res_in_backpack[id] = res_in_backpack[id + 1];
                res_in_backpack[id + 1] = 0;
                resources_on_backpack[id] = resources_on_backpack[id + 1];
                resources_on_backpack[id + 1] = null;
                resources_on_backpack[id].transform.localPosition = resources_on_backpack[id].transform.localPosition + new Vector3(0, -0.3f, 0);
            }
        }
    }
}
