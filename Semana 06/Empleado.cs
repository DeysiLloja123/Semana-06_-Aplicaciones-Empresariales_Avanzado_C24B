namespace Semana_06
{
    public class Empleado
    {
        // Propiedades de la clase Empleado
        public int IdEmpleado { get; set; }
        public string Apellidos { get; set; }
        public string Nombre { get; set; }
        public string Cargo { get; set; }
        public string Tratamiento { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public DateTime FechaContratacion { get; set; }
        public string Direccion { get; set; }
        public string Ciudad { get; set; }
        public string Region { get; set; }
        public string CodPostal { get; set; }
        public string Pais { get; set; }
        public string TelDomicilio { get; set; }
        public string Extension { get; set; }
        public string Notas { get; set; }
        public int Jefe { get; set; }
        public decimal SueldoBasico { get; set; }




        // Constructor de la clase Empleado
        public Empleado(int idEmpleado, string apellidos, string nombre, string cargo, string tratamiento,
                         DateTime fechaNacimiento, DateTime fechaContratacion, string direccion, string ciudad,
                         string region, string codPostal, string pais, string telDomicilio, string extension,
                         string notas, int jefe, decimal sueldoBasico)
        {
            IdEmpleado = idEmpleado;
            Apellidos = apellidos;
            Nombre = nombre;
            Cargo = cargo;
            Tratamiento = tratamiento;
            FechaNacimiento = fechaNacimiento;
            FechaContratacion = fechaContratacion;
            Direccion = direccion;
            Ciudad = ciudad;
            Region = region;
            CodPostal = codPostal;
            Pais = pais;
            TelDomicilio = telDomicilio;
            Extension = extension;
            Notas = notas;
            Jefe = jefe;
            SueldoBasico = sueldoBasico;
        }
    }

}