namespace Win32.Mappers;
internal interface IMapper<TFrom, TTo>
{
    internal TTo Map(TFrom source);
}
