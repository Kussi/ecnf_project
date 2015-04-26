using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Dynamic;

namespace Fhnw.Ecnf.RoutePlanner.RoutePlannerLib.Dynamic
{
    public class World : DynamicObject
    {
        private Cities _cities;
        public World(Cities cities)
        {
            _cities = cities;
        }

        public override bool TryInvokeMember(InvokeMemberBinder binder, object[] args, out object result)
        {
            String actualName = binder.Name;
            City cityExist = _cities.FindCity(actualName);

            if (cityExist != null)
            {
                result = cityExist;
            }
            else
            {
                result = String.Format("The city \"{0}\" does not exist!", binder.Name);
            }

            return true;
        }
    }
}

