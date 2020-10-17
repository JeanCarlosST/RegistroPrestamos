using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using RegistroPrestamos.DAL;
using RegistroPrestamos.Entities;

namespace RegistroPrestamos.BLL
{
    public class MorasBLL
    {
        public static bool Guardar(Moras mora){
            if(!Existe(mora.MoraID))
                return Insertar(mora); 
            else    
                return Modificar(mora);
        }
        private static bool Insertar(Moras mora){
            Context context = new Context();
            bool found = false;

            try
            {
                context.Moras.Add(mora);
                found = context.SaveChanges() > 0;

            } catch{
                throw;
            
            } finally{
                context.Dispose();
            }

            return found;
        }
        public static bool Modificar(Moras mora){
            Context context = new Context();
            bool found = false;

            try
            {
                context.Database.ExecuteSqlRaw($"delete from MorasDetalle where MoraID = {mora.MoraID}");
                foreach(var anterior in mora.Detalle)
                {
                    context.Entry(anterior).State = EntityState.Added;
                }

                context.Entry(mora).State = EntityState.Modified;
                found = context.SaveChanges() > 0;
            } 
            catch
            {
                throw;
            } 
            finally
            {
                context.Dispose();
            }

            return found;
        }
        public static bool Eliminar(int id){
            Context context = new Context();
            bool found = false;

            try{
                var mora = context.Moras.Find(id);

                if(mora != null){
                    context.Entry(mora).State = EntityState.Deleted;
                    found = context.SaveChanges() > 0;
                }
            
            } catch{
                throw;
            
            } finally{
                context.Dispose();
            }

            return found;
        }
        public static Moras Buscar(int id){
            Context context = new Context();
            Moras mora;

            try{
                mora = context.Moras
                    .Include(m => m.Detalle)
                    .Where(m => m.MoraID == id)
                    .SingleOrDefault();
                
            } catch{
                throw;
            
            } finally{
                context.Dispose();
            }

            return mora;
        }
        public static bool Existe(int id){
            Context context = new Context();
            bool found = false;

            try{
                found = context.Moras.Any(p => p.MoraID == id);
            
            } catch{
                throw;
            
            } finally{
                context.Dispose();
            }

            return found;
        }

        public static List<Moras> GetList(Expression<Func<Moras, bool>> criterio)
        {
            List<Moras> list = new List<Moras>();
            Context context = new Context();

            try {
                list = context.Moras.Where(criterio).AsNoTracking().ToList();

            } catch  {
                throw;

            } finally {
                context.Dispose();
            }

            return list;
        }
    }
}