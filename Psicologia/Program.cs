using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Psicologia
{

    //AnalisiAttributi();

            //conteggioAttributi();

            //Analisi(assembly);

            //reflection();

    delegate void Riflessioni(string problema);
    delegate void Operazioni(int number);
    delegate void Ripresa(double rip);


    class Program
    {
        static void Main(string[] args)
        {
            Riflessioni rif = ProblemiCasa;
            Riflessioni med = meditazione();
            RiflessioniCasa(rif);
            Operazioni op = StatiDepressione;
            Ripresa rip = FintaRipresa;
            
            rip = rip + FintaRipresa;
            EsecuzioneFintaRipresa(3, rip);
            

            EsecuzioneRipresa(2, op);
           
            //StatiDepressione(3);
            //ProblemiCasa(rif);
        }

        static void StatiDepressione(int num)
        {
            Console.WriteLine(" se stai {0} ore a letto, ti sentirai {1} volte depresso", num, num * 2);
        }

        static void StatiRipresa(int num)
        {
            Console.WriteLine(" se stai {0} ore con pensieri positivi, ti sentirai {1} volte meglio", num, num * 4);
        }

        static void FintaRipresa(double fp)
        {
            Console.WriteLine(" se stai {0} ore pensando di stare meglio, ti sentirai {1} volte più insicuro", fp, fp / 2);
        }

        static void EsecuzioneFintaRipresa(double num, Ripresa ripresa)
        {
            ripresa(num);
        }

        static void EsecuzioneRipresa(int num, Operazioni operazioni)
        {
            operazioni(num);
        }

        static Riflessioni meditazione()
        {
            return new Riflessioni(ProblemiCasa);
        }


        static void ProblemiCasa(string tipoProblema)
        {
            Console.WriteLine("ci sono problemi a casa, come {0}?", tipoProblema);
        }

        static void RiflessioniCasa(Riflessioni rif)
        {
            rif("problemi di comunicazione");
        }

       

        private static void conteggioAttributi()
        {
            var assembly = Assembly.GetExecutingAssembly();
            var tipi = assembly.GetTypes().Where(t => t.GetCustomAttributes<UsciteFuori>().Count() > 0);
            foreach (var tipo in tipi)
            {
                Console.WriteLine(tipo.Name);

                var metodi = tipo.GetMethods().Where(m => m.GetCustomAttributes<Meteropatia>().Count() > 0);
                foreach (var metodo in metodi)
                {
                    Console.WriteLine(metodo.Name);
                }


            }
        }

    

        private static void reflection()
        {
            var dopamina = new Dopamina { Bisogno = "Alto", Quantità = 20 };
            var dopaminaTipo = typeof(Dopamina);

            var bisognoProprDopamina = dopaminaTipo.GetProperty("Bisogno");
            Console.WriteLine("Property: " + bisognoProprDopamina.GetValue(dopamina));

            var disturboSocial = dopaminaTipo.GetMethod("disturboSocial");
            disturboSocial.Invoke(dopamina, null);
        }

       

    public class Dopamina
    {
        public string Bisogno { get; set; }
        public int Quantità;

        [Meteropatia]
        public void disturboSocial()
        {
            Console.WriteLine("disturboSocial è abbastanza alto, 5 collegamenti Facebook in un'ora!");
        }
    }

    [Riposo]
    public class Rimorsi
    {
        [Riposo]
        public int numeroRimorsi { get; set; }

        [Riposo]
        public void dormitePomeridiane() { }

        [Meteropatia]
        public void dormitePerMeteropatia() { }

    }

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Property | AttributeTargets.Method)]
    public class Riposo : Attribute
    {

    }

    [AttributeUsage(AttributeTargets.Method)]
    public class Meteropatia : Attribute
    {

    } 


    [AttributeUsage(AttributeTargets.Class)]
    public class UsciteFuori : Attribute
    {
        public string ColoreGiornata { get; set; }
        public int VogliaDiPalestra { get; set; }
    }

    [UsciteFuori(ColoreGiornata="Blu", VogliaDiPalestra=7)]
    public class percezioneCorpo
    {
        public int Pesantezza { get; set; }

        [Meteropatia]
        public void Percezione() { }
    }


    private static void AnalisiAttributi()
    {
        var sensazioni = from t in Assembly.GetExecutingAssembly().GetTypes()
                         where t.GetCustomAttributes<UsciteFuori>().Count() > 0
                         select t;

        foreach (var t in sensazioni)
        {
            Console.WriteLine(t.Name);

            foreach (var p in t.GetProperties())
            {
                Console.WriteLine(p.Name);
            }
        }
    }

    private static void Analisi(Assembly assembly)
    {
        Console.WriteLine(assembly.FullName);

        var tipi = assembly.GetTypes();
        foreach (var tipo in tipi)
        {
            Console.WriteLine("Tipo: " + tipo.Name + " TipoBase " + tipo.BaseType);

            var props = tipo.GetProperties();
            foreach (var prop in props)
            {
                Console.WriteLine("\tProperty: " + prop.Name + " tipo Property " + prop.PropertyType);
            }

            var campi = tipo.GetFields();
            foreach (var campo in campi)
            {
                Console.WriteLine("\tCampo: " + campo.Name + " tipo Campo " + campo.FieldType);
            }

            var metodi = tipo.GetMethods();
            foreach (var metodo in metodi)
            {
                Console.WriteLine("\tMetodo: " + metodo.Name);
            }
        }
    }
    }
}
