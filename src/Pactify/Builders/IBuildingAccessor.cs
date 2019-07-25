namespace Pactify.Builders
{
    internal interface IBuildingAccessor<out TResult>
    {
        TResult Build();
    }
}
