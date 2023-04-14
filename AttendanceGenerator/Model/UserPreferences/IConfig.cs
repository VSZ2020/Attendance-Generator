namespace AttendanceGenerator.Model.UserPreferences
{
    public interface IConfig
    {
        string Name => GetType().Name;
    }
}