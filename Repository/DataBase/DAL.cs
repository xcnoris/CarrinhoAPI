using CarrinhoAPI.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CarrinhoAPI.Repository.DataBase
{
    public class DAL<T> where T : class
    {
        private readonly Context context;

        public DAL(Context context)
        {
            this.context = context;
        }

        public IEnumerable<T> Listar()
        {
            return context.Set<T>().ToList();
        }

        public async Task<IEnumerable<T>> ListarAsync()
        {
            return await context.Set<T>().ToListAsync();
        }

        public async Task<IEnumerable<T>> ListarAsyncComInclude(string include)
        {
            return await context
                .Set<T>()
                .Include(include)
                .ToListAsync();
        }

        public void Adicionar(T objeto)
        {
            context.Set<T>().Add(objeto);
            context.SaveChanges();
        }

        public async Task AdicionarAsync(T objeto)
        {
            await context.Set<T>().AddAsync(objeto);
            await context.SaveChangesAsync();
        }

        public void Atualizar(T objeto)
        {
            context.Set<T>().Update(objeto);
            context.SaveChanges();
        }

        public async Task AtualizarAsync(T objeto)
        {
            context.Set<T>().Update(objeto);
            await context.SaveChangesAsync();
        }

        public void Deletar(T objeto)
        {
            context.Set<T>().Remove(objeto);
            context.SaveChanges();
        }

        public async Task DeletarAsync(T objeto)
        {
            context.Set<T>().Remove(objeto);
            await context.SaveChangesAsync();
        }

        public T? BuscarPor(Func<T, bool> condicao)
        {
            return context.Set<T>().FirstOrDefault(condicao);
        }

        public async Task<T?> BuscarPorAsync(Expression<Func<T, bool>> condicao)
        {
            return await context.Set<T>().FirstOrDefaultAsync(condicao);
        }

        public async Task<T?> RecuperarPorAsync(Expression<Func<T, bool>> condicao, string includeProperties = "")
        {
            IQueryable<T> query = context.Set<T>();

            foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            return await query.FirstOrDefaultAsync(condicao);
        }
    }
}
