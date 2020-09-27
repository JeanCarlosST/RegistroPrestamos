using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using RegistroPrestamos.DAL;
using RegistroPrestamos.Entities;

namespace RegistroPrestamos.BLL
{
    public class PrestamoBLL
    {
        public static bool Guardar(Prestamo prestamo){
            if(!Existe(prestamo.PrestamoID))
                return Insertar(prestamo); 
            else    
                return Modificar(prestamo);
        }
        private static bool Insertar(Prestamo prestamo){
            Context context = new Context();
            bool found = false;

            try{
                prestamo.Balance = prestamo.Monto;
                context.Prestamos.Add(prestamo);
                found = context.SaveChanges() > 0;

                Persona persona = PersonaBLL.Buscar(prestamo.PersonaID);
                persona.Balance += prestamo.Monto;
                PersonaBLL.Modificar(persona);

            } catch{
                throw;
            
            } finally{
                context.Dispose();
            }

            return found;
        }
        public static bool Modificar(Prestamo prestamo){
            Context context = new Context();
            bool found = false;

            try{
                prestamo.Balance = prestamo.Monto;
                Prestamo viejoPrestamo = PrestamoBLL.Buscar(prestamo.PrestamoID);
                float nuevoMonto = prestamo.Monto - viejoPrestamo.Monto;
                
                context.Entry(prestamo).State = EntityState.Modified;
                found = context.SaveChanges() > 0;
            
                Persona persona = PersonaBLL.Buscar(prestamo.PersonaID);
                persona.Balance += nuevoMonto;
                PersonaBLL.Modificar(persona);

            } catch{
                throw;
            
            } finally{
                context.Dispose();
            }

            return found;
        }
        public static bool Eliminar(int id){
            Context context = new Context();
            bool found = false;

            try{
                var prestamo = context.Prestamos.Find(id);


                if(prestamo != null){
                    Persona persona = PersonaBLL.Buscar(prestamo.PersonaID);
                    persona.Balance -= prestamo.Monto;
                    PersonaBLL.Modificar(persona);

                    context.Prestamos.Remove(prestamo);
                    found = context.SaveChanges() > 0;
                }
            
            } catch{
                throw;
            
            } finally{
                context.Dispose();
            }

            return found;
        }
        public static Prestamo Buscar(int id){
            Context context = new Context();
            Prestamo prestamo;

            try{
                prestamo = context.Prestamos.Find(id);
                
            } catch{
                throw;
            
            } finally{
                context.Dispose();
            }

            return prestamo;
        }
        public static bool Existe(int id){
            Context context = new Context();
            bool found = false;

            try{
                found = context.Prestamos.Any(p => p.PrestamoID == id);
            
            } catch{
                throw;
            
            } finally{
                context.Dispose();
            }

            return found;
        }

        public static List<Prestamo> GetList(Expression<Func<Prestamo, bool>> criterio)
        {
            List<Prestamo> list = new List<Prestamo>();
            Context context = new Context();

            try {
                list = context.Prestamos.Where(criterio).AsNoTracking().ToList();

            } catch  {
                throw;

            } finally {
                context.Dispose();
            }

            return list;
        }
    }
}