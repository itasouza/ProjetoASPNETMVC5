﻿using MetodosDeExtension;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Modelos
{

    public delegate void ExceptionEventHandler(object sender, ExceptionEventArgs e);
    public class Repositorio<TEntity> : IRepositorio<TEntity> where TEntity : class
    {
        public event ExceptionEventHandler exception;
        ASPNETMVC5 Context;

        public Repositorio()
        {
            Context = new ASPNETMVC5();
        }

        private DbSet<TEntity> EntitySet { get { return Context.Set<TEntity>(); } }
   
        public TEntity Create(TEntity toCreate)
        {
            TEntity Result = null;
            try
            {
                Result = EntitySet.Add(toCreate);
                Context.SaveChanges();
            }
            catch(DbEntityValidationException ex)
            {
                exception?.Invoke(this, new ExceptionEventArgs()
                {
                    InnerException = ex.InnerException,
                    Mensagem = ex.Message,
                    fonte = ex.Source,
                    StackTrace = ex.StackTrace,
                    TargetSite = ex.TargetSite,
                    EntityValidationErrors = ex.EntityValidationErrors
                });
                this.EscribirEnArchivoLog(ex);
            }
            catch (Exception ex)
            {

                exception?.Invoke(this, new ExceptionEventArgs()
                {
                    InnerException = ex.InnerException,
                    Mensagem = ex.Message,
                    fonte = ex.Source,
                    StackTrace = ex.StackTrace,
                    TargetSite = ex.TargetSite
                });
                this.EscribirEnArchivoLog(ex);
            }
            return Result;
        }

        public bool Delete(TEntity toDelete)
        {
            bool Result = false;
            try
            {
                EntitySet.Attach(toDelete);
                EntitySet.Remove(toDelete);
                Result = Context.SaveChanges() > 0;

            }
            catch (DbEntityValidationException ex)
            {
                exception?.Invoke(this, new ExceptionEventArgs()
                {
                    InnerException = ex.InnerException,
                    Mensagem = ex.Message,
                    fonte = ex.Source,
                    StackTrace = ex.StackTrace,
                    TargetSite = ex.TargetSite
                });
                this.EscribirEnArchivoLog(ex);
            }
            catch (Exception ex)
            {

                exception?.Invoke(this, new ExceptionEventArgs()
                {
                    InnerException = ex.InnerException,
                    Mensagem = ex.Message,
                    fonte = ex.Source,
                    StackTrace = ex.StackTrace,
                    TargetSite = ex.TargetSite
                });
                this.EscribirEnArchivoLog(ex);
            }
            return Result;
        }

        public List<TEntity> Filter(Expression<Func<TEntity, bool>> criterio)
        {
            List<TEntity> Resultado = new List<TEntity>();
            try
            {
                Resultado = EntitySet.Where(criterio).ToList();
            }
            catch (DbEntityValidationException ex)
            {
                exception?.Invoke(this, new ExceptionEventArgs()
                {
                    InnerException = ex.InnerException,
                    Mensagem = ex.Message,
                    fonte = ex.Source,
                    StackTrace = ex.StackTrace,
                    TargetSite = ex.TargetSite,
                    EntityValidationErrors = ex.EntityValidationErrors
                });
                this.EscribirEnArchivoLog(ex);
            }
            catch (Exception ex)
            {

                exception?.Invoke(this, new ExceptionEventArgs()
                {
                    InnerException = ex.InnerException,
                    Mensagem = ex.Message,
                    fonte = ex.Source,
                    StackTrace = ex.StackTrace,
                    TargetSite = ex.TargetSite
                });
                this.EscribirEnArchivoLog(ex);
            }
            return Resultado;
        }


        public TEntity Retrieve(Expression<Func<TEntity, bool>> criterio)
        {
            TEntity Resultado = null;
            try
            {
                Resultado = EntitySet.FirstOrDefault(criterio);
            }
            catch (DbEntityValidationException ex)
            {
                exception?.Invoke(this, new ExceptionEventArgs()
                {
                    InnerException = ex.InnerException,
                    Mensagem = ex.Message,
                    fonte = ex.Source,
                    StackTrace = ex.StackTrace,
                    TargetSite = ex.TargetSite,
                    EntityValidationErrors = ex.EntityValidationErrors
                });
                this.EscribirEnArchivoLog(ex);
            }
            catch (Exception ex)
            {

                exception?.Invoke(this, new ExceptionEventArgs()
                {
                    InnerException = ex.InnerException,
                    Mensagem = ex.Message,
                    fonte = ex.Source,
                    StackTrace = ex.StackTrace,
                    TargetSite = ex.TargetSite
                });
                this.EscribirEnArchivoLog(ex);
            }
            return Resultado;
        }

        public bool Update(TEntity toUpdate)
        {
            bool Resultado = false;
            try
            {
                EntitySet.Attach(toUpdate);
                Context.Entry<TEntity>(toUpdate).State = EntityState.Modified;
                Resultado = Context.SaveChanges() > 0;
            }
            catch (DbEntityValidationException ex)
            {
                exception?.Invoke(this, new ExceptionEventArgs()
                {
                    InnerException = ex.InnerException,
                    Mensagem = ex.Message,
                    fonte = ex.Source,
                    StackTrace = ex.StackTrace,
                    TargetSite = ex.TargetSite,
                    EntityValidationErrors = ex.EntityValidationErrors
                });
                this.EscribirEnArchivoLog(ex);
            }
            catch (Exception ex)
            {

                exception?.Invoke(this, new ExceptionEventArgs()
                {
                    InnerException = ex.InnerException,
                    Mensagem = ex.Message,
                    fonte = ex.Source,
                    StackTrace = ex.StackTrace,
                    TargetSite = ex.TargetSite
                });
                this.EscribirEnArchivoLog(ex);
            }
            return Resultado;
        }

        public bool Update(Expression<Func<TEntity,bool>> criterio, string propertyName, object valor)
        {
            bool Resultado = false;
            try
            {
                Context.Entry<TEntity>(EntitySet.FirstOrDefault(criterio)).Property(propertyName).CurrentValue = valor;
                Resultado = Context.SaveChanges() > 0;
            }
            catch (DbEntityValidationException ex)
            {
                exception?.Invoke(this, new ExceptionEventArgs()
                {
                    InnerException = ex.InnerException,
                    Mensagem = ex.Message,
                    fonte = ex.Source,
                    StackTrace = ex.StackTrace,
                    TargetSite = ex.TargetSite,
                    EntityValidationErrors = ex.EntityValidationErrors
                });
                this.EscribirEnArchivoLog(ex);
            }
            catch (Exception ex)
            {

                exception?.Invoke(this, new ExceptionEventArgs()
                {
                    InnerException = ex.InnerException,
                    Mensagem = ex.Message,
                    fonte = ex.Source,
                    StackTrace = ex.StackTrace,
                    TargetSite = ex.TargetSite
                });
                this.EscribirEnArchivoLog(ex);
            }
            return Resultado;
        }

        public void Dispose()
        {
            if (Context != null)
                Context.Dispose();
        }
    }

    public class ExceptionEventArgs:EventArgs
    {
        public string Mensagem { get; set; }
        public string fonte { get; set; }
        public string StackTrace { get; set; }
        public Exception InnerException { get; set; }
        public MethodBase TargetSite { get; set; }
        public IEnumerable<DbEntityValidationResult> EntityValidationErrors { get; set; }
    }
}
