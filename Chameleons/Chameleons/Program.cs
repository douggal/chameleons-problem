using System;
using System.Collections.Generic;
using System.Text;

namespace Chameleons
{
    /// <summary>
    /// The Chameleons - a researcher puts three types of chameleons on an island: 
    /// 10 brown, 14 gray and 15 black.  When two chameleons of differnt colors meet, they
    /// both change their colors to the third one.  Will it be possible for all the
    /// chameleons to become the same color?
    /// 
    /// _Algorithmic Puzzles_, Anany Levitin, Maria Levitin, Oxford Univ Press, 2011.
    /// 
    /// Plan:
    ///     develop a brute force simimulation.
    ///     once it is working, if it proves possible to get to all same color, 
    ///     run it 10000 times and figure average number of meetings until all same color
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Chameleons simulation.");

            // a list of chameleons
            List<Chameleon> cc = new List<Chameleon>();

            // how many of each color do we need
            int nBlack = 15;
            int nBrown = 10;
            int nGray = 14;

            // how many simulations to run
            int nSimulations = 10000;
            
            // create an accumulator keep track of how many generations to all same color
            Accumulator runCounts = new Accumulator();

            // number of generations for which no solution was found
            int nNoSolution = 0;

            // a random number generator
            Random r = new Random();

            // keep track of which color is the final color
            Dictionary<Chameleon.colors, int> fnlColors = new Dictionary<Chameleon.colors, int>();
            fnlColors[Chameleon.colors.Black] = 0;
            fnlColors[Chameleon.colors.Brown] = 0;
            fnlColors[Chameleon.colors.Gray] = 0;

            int g;                      // count of chameleon meetings needed to reach all same color
            bool allSameColor;          // true if all same color after run
            Chameleon.colors fnlColor;  // final color if all same color reached

             // run the simulations in a loop
            for (int i = 0; i < nSimulations; i++)
            {
                g = 0;
                Initialize(cc, nBlack, nBrown, nGray);
                RunSimulation(cc, r, out g, out allSameColor, out fnlColor);
                if (allSameColor)
                {
                    runCounts.AddDataValue(g);
                    switch (fnlColor)
                    {
                        case Chameleon.colors.Black:
                            fnlColors[Chameleon.colors.Black] += 1;
                            break;
                        case Chameleon.colors.Brown:
                            fnlColors[Chameleon.colors.Brown] += 1;
                            break;
                        case Chameleon.colors.Gray:
                            fnlColors[Chameleon.colors.Gray] += 1;
                            break;
                        default:
                            break;
                    }

                }
                else
                {
                    nNoSolution++;
                }
                cc.Clear();
            }

            Console.WriteLine("Results of simulations");
            Console.WriteLine("Number of simulations: {0}", nSimulations);
            Console.WriteLine("Number of simulations with no solution: {0}", nNoSolution);
            Console.WriteLine("Average number of meetings to all chameleons are of the same color {0}\n", runCounts.Mean());
            Console.WriteLine("Final color Black: {0}", fnlColors[Chameleon.colors.Black]);
            Console.WriteLine("Final color Brown: {0}", fnlColors[Chameleon.colors.Brown]);
            Console.WriteLine("Final color Gray: {0}", fnlColors[Chameleon.colors.Gray]);

            Console.WriteLine("Press any key to continue.");
            Console.ReadKey();
        }

        /// <summary>
        /// Run a simulation
        /// </summary>
        /// <param name="cc">List of type Chameleon</param>
        /// <param name="r">A random variable</param>
        /// <param name="g">Return parameter - number of meetings until all same color</param>
        /// <param name="allSameColor">True if simulation resulted in all same color chameleon</param>
        /// <param name="fc">Final color if all same color reached - not valid if allSameColor false</param>
        private static void RunSimulation(List<Chameleon> cc, Random r, out int g, out bool allSameColor, out Chameleon.colors fc)
        {
            int maxLoop = 1000;         // max number meetings before we give up
            g = 0;                      // loop counter - number of meetings 
            allSameColor = false;       // true if all chameleons are same color
            int numCC = cc.Count;       // number of chameleons in the list

            // count up and store how many of each color at intervals
            // 12/23/2015 (dg) needed this for debugging and to watch it run a few times.
            Dictionary<Chameleon.colors, int> tally = new Dictionary<Chameleon.colors, int>();

            int c1;         // index of one randomly selected chameleon in the list
            int c2;         // index of aother randomly selected chameleon in the list

            while (g < maxLoop && !allSameColor)
            {
                g++;

                // pick two chamelons
                c1 = r.Next(numCC);
                do
                {
                    c2 = r.Next(numCC);
                } while (c2 == c1);

                // change color if necessary
                cc[c1].SetColor(cc[c2]);

                // are they all the same color yet?
                // and how many of each color do we have?
                allSameColor = CountAndCheck(cc, allSameColor, numCC, tally);

                //Console.WriteLine("Iteration {0}, Black: {1} Brown: {2}  Gray: {3}", cnt, tally[Chameleon.colors.Black], tally[Chameleon.colors.Brown], tally[Chameleon.colors.Gray]);

            }

            // return final color of chameleons, if they did reach a final color return value is not valid.
            if (allSameColor)
            {
                fc = cc[0].Color;
            }
            else
            {
                fc = Chameleon.colors.Black; // default value
            }
        }

        /// <summary>
        /// helper method, update count of chameleons by color, check if all same color.
        /// </summary>
        /// <param name="cc"></param>
        /// <param name="allSameColor"></param>
        /// <param name="numCC"></param>
        /// <param name="tally"></param>
        /// <returns></returns>
        private static bool CountAndCheck(List<Chameleon> cc, bool allSameColor, int numCC, Dictionary<Chameleon.colors, int> tally)
        {
            tally[Chameleon.colors.Black] = 0;
            tally[Chameleon.colors.Brown] = 0;
            tally[Chameleon.colors.Gray] = 0;
            for (int i = 0; i < numCC; i++)
            {
                //if (cc[i].Color != cc[0].Color)
                //{
                //    allSameColor = false;
                //    break;
                //}
                switch (cc[i].Color)
                {
                    case Chameleon.colors.Black:
                        tally[Chameleon.colors.Black] += 1;
                        break;
                    case Chameleon.colors.Brown:
                        tally[Chameleon.colors.Brown] += 1;
                        break;
                    case Chameleon.colors.Gray:
                        tally[Chameleon.colors.Gray] += 1;
                        break;
                    default:
                        break;
                }
            }
            if (tally[Chameleon.colors.Black] == tally[Chameleon.colors.Brown]
                    && tally[Chameleon.colors.Black] == tally[Chameleon.colors.Gray])
            {
                allSameColor = true;
            }

            return allSameColor;
        }

        /// <summary>
        /// Initialize the list of chameleons by creating requested number of each color of chameleon.
        /// </summary>
        /// <param name="cc"></param>
        /// <param name="nBlack"></param>
        /// <param name="nBrown"></param>
        /// <param name="nGray"></param>
        private static void Initialize(List<Chameleon> cc, int nBlack, int nBrown, int nGray)
        {
            // create chameleons in quantities specified
            for (int i = 1; i <= nBlack; i++)
            {
                cc.Add(new Chameleon(Chameleon.colors.Black));
            }
            for (int i = 1; i <= nBrown; i++)
            {
                cc.Add(new Chameleon(Chameleon.colors.Brown));
            }
            for (int i = 1; i <= nGray; i++)
            {
                cc.Add(new Chameleon(Chameleon.colors.Gray));
            }
        }
    }
}
