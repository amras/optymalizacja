﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication2
{
    class Program
    {
        static void Main(string[] args)
        {
            int i, j, a, b, liczba_miast, dlugosc_trasy = 0, dlugosc_trasy2 = 0;
            int[,] tab = new int[9, 9];
            int[] trasa = new int[10] {0,1,2,3,4,5,6,7,8,0};
            int[] trasa2 = new int[10] {5,7,6,8,0,2,4,1,3,5};

            string[] wiersze = System.IO.File.ReadAllLines(@"C:\Users\Michal\Documents\Visual Studio 2013\Projects\ConsoleApplication2\ConsoleApplication2\DistanceMatrix.txt");
            
            liczba_miast = int.Parse(wiersze[0]);

 
            for (i = 0; i < 9; i++)
            {
                string[] wartosci = wiersze[i+1].Split();
                for (j = 0; j < 9; j++)
                {
                    tab[i, j] = int.Parse(wartosci[j]);
                }
            }

            System.Console.WriteLine("Liczba miast: " + liczba_miast + "\n");    

            for (i = 0; i < 9; i++)
            {

                for (j = 0; j < 9; j++)
                {
                    System.Console.Write(tab[i,j] + " ");          
                }
                System.Console.Write("\n");
            }

            for (j = 0; j < 9; j++)
            {
                a = trasa[j];
                b = trasa[j + 1];
                dlugosc_trasy += tab[a,b];
                //printf("%i ", tab[a][b]);
                a = trasa2[j];
                b = trasa2[j + 1];
                dlugosc_trasy2 += tab[a, b];
            }

            System.Console.WriteLine("\nDługość trasy 1: " + dlugosc_trasy + "km");
            System.Console.WriteLine("Długość trasy 2: " + dlugosc_trasy2 + "km");
            System.Console.Read();
        }
    }
}