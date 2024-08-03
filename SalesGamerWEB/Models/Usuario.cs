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
		public string usuario { get; set; }
		public string Mail { get; set; }
		public string Contraseña { get; set; }
		public string? Nombre { get; set; }
		public string? Apellido { get; set; }
		public int? Telefono { get; set; }
		public string? Direccion { get; set; }
		public int? Rol { get; set; }

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
			Rol = rol;
		}

		public Usuario(int id, string nombre_usuario, string mail, string contrasena_usuario)
		{
			Id = id;
			usuario = nombre_usuario;
			Mail = mail;
			Contraseña = contrasena_usuario;
		}

		public Usuario() { }
	}

}
