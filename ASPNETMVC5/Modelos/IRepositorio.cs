using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Modelos
{
    public interface IRepositorio<TEntity>:IDisposable
    {
        //criar
        TEntity Create(TEntity toCreate);
        //recuperar 
        TEntity Retrieve(Expression<Func<TEntity, bool>> criterio);
        //atualizar
        bool Update(TEntity toUpdate);
        //deletar
        bool Delete(TEntity toDelete);
        //retonar uma lista
        List<TEntity> Filter(Expression<Func<TEntity, bool>> criterio);
    }
}
