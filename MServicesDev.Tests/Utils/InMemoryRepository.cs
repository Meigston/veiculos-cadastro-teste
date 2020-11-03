namespace MServicesDev.Tests.Utils
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;

    using MServices.Domain.Models.Base;
    using MServices.Domain.Repositorys.Interfaces;

    public class InMemoryRepository<T> : IRepository<T>
    {
        private static List<T> dataBaseInMemory;
        private static Random idRandom;

        public InMemoryRepository()
        {
            idRandom = new Random();
            dataBaseInMemory = new List<T>();
        }

        public Task Inserir(T item)
        {
            return Task.Run(() =>
                {
                    (item as ModelBase).Id = idRandom.Next();
                    dataBaseInMemory.Add(item);
                });
        }

        public Task Atualizar(T item)
        {
            return Task.Run(() =>
                {
                    var itemEntity = dataBaseInMemory.Where(w => (item as ModelBase).Id == (w as ModelBase).Id).FirstOrDefault();

                    if (itemEntity != null)
                    {
                        dataBaseInMemory.Add(item);
                        dataBaseInMemory.Remove(itemEntity);
                    }
                });
        }

        public Task Remover(int id)
        {
            return Task.Run(() =>
                {
                    var itemEntity = dataBaseInMemory.Where(w => id == (w as ModelBase).Id).FirstOrDefault();

                    if (itemEntity != null)
                    {
                        dataBaseInMemory.Remove(itemEntity);
                    }
                });
        }

        public Task<IEnumerable<T>> ObterTodos()
        {
            return Task.Run(() =>
                    {
                        return dataBaseInMemory.AsEnumerable();
                    });
        }

        public Task<T> ObterPorId(int id)
        {
            return Task.Run(() =>
                    {
                        return dataBaseInMemory.Where(w => id == (w as ModelBase).Id).FirstOrDefault();
                    });
        }

        public Task<IEnumerable<T>> ObterPorExpressao(Expression<Func<T, bool>> expression)
        {
            return Task.Run(() =>
                {
                    return dataBaseInMemory.Where(expression.Compile());
                });
        }
    }
}
