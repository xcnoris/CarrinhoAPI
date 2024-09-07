using CarrinhoAPI.Repository.DataBase;

namespace CarrinhoAPI.Repository.Interfaces
{
    public interface IEndPointCrud<T>
    {
        // Recupera todos os itens
        public Task<List<T>> BuscarTodos();

        // Recupera um item pelo ID
        Task<T> BuscarPorId(int id);

        // Adiciona um novo item
        Task<T> Adicionar(T entity);

        // Atualiza um item existente
        public Task<T> Atualizar(int Id, T entity);

        // Remove um item pelo ID
        Task<bool> Remover(int id);

    }
}
