using Web_API.Infrastructure.Data;

namespace Web_API.Infrastructure.Repositories;

public class Repository(Context context)
{
    protected readonly Context Context = context;
}