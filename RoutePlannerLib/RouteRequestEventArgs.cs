using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fhnw.Ecnf.RoutePlanner.RoutePlannerLib
{
    public class RouteRequestEventArgs : System.EventArgs
    {
        public string FromCity { get; private set; }
        public string ToCity { get; private set; }
        public TransportModes Transportmodus;

        public RouteRequestEventArgs(string _fromCity, string _toCity, TransportModes _Transportmodus)
        {
            FromCity = _fromCity;
            ToCity = _toCity;
            Transportmodus = _Transportmodus;
        }
    }
}
