using UnityEngine;
using System.Collections;

public class SpawnHrošč : MonoBehaviour
{

    private static GameObject _hrosc;
    public GameObject hrosc;
    private static GameObject _zlati_hrosc;
    public GameObject zlati_hrosc;
    static GameObject[] hrosciList;
    static int hroscListCount = 0;

    private static void ustvariHrošč()
    {
        bool inside;
        float x;
        float z;
        do
        {
            inside = false;
            x = Random.Range(15, -14);
            z = Random.Range(15, -14);
            if (PathFinder.createMatrix()[(int)x + 16, (int)z + 16] > 0) //preveri, če je znotraj kakšne ovire
                inside = true;
            for (int j = 0; j < hroscListCount; j++) //preveri, če se prekriva s kakšnim obstoječim
            {
                if ((x == hrosciList[j].transform.position.x) && (z == hrosciList[j].transform.position.z)) //znotraj hrosca
                {
                    inside = true;
                    break;
                }
            }
        } while (inside);
        InstantiateHrošč(x + 0.5f, z + 0.5f, _hrosc);

    }

    private static void InstantiateHrošč(float x, float z, GameObject prefab)
    {
        InstantiateHrošč(hroscListCount, x, z, prefab);
        hroscListCount++;
    }

    private static void InstantiateHrošč(int index, float x, float z, GameObject prefab)
    {
        if (index > hroscListCount)
            return;
        hrosciList[index] = (GameObject)Instantiate(prefab, new Vector3(x, 0.6f, z), new Quaternion());
    }

    private static void respawnHrošč(GameObject original, GameObject prefab)
    {
        for (int i = 0; i < hroscListCount; i++)
        {
            if (hrosciList[i] == original)
            {
                InstantiateHrošč(i, original.transform.position.x, original.transform.position.z, prefab);
                Destroy(original); //odstrani starega navadnega hrošča
            }
        }
    }

    public static void upgradeHrošč(GameObject original)
    {
        respawnHrošč(original, _zlati_hrosc);
    }

    public static void respawnZlatiHrošč(GameObject original)
    {
        respawnHrošč(original, _hrosc);
    }

    void Awake()
    {
        _hrosc = hrosc;
        _zlati_hrosc = zlati_hrosc;
        hrosciList = new GameObject[10];
        hroscListCount = 0;
        //pozicioniraj hrosce
        for (int i = 0; i < 10; i++)
        {
            ustvariHrošč();
        }
    }




}
