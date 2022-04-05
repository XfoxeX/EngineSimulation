using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EngineSimulation.Engines;

namespace EngineSimulation.EngineTests
{
    class FirstEngineTest
    {
        private RootEngine _engine;
        private int _currentTime;
        // Environment temperature
        public float EnvTemp { get; set; }

        public FirstEngineTest(RootEngine engine, float envTemp)
        {
            _engine = engine;
            EnvTemp = envTemp;
            _engine.CurrentTemp = envTemp;
        }

        public void Execute()
        {
            while (_engine.CurrentTemp < _engine.T_max)
            {
                if (_engine.VM.TryGetValue(_engine.CurrentSpeed,  out float value))
                {
                    // If relationship between Rotation speed and Torque have current speed
                    _engine.Accel = value / _engine.I;

                    
                }
                else
                {
                    // Fetch current Torque by interpolation method
                    if (_engine.VM.Last().Key > _engine.CurrentSpeed)
                    {
                        interpolation();
                    }

                }




            }
        }

        public float interpolation()
        {
            //foreach(var item in _engine.VM.Keys)
            //{

            //    Console.WriteLine(item);
            //}
            _engine.CurrentSpeed = 1;
            Console.WriteLine(_engine.VM.ElementAt(binSearch()).Key);
            Console.WriteLine(_engine.VM.ElementAt(binSearch()-1).Key);
            return 0;
        }

        // Fetch two elements near the current speed
        public int binSearch()
        {
            int low = 0;
            int high = _engine.VM.Count - 1;
            int guess = 1;

            while (_engine.VM.ElementAt(guess).Key < _engine.CurrentSpeed || _engine.VM.ElementAt(guess - 1).Key >= _engine.CurrentSpeed)
            {
                guess = (low + high) / 2;

                if (_engine.VM.ElementAt(guess).Key < _engine.CurrentSpeed)
                {
                    low = guess + 1;
                }else if (_engine.VM.ElementAt(guess - 1).Key >= _engine.CurrentSpeed)
                {
                    high = guess - 1;
                }
            }

            return guess;
        }

    }
}
