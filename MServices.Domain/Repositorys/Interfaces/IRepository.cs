namespace MServices.Domain.Repositorys.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Threading.Tasks;

    public interface IRepository<TEntity>
    {
        Task Inserir(TEntity item);

        Task Atualizar(TEntity item);

        Task Remover(int id);

        Task<IEnumerable<TEntity>> ObterTodos();

        Task<TEntity> ObterPorId(int id);

        Task<IEnumerable<TEntity>> ObterPorExpressao(Expression<Func<TEntity, bool>> expression);
    }
}
