namespace EnvBoard.Web.Model
{
    using System.IO;
    using Newtonsoft.Json;

    public class Data
    {
        private readonly string _stateStorageFilePath;

        public Data(string stateStorageFilePath)
        {
            _stateStorageFilePath = stateStorageFilePath;
        }

        public State LoadState()
        {
            return File.Exists(_stateStorageFilePath)
                ? JsonConvert.DeserializeObject<State>(File.ReadAllText(_stateStorageFilePath))
                : new State();
        }

        public void SaveState(State state)
        {
            string json = JsonConvert.SerializeObject(state);

            File.WriteAllText(_stateStorageFilePath, json);
        }
    }
}