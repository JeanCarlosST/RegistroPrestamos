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
        public static bool Guardar(Persona persona){
            if(!Existe(persona.PersonaID))
                return Insertar(persona); 
            else    
                return Modificar(persona);
        }
        private static bool Insertar(Persona persona){
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
        public static bool Modificar(Persona persona){
            Context context = new Context();
            bool found = false;

            try{
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
        public static Persona Buscar(int id){
            Context context = new Context();
            Persona persona;

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
                found = context.Personas.Any(p => p.PersonaID == id);
            
            } catch{
                throw;
            
            } finally{
                context.Dispose();
            }

            return found;
        }

        public static List<Persona> GetList(Expression<Func<Persona, bool>> criterio)
        {
            List<Persona> list = new List<Persona>();
            Context context = new Context();

            try {
                list = context.Personas.Where(criterio).AsNoTracking().ToList();

            } catch  {
                throw;

            } finally {
                context.Dispose();
            }

            return list;
        }

        public static List<Persona> Getactor()
        {
            List<Persona> list = new List<Persona>();
            Context context = new Context();

            try {
                list = context.Personas.ToList();

            } catch  {
                throw;

            } finally {
                context.Dispose();
            }

            return list;
        }
    }
}