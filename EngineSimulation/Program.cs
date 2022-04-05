using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EngineSimulation.Engines;

namespace EngineSimulation
{
    class Program
    {
        static void Main(string[] args)
        {
            ///////////////////////////////////////////
            /// Create first engine

            FirstEngine firstEngine = new FirstEngine();

            ///////////////////////////////////////////
            /// Set data from config

            // Motor moment of inertia
            firstEngine.I = float.Parse(ConfigurationManager.AppSettings.Get("I"));
            // Superheating temperature
            firstEngine.T_max = float.Parse(ConfigurationManager.AppSettings.Get("T_max"));
            // Coef of dependence of the heating rate on the torque
            firstEngine.H_m = float.Parse(ConfigurationManager.AppSettings.Get("H_m"));
            // Coef of dependence of the heating rate on the rotation speed
            firstEngine.H_v = float.Parse(ConfigurationManager.AppSettings.Get("H_v"));
            // Coef of dependence of the cooling rate on the engine and environment temp
            firstEngine.C = float.Parse(ConfigurationManager.AppSettings.Get("C"));

            // Create and set relationship between V and M and

            // Rotation speed
            string[] V = ConfigurationManager.AppSettings.Get("V").Split(',');
            // Torque
            string[] M = ConfigurationManager.AppSettings.Get("M").Split(',');
            
            if (V.Length == M.Length)
            {
                for (var i = 0; i < V.Length; i++)
                {
                    // Set data
                    firstEngine.VM.Add(float.Parse(V[i]), float.Parse(M[i]));
                }
            }

            ///////////////////////////////////////////
            /// Create first engine test



            // Берем данные из конфигурации, обрабатываем, запускаем тест

            // Main берет весь инпут и пускает в работу во все классы
        }
    }
}
