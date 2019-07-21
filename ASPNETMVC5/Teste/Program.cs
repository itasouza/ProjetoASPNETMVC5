using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Teste
{
    class Program
    {
        static void Main(string[] args)
        {

            using (Modelos.Repositorio<Modelos.Estatu> obj = new Modelos.Repositorio<Modelos.Estatu>())
            {
                obj.exception += obj_Exception;
                obj.Delete(obj.Retrieve(x => x.Id == 2));

                var listado = obj.Filter(x => true);
                foreach (var item in listado)
                {
                    Console.WriteLine(item.Descripcion);
                }


            }

            Console.WriteLine("Pressione <enter> para sair");
            Console.ReadLine();
        }

        private static void obj_Exception(object sender, Modelos.ExceptionEventArgs e)
        {
            Console.WriteLine(string.Format("Mensagem de error: {0}", e.Mensagem));
            if (e.EntityValidationErrors != null) {

                foreach (var item in e.EntityValidationErrors)
                {
                    foreach (var error in item.ValidationErrors)
                    {
                        Console.WriteLine(
                            string.Format("Mensagem:{0},PropertyName:{1}", error.ErrorMessage, error.PropertyName));
                    }
                }
            }
        }

    }
}
