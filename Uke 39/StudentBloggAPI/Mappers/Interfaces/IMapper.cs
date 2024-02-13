namespace StudentBloggAPI.Mappers.Interfaces;

// Task<T> //generisk interface
public interface IMapper<TModel, TDto>  //TIn, TOut
{
    TDto MapToDTO(TModel model);
    TModel MapToModel(TDto dto);
}
