/*  MapleLib - A general-purpose MapleStory library
 * Copyright (C) 2009, 2010 Snow and haha01haha01
   
 * This program is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

 * This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

 * You should have received a copy of the GNU General Public License
    along with this program.  If not, see <http://www.gnu.org/licenses/>.*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MapleLib.WzLib.WzStructure
{
    public struct Foothold
    {
        public int x1, x2, y1, y2;
        public int prev, next;
        public int num, layer;

        public Foothold(int x1, int x2, int y1, int y2, int num, int layer)
        {
            this.x1 = x1;
            this.x2 = x2;
            this.y1 = y1;
            this.y2 = y2;
            next = 0;
            prev = 0;
            this.num = num;
            this.layer = layer;
        }

        /*public static Foothold findBelow(Point p, List<FootholdLines>)
        {
            // find fhs with matching x coordinates
            List<Foothold> xMatches = new List<Foothold>();
            Foothold nearestFh = null;
            int nearestDistance = 0;
            foreach (Foothold fh in EnvironmentSettings.footholds)
            {
                if (fh.getX1() <= p.X && fh.getX2() >= p.X)
                    xMatches.Add(fh);
            }
            //xMatches.Sort();
            foreach (Foothold fh in xMatches)
            {
                if (!fh.IsWall() && fh.y1 != fh.y2)
                {
                    int calcY;
                    double s1 = Math.Abs(fh.y2 - fh.y1);
                    double s2 = Math.Abs(fh.x2 - fh.x1);
                    double s4 = Math.Abs(p.X - fh.x1);
                    double alpha = Math.Atan(s2 / s1);
                    double beta = Math.Atan(s1 / s2);
                    double s5 = Math.Cos(alpha) * (s4 / Math.Cos(beta));
                    if (fh.y2 < fh.y1)
                        calcY = fh.y1 - (int)s5;
                    else
                        calcY = fh.y1 + (int)s5;
                    if (calcY >= p.Y && (nearestFh == null || calcY - p.Y < nearestDistance))
                    {
                        nearestFh = fh;
                        nearestDistance = calcY - p.Y;
                    }
                }
                else if (!fh.isWall())
                {
                    if (fh.getY1() >= p.Y && (nearestFh == null || fh.getY1() - p.Y < nearestDistance))
                    {
                        nearestFh = fh;
                        nearestDistance = fh.getY1() - p.Y;
                    }
                }
            }
            return nearestFh;
        }*/

        public bool IsWall()
        {
            return x1 == x2;
        }
    }
}
