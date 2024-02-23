using System.Text;
using System.Linq;
using System.IO;
namespace Borda_Bertram_emelt
{
    internal class Program
    {
        static List<Tabor> taborok = new List<Tabor>();

        static void Main(string[] args)
        {
            //1.feladat
            var sr = new StreamReader(path: @"..\..\..\src\taborok.txt", encoding: Encoding.UTF8);
            while (!sr.EndOfStream)
            {
                taborok.Add(new Tabor(sr.ReadLine()));
            }
            sr.Close();

            //2.feladat
            Console.WriteLine($"2. feladat\nAz adatsorok száma:{taborok.Count}");
            Console.WriteLine($"Az először rögzített tábor táméja: {taborok[0].tema}");
            Console.WriteLine($"Az utoljára rögzített tábor táméja: {taborok[taborok.Count-1].tema}");

            //3.feladat
            Console.WriteLine("3. feladat");
            bool volt = false;
            foreach (var item in taborok)
            {
                if (item.tema == "zenei")
                {
                    volt = true;
                    Console.WriteLine($"Zenei tábor kezdődik {item.honap1}. hó {item.nap1}. napján.");
                }
            }
            if (!volt)
            {
                Console.WriteLine("„Nem volt zenei tábor.");
            }

            //4.feladat
            Console.WriteLine("4. feladat\nLegnépszerűbbek:");
            var tabor = taborok.MaxBy(r => r.diak.Length);
            foreach (var item in taborok)
            {
                if (item.diak.Length == tabor.diak.Length)
                {
                    Console.WriteLine($"{item.honap1} {item.nap1} {item.tema}");
                }
            }

            //6.feladat
            Console.WriteLine("6. feladat");
            Console.Write("hó: ");
            int ho = Convert.ToInt32(Console.ReadLine());
            Console.Write("nap: ");
            int nap = Convert.ToInt32(Console.ReadLine());
            int darab = 0;
            int keresett = sorszam(ho,nap);
            foreach (var item in taborok)
            {
                int kezdes = sorszam(item.nap1,item.honap1);
                int vege = sorszam(item.nap2, item.honap2);
                if (keresett >= kezdes && keresett <= vege )
                {
                    darab++;
                }
            }
            Console.WriteLine($"Ekkor éppen {darab} tábor tart.");


            //7.feladat
            Console.WriteLine("7. feladat");
            Console.Write("Adja meg egy tanuló betűjelét: ");
            string betu = Console.ReadLine().ToUpper();
            List<Tabor> jelentkezett = new List<Tabor>();
            foreach (var item in taborok)
            {
                if (item.diak.Contains(betu))
                {
                    jelentkezett.Add(item);
                }
            }

            for (int i = 0; i < jelentkezett.Count-1; i++)
            {
                for (int x = i+1; x < jelentkezett.Count; x++)
                {
                    if (sorszam(jelentkezett[i].honap1, jelentkezett[i].nap1) > sorszam(jelentkezett[x].honap1, jelentkezett[x].nap1))
                    {
                        (jelentkezett[i], jelentkezett[x]) = (jelentkezett[x], jelentkezett[i]);
                    }
                }
            }

            var sw = new StreamWriter("egytanulo.txt");
            foreach (var item in jelentkezett)
            {
                sw.WriteLine($"{item.honap1}.{item.nap1}-{item.honap2}.{item.nap2}. {item.tema}");
            }
            sw.Close();

            for (int i = 0; i < jelentkezett.Count-1; i++)
            {
                int ikezdes = sorszam(jelentkezett[i].honap1, jelentkezett[i].nap1);
                int ivege = sorszam(jelentkezett[i].honap2, jelentkezett[i].nap2);
                for (int x = i+1; x < jelentkezett.Count; x++)
                {
                    int xkezdes = sorszam(jelentkezett[x].honap1, jelentkezett[i].nap1);
                    int xvege = sorszam(jelentkezett[x].honap2, jelentkezett[i].nap2);
                    if ((ikezdes >= xkezdes && ikezdes <= xvege) || (ivege>= xkezdes && ivege<=xvege) || (xkezdes >= ikezdes && xkezdes <= ivege) || (xvege >= ikezdes && xvege <= ivege))
                    {
                        Console.WriteLine("Nem mehet el mindegyik táborba.");
                        return;
                    }
                }
            }
            Console.ReadKey();
        }

        //5. feladat
        static int sorszam(int honap, int nap)
        {
            int sorszam = 0;
            int honapkul = honap - 6;
            if (honapkul > 0)
            {
                for (int i = 0; i < honapkul; i++)
                {
                    if (i == 0)
                    {
                        sorszam += 30;
                    }
                    else
                    {
                        sorszam += 31;
                    }
                }
            }
            sorszam += nap;
            return sorszam-15;
        }
    }
}