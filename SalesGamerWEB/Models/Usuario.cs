using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesGamerWEB.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        public String usuario { get; set; }
        public String Mail { get; set; }
        public String Contraseña { get; set; }
        public String Nombre { get; set; }
        public String Apellido { get; set; }
        public int Telefono { get; set; }
        public String Direccion { get; set; }
        public int ID_rol { get; set; }



        public Usuario(int id, string nombre_usuario, string mail, string contrasena_usuario, string nombre, string apellido, int telefono, string direccion, int rol)
        {
            Id = id;
            usuario = nombre_usuario;
            Mail = mail;
            Contraseña = contrasena_usuario;
            Nombre = nombre;
            Apellido = apellido;
            Telefono = telefono;
            Direccion = direccion;
            ID_rol = rol;
        }

        public Usuario()
        {

        }
    }
}
