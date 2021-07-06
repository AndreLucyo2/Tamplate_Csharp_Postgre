using AppConsole.Cod;
using System;

namespace AppConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            //CRUD Postgres
            CrudPosTgres.MainCRUD();

            Console.WriteLine("Sistema Finalizado com sucesso!");
            Console.ReadKey();
        }
    }
}
