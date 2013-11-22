using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication2
{
    class Program
    {        
        static int[,] wczytaj_plik()
        {
            string[] wiersze = System.IO.File.ReadAllLines(@"C:\Users\Michal\Documents\GitHub\optymalizacja\ConsoleApplication2\ConsoleApplication2\DistanceMatrix.txt");

            int liczba_miast = int.Parse(wiersze[0]);
            int[,] macierz = new int[9, 9];
            int i, j;

            for (i = 0; i < 9; i++)
            {
                string[] wartosci = wiersze[i + 1].Split();
                for (j = 0; j < 9; j++)
                {
                    macierz[i, j] = int.Parse(wartosci[j]);
                }
            }
            return macierz;
        }

        static void wyswietl_plik(int[,] macierz)
        {
            int i, j;

            for (i = 0; i < 9; i++)
            {

                for (j = 0; j < 9; j++)
                {
                    System.Console.Write(macierz[i, j] + " ");
                }
                System.Console.Write("\n");
            }
        }

        static int oblicz_trase(int[] obliczana_trasa, int[,] macierz)
        {
            int dlugosc_trasy = 0;
            int j;

            for (j = 0; j < 9; j++)
            {
                dlugosc_trasy += macierz[obliczana_trasa[j],obliczana_trasa[j + 1]];              
            }
            return dlugosc_trasy;
        }

        static int[] oblicz_dwie_trasy(int[] obliczana_trasa, int[,] macierz)
        {
            int[] dlugosc_trasy = new int[2] {0,0};
            int j = 0;
    
                while (obliczana_trasa[j + 1] != 0)
                {
                    dlugosc_trasy[0] += macierz[obliczana_trasa[j], obliczana_trasa[j + 1]];
                    j++;
                }
                dlugosc_trasy[0] += macierz[obliczana_trasa[j], obliczana_trasa[j + 1]];
                j++;
                while (obliczana_trasa[j + 1] != 0)
                {
                    dlugosc_trasy[1] += macierz[obliczana_trasa[j], obliczana_trasa[j + 1]];
                    j++;
                }
                dlugosc_trasy[1] += macierz[obliczana_trasa[j], obliczana_trasa[j + 1]];

            return dlugosc_trasy;
        }

        static void optymalizacja(int[] trasa, int[,] macierz)
        {
            int a, b, c, d, i;
            int nowa_dlugosc = 0, najmniejsza_dlugosc = 999999;
            int[] najkrotsza_trasa = new int[10];
            Random rnd = new Random();

            for (i = 0; i < 1000; i++)
            {               
                a = rnd.Next(1, 8);
                b = rnd.Next(1, 8);

                c = trasa[a];
                d = trasa[b];
                trasa[a] = d;
                trasa[b] = c;

                nowa_dlugosc = oblicz_trase(trasa, macierz);

                if (najmniejsza_dlugosc > nowa_dlugosc)
                {
                    najmniejsza_dlugosc = nowa_dlugosc;
                    foreach (int k in trasa) najkrotsza_trasa[k] = trasa[k];              
                }
            }
            System.Console.Write("\nDługość trasy zoptymalizowanej ");
            for (i = 0; i < 9; i++) System.Console.Write(najkrotsza_trasa[i] + "->");
            System.Console.Write(najkrotsza_trasa[9] + " " + najmniejsza_dlugosc + "km");
        }

        static int algorytm_SA(int[] trasa, int[,] macierz)
        {
            int a, b, c, d;
            int nowa_dlugosc = 0, najmniejsza_dlugosc = 999999;
            int[] najkrotsza_trasa = new int[10];
            double p, q, delta, lambda = 0.995, T = 4000;
            Random rnd = new Random();

            while (T > 0.5)
            {
                a = rnd.Next(1, 8);
                b = rnd.Next(1, 8);
                q = rnd.NextDouble();

                c = trasa[a];
                d = trasa[b];
                trasa[a] = d;
                trasa[b] = c;

                nowa_dlugosc = oblicz_trase(trasa, macierz);

                if (najmniejsza_dlugosc >= nowa_dlugosc)
                {
                    najmniejsza_dlugosc = nowa_dlugosc;
                    foreach (int k in trasa) najkrotsza_trasa[k] = trasa[k];
                }
                else
                {
                    delta = nowa_dlugosc - najmniejsza_dlugosc;
                    p = Math.Exp(-delta / T);
                    if (p > q)
                    {
                        trasa[a] = c;
                        trasa[b] = d;
                    }
                }
                T *= lambda;
            }

            // System.Console.Write("\nDługość trasy zoptymalizowanej ");
            // for (int i = 0; i < 9; i++) System.Console.Write(najkrotsza_trasa[i] + "->");
            // System.Console.Write(najkrotsza_trasa[9] + " " + najmniejsza_dlugosc + "km");
            return najmniejsza_dlugosc;
        }

        static int[] SA_dwietrasy(int[] trasa, int[,] macierz)
        {
            int a, b, c, d, i;
            int nowa_dlugosc = 0, najmniejsza_dlugosc = 999999;
            int[] najkrotsza_trasa = new int[11];
            int[] najmniejsze_dlugosci = new int[2];
            int[] nowe_dlugosci = new int[2];
            double p, q, delta, lambda = 0.995, T = 4000;
            Random rnd = new Random();

                while (T > 0.5)
                {
                    a = rnd.Next(1, 9);
                    b = rnd.Next(1, 9);
                    q = rnd.NextDouble();

                    c = trasa[a];
                    d = trasa[b];
                    trasa[a] = d;
                    trasa[b] = c;

                    nowe_dlugosci = oblicz_dwie_trasy(trasa, macierz);
                    nowa_dlugosc = nowe_dlugosci.Max();

                    if (najmniejsza_dlugosc >= nowa_dlugosc)
                    {
                        najmniejsza_dlugosc = nowa_dlugosc;                       
                        najkrotsza_trasa = trasa;
                        najmniejsze_dlugosci = nowe_dlugosci;                     
                    }
                    else
                    {
                        delta = nowa_dlugosc - najmniejsza_dlugosc;
                        p = Math.Exp(-delta / T);
                        if (p > q)
                        {
                            trasa[a] = c;
                            trasa[b] = d;
                        }
                    }
                    T *= lambda; 
                }

                
                //System.Console.Write("\nDługości tras zoptymalizowanych ");
                //for (int j = 0; j < 10; j++) System.Console.Write(najkrotsza_trasa[j] + "->");
                //System.Console.Write(najkrotsza_trasa[10] + " " + nowe_dlugosci[0] + "km  " + nowe_dlugosci[1] + "km  ");
                return najmniejsze_dlugosci;           
        }

        static void Main(string[] args)
        {
            int[,] macierz = new int[9, 9];
            int[] trasa = new int[10] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 0 };
            int[] trasa2 = new int[10] { 0, 1, 2, 4, 6, 8, 0, 0, 0, 0 };
            int dlugosc_poczatkowa = 0, nowa_dlugosc, najmniejsza=9999999;

            int[] dwietrasy = new int[11] { 0, 1, 2, 3, 4, 0, 5, 6, 7, 8, 0 };
            int[] dlugosci_poczatkowe = new int[2] { 0, 0 };
            int[] nowe_dlugosci = new int[2];
            int[] najmniejsze_dlugosci = new int[2];

            macierz = wczytaj_plik();
            wyswietl_plik(macierz);
            dlugosc_poczatkowa = oblicz_trase(trasa, macierz);
            dlugosci_poczatkowe = oblicz_dwie_trasy(dwietrasy, macierz);

            for (int i = 0; i < 1000; i++)
            {
                 //nowa_dlugosc = algorytm_SA(trasa, macierz);
                 //if (najmniejsza > nowa_dlugosc) najmniejsza = nowa_dlugosc;

                nowe_dlugosci = SA_dwietrasy(dwietrasy, macierz);
                if (najmniejsza > nowe_dlugosci.Max())
                {
                    najmniejsza = nowe_dlugosci.Max();
                    najmniejsze_dlugosci = nowe_dlugosci;
                }
            } 
          
            System.Console.Write("\nDługości tras  " + najmniejsze_dlugosci[0] + "   " + najmniejsze_dlugosci[1]);
            
            //SA_dwietrasy(dwietrasy, macierz);       
            //System.Console.Write("\nDługości tras  " + oblicz_trase(trasa2, macierz));

            System.Console.Read();        
        }
    }
}
