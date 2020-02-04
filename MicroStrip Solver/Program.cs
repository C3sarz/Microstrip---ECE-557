/*  Microstrip Solver
 * EM Theory - ECE 557
 * Made by: Cesar Zavala
 */

using System;

namespace MicroStrip_Solver
{
    public class Program
    {
        static void Main(string[] args)
        {
            double s, width, height, relativeP, t, z0, effectiveP;
            
            Console.WriteLine("Microstrip Solver, no exception handling!");

            Console.WriteLine("Please input the width, height, and Relative Premittivity!");
            Console.Write("Width: ");
            width = Double.Parse(Console.ReadLine());
            Console.Write("Height: ");
            height = Double.Parse(Console.ReadLine());
            Console.Write("Relative Permittivity: ");
            relativeP = Double.Parse(Console.ReadLine());

            s = width / height;
            t = Math.Pow((30.67 / s), 0.75);
            effectiveP = findEffectiveP(relativeP, s);

            z0 = findImpedance(effectiveP, s, t);   // Line impedance calculation.

            Console.WriteLine("\n------- Values -------");
            Console.Write("Width: " + width);
            Console.Write("     Height: " + height);
            Console.WriteLine("     RelativeP: " + relativeP);
            Console.WriteLine("\nEffectiveP: " + effectiveP);
            Console.WriteLine("Z0 (Line Impedance): " + z0);
            Console.WriteLine("-----------------------");

        }

        /// <summary>
        /// Finds the line impedance.
        /// </summary>
        /// <param name="effectiveP">Effective Premittivity</param>
        /// <param name="s">Width/Height Ratio</param>
        /// <param name="t">T variable</param>
        /// <returns></returns>
        private static double findImpedance(double effectiveP, double s, double t)
        {
            return ((60 / Math.Sqrt(effectiveP)) * Math.Log((6 + (2 * Math.PI - 6) * Math.Exp(-t)) / s + Math.Sqrt(1 + 4 / (s * s))));
        }

        /// <summary>
        /// Finds the x variable for the Effective Premittivity
        /// </summary>
        /// <param name="relativeP">Relative Premittivity</param>
        /// <returns></returns>
        public static double findX(double relativeP)
        {
            return (0.56 * Math.Pow(((relativeP - 0.9) / (relativeP + 3)), 0.05));
        }

        /// <summary>
        /// Finds the y variable for the Effective Premittivity
        /// </summary>
        /// <param name="s">Width/Height Ratio</param>
        /// <returns></returns>
        public static double findY(double s)
        {
            return 1 + (0.02 * Math.Log(((s * s * s * s) + 3.7 * Math.Pow(10, -4) * (s * s)) / ((s * s * s * s) + 0.43)))
                + (0.05 * Math.Log(1 + 1.7 * Math.Pow(10, -4) * (s * s * s)));
        }

        /// <summary>
        /// Finds the Effective Premittivity.
        /// </summary>
        /// <param name="relativeP">Relative Premittivity</param>
        /// <param name="s">Width/Height Ratio</param>
        /// <returns></returns>
        public static double findEffectiveP(double relativeP, double s)
        {
            return ((relativeP + 1) / 2) + ((relativeP - 1) / 2) * Math.Pow((1 + 10 / s), -(findX(relativeP) * findY(s)));
        }

    }
}
