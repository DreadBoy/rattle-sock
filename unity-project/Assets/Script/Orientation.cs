using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Random = System.Random;

namespace Assets.Script
{
    public class Orientation
    {
		public enum direction{
			UP,
			RIGHT,
			DOWN,
			LEFT
		};
		int orient;
		public Orientation(){
			orient = 0;
		}
		public direction Direction{
			get{ return (direction)orient; }
			set{ orient = (int)value; }
		}
		public Quaternion getQuaternion(){
			return Quaternion.Euler(0, orient * 90, 0);
		}
        public void rotateLeft()
        {
			orient--;
			orient %= 4;
        }
        public void rotateRight()
        {
			orient++;
			orient %= 4;
        }
        public void random()
        {
            Random rand = new Random();
            int Y = rand.Next(0, 3);
			int count = 0;
			while (Y == orient || Y != (orient + 2)%4 || count < 10)
            {
                Y = rand.Next(0, 3);
				count++;
            }
			if(count < 10)
				orient = Y;
        }
    }
}
