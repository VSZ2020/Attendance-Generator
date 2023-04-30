namespace SQLiteRepository
{
    public interface IAppItemsRepository: IItemsRepository<AppContext>
    {
    }

    public class AppItemsRepository: IAppItemsRepository
    {

    }
}
