﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fhnw.Ecnf.RoutePlanner.RoutePlannerLib
{
    public class WayPoint
    {
        public string Name { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }

        public WayPoint() { }

        public WayPoint(string _name, double _latitude, double _longitude)
        {
            Name = _name;
            Latitude = _latitude;
            Longitude = _longitude;
        }

        public override string ToString()
        {
            string s = "WayPoint: ";
            if (Name != null && Name != "")
            {
                s += Name + " ";
            }
            s += string.Format("{0:N2}/{1:N2}", Latitude, Longitude);
            return s;
        }

        public double Distance(WayPoint target)
        {
            double earthRadius = 6371.0;
            double theta = this.Longitude - target.Longitude;
            double distance =
                earthRadius * Math.Acos(Math.Sin(deg2rad(this.Latitude))
                * Math.Sin(deg2rad(target.Latitude))
                + Math.Cos(deg2rad(this.Latitude))
                * Math.Cos(deg2rad(target.Latitude))
                * Math.Cos(deg2rad(theta)));
            return distance;
        }

        private double deg2rad(double deg)
        {
            return (deg * Math.PI / 180.0);
        }

        public static WayPoint operator +(WayPoint lhs, WayPoint rhs)
        {
            return new WayPoint(lhs.Name, lhs.Latitude + rhs.Latitude, lhs.Longitude + rhs.Longitude);
        }

        public static WayPoint operator -(WayPoint lhs, WayPoint rhs)
        {
            return new WayPoint(lhs.Name, lhs.Latitude - rhs.Latitude, lhs.Longitude - rhs.Longitude);
        }
    }
}
