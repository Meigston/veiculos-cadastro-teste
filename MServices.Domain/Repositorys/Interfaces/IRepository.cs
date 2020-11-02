namespace MServices.Domain.Services.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Threading.Tasks;

    using MServices.Domain.Models;

    public interface IRepository<TEntity>
    {
        Task Inserir(TEntity item);

        Task Atualizar(TEntity item);

        Task Remover(int id);

        Task<IEnumerable<Veiculo>> ObterTodos();

        Task<Veiculo> ObterPorId(int id);

        Task<IEnumerable<Veiculo>> ObterPorExpressao(Expression<Func<TEntity, bool>> expression);
    }
}
