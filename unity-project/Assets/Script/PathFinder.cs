using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PathFinder
{
	private PathFinder() {}
	
	class Node
	{
		float _X;
		float _Y;
		Vector2 _oce;
		double _cena;
		public Node() { }
		public Node(Vector2 trenutni, Vector2 oce)
		{
			this._X = trenutni.x;
			this._Y = trenutni.y;
			this._oce = oce;
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
	
	public static List<Vector2> findPath(Vector2 start, Vector2 goal, int[,] map)
	{
		List<Vector2> path = new List<Vector2>();
		
		List<Node> odprti = new List<Node>();
		List<Node> zaprti = new List<Node>();
		Node trenutni = new Node();
		
		bool našel = false; //če ne najde, tudi poti ne sme naredit
		
		odprti.Insert(0, new Node(start, start));
		
		//*************ZAČETEK ZANKE***********
		while (odprti.Count > 0)
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
					
					trenutni = new Node(new Vector2(zaprti[0].X + x, zaprti[0].Y + y), zaprti[0].trenutni);
					if (trenutni.trenutni == goal)
					{
						našel = true;
						goto Skonstruiraj;
					}
					
					bool preskoči = false;
					if(map[(int)trenutni.trenutni.x, (int)trenutni.trenutni.y] > 0)
						preskoči = true;
					for(int i = 0; i< zaprti.Count; i++)
					{
						if(zaprti[i].trenutni == trenutni.trenutni){
							preskoči = true;
							break;
						}
					}
					if (!preskoči)
					{
						if (odprti.Count == 0)
						{
							odprti.Insert(0, trenutni);
						}
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

}