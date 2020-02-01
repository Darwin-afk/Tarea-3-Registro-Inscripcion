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
    public class PersonasBLL
    {
        public static bool Guardar(Personas persona)
        {
            bool paso = false;
            Contexto db = new Contexto();

            try
            {
                if (db.Personas.Add(persona) != null)
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
        public static bool Modificar(Personas persona)
        {
            bool paso = false;
            Contexto db = new Contexto();

            try
            {
                db.Entry(persona).State = EntityState.Modified;
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


                List<Inscripciones> listado = InscripcionesBLL.GetList(p => p.PersonaId == id);
                foreach(Inscripciones inscripcion in listado)
                {
                    InscripcionesBLL.Eliminar(inscripcion.InscripcionId);
                }

                var eliminar = db.Personas.Find(id);
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
        public static Personas Buscar(int PersonaId)
        {
            Contexto db = new Contexto();
            Personas persona = new Personas();

            try
            {
                persona = db.Personas.Find(PersonaId);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                db.Dispose();
            }
            return persona;
        }
        public static List<Personas> GetList(Expression<Func<Personas, bool>> persona)
        {
            List<Personas> Lista = new List<Personas>();
            Contexto db = new Contexto();

            try
            {
                Lista = db.Personas.Where(persona).ToList();
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

        public static int ultimoRegistro()
        {
            int valor = 0;
            Contexto db = new Contexto();

            try
            {
                var ultimo = db.Personas
                    .OrderByDescending(x => x.PersonaId)
                    .First();
                valor = ultimo.PersonaId;
            }
            catch(Exception)
            {
                throw;
            }
            finally
            {
                db.Dispose();
            }

            return valor;
        }
    }
}
