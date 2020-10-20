using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using RegistroPrestamos.DAL;
using RegistroPrestamos.Entities;

namespace RegistroPrestamos.BLL
{
    public class PersonaBLL
    {
        public static bool Guardar(Personas persona){
            if(!Existe(persona.PersonaId))
                return Insertar(persona); 
            else    
                return Modificar(persona);
        }
        private static bool Insertar(Personas persona){
            Context context = new Context();
            bool found = false;

            try{
                context.Personas.Add(persona);
                found = context.SaveChanges() > 0;
            
            } catch{
                throw;
            
            } finally{
                context.Dispose();
            }

            return found;
        }
        public static bool Modificar(Personas persona){
            Context context = new Context();
            bool found = false;

            try
            {
                context.Entry(persona).State = EntityState.Modified;
                found = context.SaveChanges() > 0;
            
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
                var persona = context.Personas.Find(id);

                if(persona != null){
                    context.Personas.Remove(persona);
                    found = context.SaveChanges() > 0;
                }
            
            } catch{
                throw;
            
            } finally{
                context.Dispose();
            }

            return found;
        }
        public static Personas Buscar(int id){
            Context context = new Context();
            Personas persona;

            try{
                persona = context.Personas.Find(id);
            
            } catch{
                throw;
            
            } finally{
                context.Dispose();
            }

            return persona;
        }
        public static bool Existe(int id){
            Context context = new Context();
            bool found = false;

            try{
                found = context.Personas.Any(p => p.PersonaId == id);
            
            } catch{
                throw;
            
            } finally{
                context.Dispose();
            }

            return found;
        }

        public static List<Personas> GetList(Expression<Func<Personas, bool>> criterio)
        {
            List<Personas> list = new List<Personas>();
            Context context = new Context();

            try {
                list = context.Personas.Where(criterio).AsNoTracking().ToList<Personas>();

            } catch  {
                throw;

            } finally {
                context.Dispose();
            }

            return list;
        }
    }
}