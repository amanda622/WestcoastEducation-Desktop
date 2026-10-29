namespace WestcoastEducation.Interfaces;

// Contract for saving and loading data.
// Code that uses IStorage does not need to know HOW the data is stored
// (JSON file today, maybe a database later).
public interface IStorage<T> where T : class
{
    List<T> Read(string path);
    void Write(List<T> data, string path);
}
