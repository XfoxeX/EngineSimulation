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
        // Engine heating rate
        private float _vH;
        // Engine cooling rate
        private float _vC;
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
                // Print intermediate result
                printResult();

                // Fetch acceleration
                if (_engine.VM.TryGetValue(_engine.CurrentSpeed, out float value))
                {
                    // If relationship between Rotation speed and Torque have current speed
                    _engine.CurrentM = value;
                    _engine.Accel = value / _engine.I;
                }
                else
                {
                    // Fetch current Torque by interpolation method
                    if (_engine.VM.Last().Key > _engine.CurrentSpeed)
                    {
                        _engine.CurrentM = interpolation();
                        _engine.Accel = _engine.CurrentM / _engine.I;
                    }
                    else
                    {
                        throw new ArgumentException("Выход за рамки линейной зависимости V с M!");
                    }

                }

                //  Fetch next engine heating rate
                _vH = _engine.CurrentM * _engine.H_m + (float)Math.Pow(_engine.CurrentSpeed, 2) * _engine.H_v;
                // Fetch next engine cooling rate
                _vC = _engine.C * (EnvTemp - _engine.CurrentTemp);
                // Fetch next engine temp
                _engine.CurrentTemp += (_vC + _vH) * _currentTime;
                // Fetch next speed
                _engine.CurrentSpeed += _engine.Accel;

                _currentTime++;
            }

            Console.WriteLine("\n\n\n Времени до перегрева: " + _currentTime);
        }

        // Interpolation method
        public float interpolation()
        {

            // Interpolation result
            float currentM;

            // Fetch two elements near the current speed
            int max = binSearch();

            // Fetch interpolation elements
            float maxV = _engine.VM.ElementAt(max).Key;
            float maxM = _engine.VM[maxV];
            float minV = _engine.VM.ElementAt(max-1).Key;
            float minM = _engine.VM[minV];

            // Fetch current M
            currentM = minM + ( (_engine.CurrentSpeed - minV) / (maxV - minV) * (maxM - minM) );
            return currentM;
        }

        // Fetch two elements near the current speed method
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

        public void printResult()
        {
            Console.WriteLine("Ответ: ");
            Console.Write("Time: " + _currentTime + " V: " + _engine.CurrentSpeed + " temp: " + _engine.CurrentTemp + "\n");
        }

    }
}
