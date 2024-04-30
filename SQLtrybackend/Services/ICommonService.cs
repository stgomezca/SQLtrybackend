using SQLtrybackend.DTOs;

namespace SQLtrybackend.Services
{
    public interface ICommonService<T, TI, TU> // Esto permite tener un generic en caso de que otros servicios utilicen esta interfaz.
    {
        Task<IEnumerable<T>> Get();
        Task<T> GetById(int id);
        Task<T> Add(TI beerInsertDto);
        Task<T> Update(int id, TU beerUpdateDto);
        Task<T> DeleteById(int id);
    }
}
