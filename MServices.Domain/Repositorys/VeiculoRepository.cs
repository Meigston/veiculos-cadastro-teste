namespace MServices.Domain.Repositorys
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;

    using MServices.Domain.Infra.DB;
    using MServices.Domain.Menssages;
    using MServices.Domain.Models;
    using MServices.Domain.Repositorys.Interfaces;

    public class VeiculoRepository : IRepository<Veiculo>
    {
        private readonly FrotaContext frotaContext;

        public VeiculoRepository(FrotaContext frotaContext)
        {
            this.frotaContext = frotaContext;
        }

        public async Task Inserir(Veiculo item)
        {
            this.frotaContext.Veiculos.Add(item);
            await this.frotaContext.SaveChangesAsync();
        }

        public async Task Atualizar(Veiculo item)
        {
            this.frotaContext.Update(item);
            await this.frotaContext.SaveChangesAsync();
        }

        public async Task Remover(int id)
        {
            var veiculo = await this.ObterPorId(id);
            this.frotaContext.Remove(veiculo);
            await this.frotaContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Veiculo>> ObterTodos()
        {
            return await Task.Run(() => this.frotaContext.Veiculos.AsEnumerable());
        }

        public async Task<Veiculo> ObterPorId(int id)
        {
            return await Task.Run(() =>
            {
                var veiculo = this.frotaContext.Veiculos.Where(w => w.Id == id).FirstOrDefault();

                if (veiculo == null)
                {
                    throw new KeyNotFoundException(string.Format(Excecoes.ID_NotFound, id));
                }

                return veiculo;
            });
        }

        public async Task<IEnumerable<Veiculo>> ObterPorExpressao(Expression<Func<Veiculo, bool>> expression)
        {
            return await Task.Run(() => this.frotaContext.Veiculos.Where(expression).AsEnumerable());
        }
    }
}
