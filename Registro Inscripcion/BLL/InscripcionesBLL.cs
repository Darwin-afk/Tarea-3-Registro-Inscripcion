using Microsoft.EntityFrameworkCore;
using Registro_Inscripcion.DAL;
using Registro_Inscripcion.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Registro_Inscripcion.BLL
{
    public class InscripcionesBLL
    {
        public static bool Guardar(Inscripciones inscripcion)
        {
            bool paso = false;
            Contexto db = new Contexto();

            try
            {
                if (db.Inscripciones.Add(inscripcion) != null)
                    paso = db.SaveChanges() > 0;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                db.Dispose();
            }

            return paso;
        }
        public static bool Modificar(Inscripciones inscripcion)
        {
            bool paso = false;
            Contexto db = new Contexto();

            try
            {
                db.Entry(inscripcion).State = EntityState.Modified;
                paso = db.SaveChanges() > 0;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                db.Dispose();
            }

            return paso;
        }
        public static bool Eliminar(int id)
        {
            bool paso = false;
            Contexto db = new Contexto();

            try
            {
                var eliminar = db.Inscripciones.Find(id);
                Personas persona = PersonasBLL.Buscar(eliminar.PersonaId);
                persona.Balance -= eliminar.Balance;
                db.Entry(eliminar).State = EntityState.Deleted;

                paso = db.SaveChanges() > 0;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                db.Dispose();
            }

            return paso;
        }
        public static Inscripciones Buscar(int InscripcionId)
        {
            Contexto db = new Contexto();
            Inscripciones inscripcion = new Inscripciones();

            try
            {
                inscripcion = db.Inscripciones.Find(InscripcionId);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                db.Dispose();
            }
            return inscripcion;
        }
        public static List<Inscripciones> GetList(Expression<Func<Inscripciones, bool>> inscripcion)
        {
            List<Inscripciones> Lista = new List<Inscripciones>();
            Contexto db = new Contexto();

            try
            {
                Lista = db.Inscripciones.Where(inscripcion).ToList();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                db.Dispose();
            }

            return Lista;
        }
    }
}
