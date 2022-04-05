using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EngineSimulation.Engines
{
    class RootEngine
    {
        // Motor moment of inertia
        public float I { get; set; }
        // Relationship between Rotation speed and Torque
        public SortedDictionary<float, float> VM = new SortedDictionary<float, float>();
        // Superheating temperature
        public float T_max { get; set; }
        // Coef of dependence of the heating rate on the torque
        public float H_m { get; set; }
        // Coef of dependence of the heating rate on the rotation speed
        public float H_v { get; set; }
        // Coef of dependence of the cooling rate on the engine and environment temp
        public float C { get; set; }

        public float CurrentTemp { get; set; }
        public float CurrentSpeed { get; set; }
        // Acceleration
        public float Accel { get; set; }
    }
}
