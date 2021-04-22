using System;
using System.Collections.Generic;
using System.Linq;
using PlanM.Modelo;


namespace PlanM
{
    class Program
    {

        static List<Empleados> Empleados = new List<Empleados>();
        static Validaciones Validar = new Validaciones();
         
        static void Main(string[] args)
        {
            int menu;
            string aux;
            bool entradaValida = false;
            
            do
            {
                Console.SetCursorPosition(45, 8); Console.WriteLine("Bienvenidos....");

                Console.SetCursorPosition(45, 10); Console.WriteLine("1.) Agregar Empleados");
                Console.SetCursorPosition(45, 11); Console.WriteLine("2.) Buscar Empleados");
                Console.SetCursorPosition(45, 12); Console.WriteLine("3.) Listar Empleados");

                Console.SetCursorPosition(45, 14); Console.WriteLine("0.) Salir... ");

                do
                {
                    Console.SetCursorPosition(47, 18); Console.Write("╔═");
                    Console.SetCursorPosition(52, 18); Console.Write("═╗");
                    Console.SetCursorPosition(47, 20); Console.Write("╚═");
                    Console.SetCursorPosition(52, 20); Console.Write("═╝");
                    Console.SetCursorPosition(40, 16); Console.WriteLine("Escoja una opcion");
                    Console.SetCursorPosition(50, 19); aux = Console.ReadLine();
                    if (!Validar.Vacio(aux))
                        if (Validar.TipoNumero(aux))
                            entradaValida = true;
                } while (!entradaValida);

                menu = Convert.ToInt32(aux);

                switch (menu)
                {
                    case 1:
                        AgregarEmpleado();
                        break;
                    case 2:
                        BuscarEmpleado();
                        break;
                    case 3:
                        ListarEmpleados();
                        break;
                    case 0:
                        Console.WriteLine("Gracias y hasta luego !...");
                        break;
                    default:
                        Console.WriteLine("Opcion invalida");
                        break;
                }
            } while (menu > 0);
        }

        static void AgregarEmpleado()
        {

            Console.Clear();
            var Db = new planmejoContext();
            string Nombre, Cedula, Salario, DiasVacaciones;
            int Cedula1, Salario1, DiasVacaciones1, divi, totalPagar;

            bool cedVal = false;
            bool nomVal = false;
            bool salVal = false;
            bool dvVal = false;

            
            Console.Clear();
            Console.SetCursorPosition(40, 5); Console.WriteLine("_________________________________");
            Console.SetCursorPosition(40, 6); Console.WriteLine("         Ingrese datos           ");
            Console.SetCursorPosition(40, 7); Console.WriteLine("_________________________________");


            do
            {
                
                Console.SetCursorPosition(20, 10); Console.WriteLine("Digite cedula del nuevo empleado: ");
                Console.SetCursorPosition(60, 10); Cedula = Console.ReadLine();
                if (!Validar.Vacio(Cedula))
                    if (Validar.TipoNumero(Cedula))
                        cedVal = true;
            } while (!cedVal);
            Cedula1 = Convert.ToInt32(Cedula);

            do
            {
                
                Console.SetCursorPosition(20, 11); Console.WriteLine("Digite el nombre del empleado: ");
                Console.SetCursorPosition(60, 11); Nombre = Console.ReadLine();
                if (!Validar.Vacio(Nombre))
                    if (Validar.TipoTexto(Nombre))
                        nomVal = true;
            } while (!nomVal);

            do
            {
                
                Console.SetCursorPosition(20, 12); Console.WriteLine("Digite sueldo del empleado: ");
                Console.SetCursorPosition(60, 12); Salario = Console.ReadLine();
                if (!Validar.Vacio(Salario))
                    if (Validar.TipoNumero(Salario))
                        salVal = true;
            } while (!salVal);
            Salario1 = Convert.ToInt32(Salario);

            do
            {
                
                Console.SetCursorPosition(20, 13); Console.WriteLine("Digite dias de vacaciones del empleado : ");
                Console.SetCursorPosition(60, 13); DiasVacaciones = Console.ReadLine();
                if (!Validar.Vacio(DiasVacaciones))
                    if (Validar.TipoNumero(DiasVacaciones))
                        dvVal = true;
            } while (!dvVal);
            DiasVacaciones1 = Convert.ToInt32(DiasVacaciones);

            divi = Salario1 / 30;
            totalPagar = divi * DiasVacaciones1;


            Empleados AUX = new Empleados();
            AUX.Cedula = (uint)Convert.ToInt32(Cedula);
            AUX.Nombre = Nombre;
            AUX.Salario = (int)Convert.ToInt32(Salario);
            AUX.DiasTrabajados = (int)Convert.ToInt32(DiasVacaciones);
            AUX.VacacionesPagar = (int)Convert.ToInt32(totalPagar);

            Db.Empleados.Add(AUX);
            Empleados.Add(AUX);
            Db.SaveChanges();

            Console.Clear();
        }

        static void BuscarEmpleado()
        {
            Console.Clear();
            var db = new planmejoContext();
            var empleados = db.Empleados.ToList();
            string cedula;
            bool cedVal = false;

            do
            {
                Console.Clear();
                Console.SetCursorPosition(35, 5); Console.WriteLine("BUSCAR UN EMPLEADO...");
                Console.SetCursorPosition(35, 10); Console.WriteLine("Digite la cedula a buscar: ");
                Console.SetCursorPosition(40, 15); cedula = (Console.ReadLine());
                if (!Validar.Vacio(cedula))
                    if (Validar.TipoNumero(cedula))
                        cedVal = true;
            } while (!cedVal);

            if (Existe(Convert.ToInt32(cedula)))
            {
                
                Console.SetCursorPosition(38, 5); Console.WriteLine("Empleado encontrado...");

                Empleados myEmpleado = ObtenerDatos(Convert.ToInt32(cedula));

                Console.SetCursorPosition(38, 8); Console.WriteLine("Cedula: " + myEmpleado.Cedula);
                Console.SetCursorPosition(38, 9); Console.WriteLine("Nombre: " + myEmpleado.Nombre);
                Console.SetCursorPosition(38, 10); Console.WriteLine("Salario: " + myEmpleado.Salario);
                Console.SetCursorPosition(38, 11); Console.WriteLine("Dias de vacaciones:" + myEmpleado.DiasTrabajados);
                Console.SetCursorPosition(38, 12); Console.WriteLine("Vacaciones a Pagar: " + myEmpleado.VacacionesPagar);

                Console.SetCursorPosition(30, 20); Console.WriteLine("Presione una tecla para continnuar");
                Console.ReadKey();

            }
            else
            {
                Console.WriteLine("El empleado no existe... ");
                Console.ReadKey();
            }
            Console.WriteLine("Presiona una tecla para continuar...");
            Console.ReadKey();
            Console.Clear();
        }

        static void ListarEmpleados()
        {
            Console.Clear();            
            var db = new planmejoContext();
            var empleados = db.Empleados.ToList();
            int y = 10;
            Console.SetCursorPosition(2, 5); Console.WriteLine("...Listar Empleados...");

            Console.SetCursorPosition(2, y); Console.WriteLine("Cedula: ");
            Console.SetCursorPosition(15, y); Console.WriteLine("Nombre: ");
            Console.SetCursorPosition(35, y); Console.WriteLine("Salarios: ");
            Console.SetCursorPosition(45, y); Console.WriteLine("Dias: ");
            Console.SetCursorPosition(55, y); Console.WriteLine("Total pago vacaciones: ");

            foreach (var myEmpleado in empleados)

            {
                y++;               
                Console.SetCursorPosition(2, y); Console.WriteLine(myEmpleado.Cedula);
                Console.SetCursorPosition(15, y); Console.WriteLine(myEmpleado.Nombre);
                Console.SetCursorPosition(35, y); Console.WriteLine(myEmpleado.Salario);
                Console.SetCursorPosition(45, y); Console.WriteLine(myEmpleado.DiasTrabajados);
                Console.SetCursorPosition(55, y); Console.WriteLine(myEmpleado.VacacionesPagar);

            }

            Console.WriteLine("\n");

            Console.SetCursorPosition(10, 20); Console.WriteLine("Presione una tecla para volver al menu principal");
            Console.ReadKey();
            Console.Clear();


        }

        static bool Existe(int cedula)
        {
            Console.Clear();
            var db = new planmejoContext();
            var empleados = db.Empleados.ToList();
            bool aux = false;
            foreach (var myEmpleado in empleados)
            {
                if (myEmpleado.Cedula == cedula)
                    aux = true;
            }
            return aux;
        }

        static Empleados ObtenerDatos(int cedula)
        {
            var db = new planmejoContext();
            var empleados = db.Empleados.ToList();
            foreach (Empleados ObjetoEmpleado in empleados)
            {
                if (ObjetoEmpleado.Cedula == cedula)
                    return ObjetoEmpleado;
            }
            return null;

        }





    }
}
