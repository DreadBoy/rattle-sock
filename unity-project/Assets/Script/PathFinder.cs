using System;
using System.Collections.Generic;
using UnityEngine;

public class PathFinder : MonoBehaviour
{
    private PathFinder() { }
    public static int[,] map = new int[33, 33];

    class Node
    {
        float _X;
        float _Y;
        Vector2 _oce;
        double _cena;
        public Node() { }
        public Node(Vector2 trenutni, Vector2 oce, double cena)
        {
            this._X = trenutni.x;
            this._Y = trenutni.y;
            this._oce = oce;
            this._cena = cena;
        }
        public Vector2 oce
        {
            get { return _oce; }
            set { _oce = value; }
        }
        public double cena
        {
            get { return _cena; }
            set { _cena = value; }
        }
        public float X
        {
            get { return _X; }
            set { _X = value; }
        }
        public float Y
        {
            get { return _Y; }
            set { _Y = value; }
        }
        public Vector2 trenutni
        {
            get { return new Vector2(_X, _Y); }
            set { _X = value.x; _Y = value.y; }
        }
    }

    public static int[,] createMatrix()
    {
        //dodaj ovire
        for (int x = 0; x < 32; x++)
        {
            for (int z = 0; z < 32; z++)
            {
                Collider[] colliders = Physics.OverlapSphere(new Vector3(x-15.5f, 1, z-15.5f), 0);
                if (colliders.Length > 0)
                    map[x, z] = 1;
            }
        }

        //vizualno prikaži matriko
        /*for (int x = 0; x < 32; x++)
            for (int y = 0; y < 32; y++)
                if(map[x, y] <= 0)
                    Instantiate(AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Amatrix.prefab", typeof(Transform)), new Vector3(x - 15.5f, 0.5f, y - 15.5f), Quaternion.identity);
        */
        return map;
    }

    public static List<Vector2> findPath(Vector2 start, Vector2 goal)
    {
        if (start.x < 0 || start.x > 32 || start.y < 0 || start.y > 32)
            return new List<Vector2>();
        List<Vector2> path = new List<Vector2>();

        List<Node> odprti = new List<Node>();
        List<Node> zaprti = new List<Node>();
        Node trenutni = new Node();

        bool force_break = false;

        bool našel = false; //če ne najde, tudi poti ne sme naredit

        odprti.Insert(0, new Node(start, start, dobiCeno(start, goal)));

        //*************ZAČETEK ZANKE***********
        while (odprti.Count > 0 && !force_break)
        {
            //poberemo naslednjega iz odprtih
            zaprti.Insert(0, odprti[0]);
            odprti.RemoveAt(0);
            //preverim vse sosede
            int x = 0;
            int y = 0;
            for (x = -1; x <= 1; x++)
            {
                int max_y = 1;
                if (x != 0)
                    max_y = 0;
                for (int y2 = -1; y2 <= max_y; y2 += 2)
                {
                    y = y2;
                    if (x != 0)
                        y = 0;
                    //tukaj dejansko začnem preverjat sosede

                    //če sem na cilju
                    trenutni = new Node(new Vector2(zaprti[0].X + x, zaprti[0].Y + y), zaprti[0].trenutni, dobiCeno(new Vector2(zaprti[0].X + x, zaprti[0].Y + y), goal));
                    if (trenutni.trenutni == goal)
                    {
                        našel = true;
                        goto Skonstruiraj;
                    }

                    bool preskoči = false;
                    //če je izven mape, potem preskoči
                    if ((int)trenutni.trenutni.x < 0 || (int)trenutni.trenutni.x > map.GetLength(0))
                        preskoči = true;
                    if ((int)trenutni.trenutni.y < 0 || (int)trenutni.trenutni.y > map.GetLength(0))
                        preskoči = true;
                    //če je trenutni ovira, potem preskoči
                    if (map[(int)trenutni.trenutni.x, (int)trenutni.trenutni.y] > 0)
                        preskoči = true;
                    //če sem ga že pregledal, potem preskoči
                    for (int i = 0; i < zaprti.Count; i++)
                    {
                        if (zaprti[i].trenutni == trenutni.trenutni)
                        {
                            preskoči = true;
                            break;
                        }
                    }
                    //če ga ne preskočim
                    if (!preskoči)
                    {
                        //ga dodam za pregled
                        //če je edini za pregledat, da samo dodaj
                        if (odprti.Count == 0)
                        {
                            odprti.Insert(0, trenutni);
                        }
                        //drugače preglej obstoječe uteži in ga dodaj na pravo mesto
                        else if (odprti.Count > 0)
                        {
                            int mesto = odprti.Count;
                            for (int stevec = 0; stevec < odprti.Count; stevec++)
                            {
                                if (trenutni.cena < odprti[stevec].cena)
                                {
                                    mesto = stevec;
                                    break;
                                }
                            }
                            odprti.Insert(mesto, trenutni);
                        }
                    }
                }
            }
        }
    //skonstruiraj pot
    Skonstruiraj:
        while (trenutni.trenutni != start && našel)
        {
            path.Insert(0, trenutni.trenutni);
            for (int i = 0; i < zaprti.Count; i++)
            {
                if (trenutni.oce == zaprti[i].trenutni)
                {
                    trenutni = zaprti[i];
                    break;
                }
            }
        }
        return path;
    }
    public static double dobiCeno(Vector2 point1, Vector2 point2)
    {
        return Math.Sqrt(Math.Pow((point1.x - point2.x), 2.0) + Math.Pow((point1.y - point2.y), 2.0));
    }

}