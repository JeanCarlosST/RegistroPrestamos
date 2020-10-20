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
        public static bool Guardar(Prestamos prestamo){
            if(!Existe(prestamo.PrestamoId))
                return Insertar(prestamo); 
            else    
                return Modificar(prestamo);
        }
        private static bool Insertar(Prestamos prestamo){
            Context context = new Context();
            bool found = false;

            try{
                prestamo.Balance = prestamo.Monto;
                context.Prestamos.Add(prestamo);
                found = context.SaveChanges() > 0;

                Personas persona = PersonaBLL.Buscar(prestamo.PersonaId);
                persona.Balance += prestamo.Monto;
                PersonaBLL.Modificar(persona);

            } catch{
                throw;
            
            } finally{
                context.Dispose();
            }

            return found;
        }
        public static bool Modificar(Prestamos prestamo){
            Context context = new Context();
            bool found = false;

            try{
                prestamo.Balance = prestamo.Monto;
                Prestamos viejoPrestamo = PrestamoBLL.Buscar(prestamo.PrestamoId);
                float nuevoMonto = prestamo.Monto - viejoPrestamo.Monto;
                
                context.Database.ExecuteSqlRaw($"delete from MorasDetalle where PrestamoId = {prestamo.PrestamoId}");
                foreach(var anterior in prestamo.Detalle)
                {
                    context.Entry(anterior).State = EntityState.Added;
                }

                context.Entry(prestamo).State = EntityState.Modified;
                found = context.SaveChanges() > 0;
            
                Personas persona = PersonaBLL.Buscar(prestamo.PersonaId);
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
                    Personas persona = PersonaBLL.Buscar(prestamo.PersonaId);
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
        public static Prestamos Buscar(int id){
            Context context = new Context();
            Prestamos prestamo;

            try{
                prestamo = context.Prestamos
                    .Include(p => p.Detalle)
                    .Where(p => p.PrestamoId == id)
                    .SingleOrDefault();
                
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
                found = context.Prestamos.Any(p => p.PrestamoId == id);
            
            } catch{
                throw;
            
            } finally{
                context.Dispose();
            }

            return found;
        }

        public static List<Prestamos> GetList(Expression<Func<Prestamos, bool>> criterio)
        {
            List<Prestamos> list = new List<Prestamos>();
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