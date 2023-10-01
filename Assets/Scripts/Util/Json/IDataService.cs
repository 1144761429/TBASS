public interface IDataService
{
    bool SaveData<T>(EPersistentDataPath mainPath, string relativePath, T data);
    T LoadData<T>(EPersistentDataPath mainPath, string relativePath);
}