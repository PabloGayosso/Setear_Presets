using System;
using System.Collections.Generic;
using VideoOS.Platform;
using VideoOS.Platform.ConfigurationItems;
using VideoOS.Platform.SDK.Platform;

namespace SetPresetMilestone
{
    class Program
    {
        private static Item _camaraEncontrada = null;

        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("Inicializando entorno...");
                VideoOS.Platform.SDK.Environment.Initialize();

                Console.WriteLine("Inicialización completada.");

                Console.Write("Nombre parcial de la cámara a buscar: ");
                string nombreCamara = Console.ReadLine();

                BuscarCamara(nombreCamara);

                if (_camaraEncontrada != null)
                {
                    Console.WriteLine($"Cámara encontrada: {_camaraEncontrada.Name}");
                }
                else
                {
                    Console.WriteLine("No se encontró ninguna cámara con ese nombre.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR GENERAL: " + ex.Message);
                Console.WriteLine("Detalles: " + ex.ToString());
            }

            Console.WriteLine("Presiona una tecla para salir...");
            Console.ReadKey();
        }

        static void BuscarCamara(string nombreParcial)
        {
            try
            {
                Console.WriteLine("Buscando cámaras...");
                List<Item> rootItems = Configuration.Instance.GetItems();

                foreach (Item item in rootItems)
                {
                    CheckChildren(item, nombreParcial);
                    if (_camaraEncontrada != null) break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al buscar cámara: " + ex.Message);
            }
        }

        static void CheckChildren(Item parent, string nombreParcial)
        {
            try
            {
                var hijos = parent.GetChildren();
                if (hijos == null) return;

                foreach (var item in hijos)
                {
                    if (item.FQID.Kind == Kind.Camera && item.FQID.FolderType == FolderType.No)
                    {
                        if (item.Name.IndexOf(nombreParcial, StringComparison.OrdinalIgnoreCase) >= 0)
                        {
                            _camaraEncontrada = item;
                            return;
                        }
                    }
                    else if (item.HasChildren != HasChildren.No)
                    {
                        CheckChildren(item, nombreParcial);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al revisar hijos: " + ex.Message);
            }
        }
    }
}



//using System;
//using System.Collections.Generic;
//using VideoOS.Platform;
//using VideoOS.Platform.ConfigurationItems;
//using VideoOS.Platform.SDK.Platform;

//namespace SetPresetMilestone
//{
//    class Program
//    {
//        private static Item _camaraEncontrada = null;

//        static void Main(string[] args)
//        {
//            Console.WriteLine("Inicializando entorno...");

//            // 
//            VideoOS.Platform.SDK.Environment.Initialize();

//            //

//            Console.Write("Nombre parcial de la cámara a buscar: ");
//            string nombreCamara = Console.ReadLine();

//            BuscarCamara(nombreCamara);

//            if (_camaraEncontrada != null)
//            {
//                Console.WriteLine($"Cámara encontrada: {_camaraEncontrada.Name}");
//            }
//            else
//            {
//                Console.WriteLine("No se encontró ninguna cámara con ese nombre.");
//            }

//            Console.WriteLine("Presiona una tecla para salir...");
//            Console.ReadKey();
//        }

//        static void BuscarCamara(string nombreParcial)
//        {
//            try
//            {
//                List<Item> rootItems = Configuration.Instance.GetItems();

//                foreach (Item item in rootItems)
//                {
//                    CheckChildren(item, nombreParcial);
//                    if (_camaraEncontrada != null) break;
//                }
//            }
//            catch (Exception ex)
//            {
//                Console.WriteLine("Error al buscar cámara: " + ex.Message);
//            }
//        }

//        static void CheckChildren(Item parent, string nombreParcial)
//        {
//            var hijos = parent.GetChildren();
//            if (hijos == null) return;

//            foreach (var item in hijos)
//            {
//                if (item.FQID.Kind == Kind.Camera && item.FQID.FolderType == FolderType.No)
//                {
//                    if (item.Name.IndexOf(nombreParcial, StringComparison.OrdinalIgnoreCase) >= 0)
//                    {
//                        _camaraEncontrada = item;
//                        return;
//                    }
//                }
//                else if (item.HasChildren != HasChildren.No)
//                {
//                    CheckChildren(item, nombreParcial);
//                }
//            }
//        }
//    }
//}
